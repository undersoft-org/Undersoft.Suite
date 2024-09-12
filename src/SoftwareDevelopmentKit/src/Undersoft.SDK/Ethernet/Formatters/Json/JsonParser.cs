namespace Undersoft.SDK.Ethernet
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Undersoft.SDK.Instant;
    using Undersoft.SDK.Proxies;
    using Undersoft.SDK.Uniques;

    public enum JsonToken
    {
        Unknown,
        LeftBrace,
        RightBrace,
        Colon,
        Comma,
        LeftBracket,
        RightBracket,
        String,
        Number,
        True,
        False,
        Null
    }

    public interface IJson { }

    public class InvalidJsonException : Exception
    {
        public InvalidJsonException(string message)
            : base(message) { }
    }

    public class JsonParser
    {
        private static readonly char[] _base16 = new[]
        {
            '0',
            '1',
            '2',
            '3',
            '4',
            '5',
            '6',
            '7',
            '8',
            '9',
            'A',
            'B',
            'C',
            'D',
            'E',
            'F'
        };
        private static readonly IDictionary<string, IDictionary<Type, PropertyInfo[]>> _cache;
        private static readonly string[][] _unichar = new string[][]
        {
            new string[] { new string('"', 1), @"\""", },
            new string[] { new string('/', 1), @"\/", },
            new string[] { new string('\\', 1), @"\\", },
            new string[] { new string('\b', 1), @"\b", },
            new string[] { new string('\f', 1), @"\f", },
            new string[] { new string('\n', 1), @"\n", },
            new string[] { new string('\r', 1), @"\r", },
            new string[] { new string('\t', 1), @"\t", }
        };
        private static NumberStyles _numbers = NumberStyles.Float;
        private static readonly NumberFormatInfo _nfi = JsonParserProperties.JsonNumberInfo();

        static JsonParser()
        {
            _cache = new Dictionary<string, IDictionary<Type, PropertyInfo[]>>();
            foreach (string cmplx in Enum.GetNames(typeof(TransitComplexity)))
                _cache.Add(cmplx, new Dictionary<Type, PropertyInfo[]>());
        }

        public static T Deserialize<T>(string json)
        {
            T instance;
            var map = PrepareInstance(out instance);
            var bag = FromJson(json);

            DeserializeImpl(map, bag, instance);
            return instance;
        }

        public static object Deserialize(string json, Type type)
        {
            object instance;
            var map = PrepareInstance(out instance, type);
            var bag = FromJson(json);

            DeserializeImpl(map, bag, instance);
            return instance;
        }

        public static void DeserializeType(
            IEnumerable<PropertyInfo> map,
            IDictionary<string, object> bag,
            object instance
        )
        {
            IProxy sleeve = instance.ToProxy();
            int i = 0;
            foreach (PropertyInfo info in map)
            {
                bool mutated = false;
                string key = info.Name;
                if (!bag.ContainsKey(key))
                {
                    var capitalize = info.Name.Replace("_", "")[0].ToString().ToUpper();
                    key = (capitalize + info.Name.Remove(0, 1));
                    if (!bag.ContainsKey(key))
                    {
                        key = info.Name.Replace("-", "");
                        if (!bag.ContainsKey(key))
                            continue;
                    }
                }

                object value = bag[key];

                if (value != null && value.GetType() == typeof(String))
                    if (value.Equals("null"))
                        value = null;

                if (value != null)
                {
                    if (info.PropertyType == typeof(byte[]))
                        if (value.GetType() == typeof(List<object>))
                            value = ((List<object>)value)
                                .Select(symbol => Convert.ToByte(symbol))
                                .ToArray();
                        else
                            value = ((object[])value)
                                .Select(symbol => Convert.ToByte(symbol))
                                .ToArray();
                    else if (info.PropertyType == typeof(Uscn))
                        value = new Uscn(value.ToString());
                    else if (info.PropertyType == typeof(Single))
                        value = Convert.ToSingle(value, _nfi);
                    else if (info.PropertyType == typeof(DateTime))
                        value = Convert.ToDateTime(value);
                    else if (info.PropertyType == typeof(double))
                        value = Convert.ToDouble(value, _nfi);
                    else if (info.PropertyType == typeof(decimal))
                        value = Convert.ToDecimal(value, _nfi);
                    else if (info.PropertyType == typeof(int))
                        value = Convert.ToInt32(value);
                    else if (info.PropertyType == typeof(long))
                        value = Convert.ToInt64(value);
                    else if (info.PropertyType == typeof(short))
                        value = Convert.ToInt16(value);
                    else if (info.PropertyType == typeof(bool))
                        value = Convert.ToBoolean(value);
                    else if (info.PropertyType == typeof(IInstant))
                    {
                        object n = info.GetValue(instance, null);
                        DeserializeType(
                            n.GetType().GetProperties(),
                            (Dictionary<string, object>)value,
                            n
                        );
                        mutated = true;
                    }
                    else if (info.PropertyType == typeof(Type))
                    {
                        object typevalue = info.GetValue(instance, null);
                        if (value != null)
                            typevalue = Type.GetType(value.ToString());
                        value = typevalue;
                    }
                    else if (info.PropertyType.IsEnum)
                    {
                        object enumvalue = info.GetValue(instance, null);
                        enumvalue = Enum.Parse(info.PropertyType, value.ToString());
                        value = enumvalue;
                    }
                }

                if (!mutated)
                    sleeve[i] = value;
            }
        }

        public static IDictionary<string, object> FromJson(string json)
        {
            JsonToken type;

            var result = FromJson(json, out type);

            switch (type)
            {
                case JsonToken.LeftBrace:
                    var @object = (IDictionary<string, object>)result.Single().Value;
                    return @object;
            }

            return result;
        }

        public static IDictionary<string, object> FromJson(string json, out JsonToken type)
        {
            var data = json.ToCharArray();
            var index = 0;

            var token = NextToken(data, ref index);
            switch (token)
            {
                case JsonToken.LeftBrace:
                case JsonToken.LeftBracket:
                    index--;
                    type = token;
                    break;
                default:
                    throw new InvalidJsonException("JSON must begin with an object or vector");
            }

            return ParseObject(data, ref index);
        }

        public static IEnumerable<PropertyInfo> PrepareInstance(out object instance, Type type)
        {
            instance = Activator.CreateInstance(type);

            CacheReflection(type);

            return _cache["Standard"][type];
        }

        public static IEnumerable<PropertyInfo> PrepareInstance<T>(out T instance)
        {
            instance = Activator.CreateInstance<T>();
            Type item = typeof(T);

            CacheReflection(item);

            return _cache["Standard"][item];
        }

        public static object Serialize(object instance)
        {
            Type type = instance.GetType();
            IDictionary<string, object> bag = GetBagForObject(type, instance);

            return ToJson(bag);
        }

        public static string Serialize<T>(T instance)
        {
            IDictionary<string, object> bag = GetBagForObject(instance);

            return ToJson(bag);
        }

        public static string ToJson(
            IDictionary<int, object> bag,
            TransitComplexity complexity = TransitComplexity.Standard
        )
        {
            var sb = new StringBuilder(0);

            SerializeItem(sb, bag, null, complexity);

            return sb.ToString();
        }

        public static string ToJson(
            IDictionary<string, object> bag,
            TransitComplexity complexity = TransitComplexity.Standard
        )
        {
            var sb = new StringBuilder(4096);

            SerializeItem(sb, bag, null, complexity);

            return sb.ToString();
        }

        internal static string BaseConvert(int input, char[] charSet, int minLength)
        {
            var sb = new StringBuilder();
            var @base = charSet.Length;

            while (input > 0)
            {
                var index = input % @base;
                sb.Insert(0, new[] { charSet[index] });
                input = input / @base;
            }

            while (sb.Length < minLength)
            {
                sb.Insert(0, "0");
            }

            return sb.ToString();
        }

        internal static void CacheReflection(
            Type item,
            TransitComplexity complexity = TransitComplexity.Standard
        )
        {
            if (_cache[complexity.ToString()].ContainsKey(item))
                return;

            PropertyInfo[] verified = new PropertyInfo[0];

            _cache[complexity.ToString()].Add(item, verified);
        }

        internal static IDictionary<string, object> GetBagForObject<T>(
            T instance,
            TransitComplexity complexity = TransitComplexity.Standard
        )
        {
            return GetBagForObject(typeof(T), instance, complexity);
        }

        internal static IDictionary<string, object> GetBagForObject(
            Type type,
            object instance,
            TransitComplexity complexity = TransitComplexity.Standard
        )
        {
            CacheReflection(type, complexity);

            if (type.FullName == null)
            {
                return null;
            }

            bool anonymous = type.FullName.Contains("__AnonymousType");
            PropertyInfo[] map = _cache[complexity.ToString()][type];

            IDictionary<string, object> bag = InitializeBag();
            foreach (PropertyInfo info in map)
            {
                if (info != null)
                {
                    var readWrite = (info.CanWrite && info.CanRead);
                    if (!readWrite && !anonymous)
                    {
                        continue;
                    }
                    object value = null;
                    try
                    {
                        value = info.GetValue(instance, null);
                    }
                    catch (Exception ex) { }
                    bag.Add(info.Name, value);
                }
            }

            return bag;
        }

        internal static void GetKeyword(
            string word,
            JsonToken target,
            IList<char> data,
            ref int index,
            ref JsonToken result
        )
        {
            int buffer = data.Count - index;
            if (buffer < word.Length)
            {
                return;
            }

            for (var i = 0; i < word.Length; i++)
            {
                if (data[index + i] != word[i])
                {
                    return;
                }
            }

            result = target;
            index += word.Length;
        }

        internal static JsonToken GetTokenFromSymbol(char symbol)
        {
            return GetTokenFromSymbol(symbol, JsonToken.Unknown);
        }

        internal static JsonToken GetTokenFromSymbol(char symbol, JsonToken token)
        {
            switch (symbol)
            {
                case '{':
                    token = JsonToken.LeftBrace;
                    break;
                case '}':
                    token = JsonToken.RightBrace;
                    break;
                case ':':
                    token = JsonToken.Colon;
                    break;
                case ',':
                    token = JsonToken.Comma;
                    break;
                case '[':
                    token = JsonToken.LeftBracket;
                    break;
                case ']':
                    token = JsonToken.RightBracket;
                    break;
                case '"':
                    token = JsonToken.String;
                    break;
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case '.':
                case 'e':
                case 'E':
                case '+':
                case '-':
                    token = JsonToken.Number;
                    break;
            }
            return token;
        }

        internal static string GetUnicode(char character)
        {
            switch (character)
            {
                case '"':
                    return @"\""";
                case '/':
                    return @"\/";
                case '\\':
                    return @"\\";
                case '\b':
                    return @"\b";
                case '\f':
                    return @"\f";
                case '\n':
                    return @"\n";
                case '\r':
                    return @"\r";
                case '\t':
                    return @"\t";
            }
            return new string(character, 1);
        }

        internal static void IgnoreWhitespace(IList<char> data, ref int index, char symbol)
        {
            var token = JsonToken.Unknown;
            IgnoreWhitespace(data, ref index, ref token, symbol);
            return;
        }

        internal static JsonToken IgnoreWhitespace(
            IList<char> data,
            ref int index,
            ref JsonToken token,
            char symbol
        )
        {
            switch (symbol)
            {
                case ' ':
                case '\\':
                case '/':
                case '\b':
                case '\f':
                case '\n':
                case '\r':
                case '\t':
                    index++;
                    token = NextToken(data, ref index);
                    break;
            }
            return token;
        }

        internal static Dictionary<string, object> InitializeBag()
        {
            return new Dictionary<string, object>(0, StringComparer.OrdinalIgnoreCase);
        }

        internal static JsonToken NextToken(IList<char> data, ref int index)
        {
            var symbol = data[index];
            var token = GetTokenFromSymbol(symbol);
            token = IgnoreWhitespace(data, ref index, ref token, symbol);

            GetKeyword("true", JsonToken.True, data, ref index, ref token);
            GetKeyword("false", JsonToken.False, data, ref index, ref token);
            GetKeyword("null", JsonToken.Null, data, ref index, ref token);

            return token;
        }

        internal static IEnumerable<object> ParseArray(IList<char> data, ref int index)
        {
            var result = new List<object>();

            index++;
            while (index < data.Count - 1)
            {
                var token = NextToken(data, ref index);
                switch (token)
                {
                    case JsonToken.Unknown:
                        throw new InvalidJsonException(
                            string.Format(
                                "Invalid JSON found while parsing an vector at index {0}.",
                                index
                            )
                        );
                    case JsonToken.RightBracket:
                        index++;
                        return result;

                    case JsonToken.Comma:
                    case JsonToken.RightBrace:
                    case JsonToken.Colon:
                        index++;
                        break;

                    case JsonToken.LeftBrace:
                        var nested = ParseObject(data, ref index);
                        result.Add(nested);
                        break;
                    case JsonToken.LeftBracket:
                    case JsonToken.String:
                    case JsonToken.Number:
                    case JsonToken.True:
                    case JsonToken.False:
                    case JsonToken.Null:
                        var value = ParseValue(data, ref index);
                        result.Add(value);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return result;
        }

        internal static object ParseNumber(IList<char> data, ref int index)
        {
            var symbol = data[index];
            IgnoreWhitespace(data, ref index, symbol);

            var start = index;
            var length = 0;
            while (ParseToken(JsonToken.Number, data, ref index))
            {
                length++;
                index++;
            }

            double result;
            var buffer = new string(data.Skip(start).Take(length).ToArray());
            if (!double.TryParse(buffer, _numbers, CultureInfo.InvariantCulture, out result))
            {
                throw new InvalidJsonException(
                    string.Format("Value '{0}' was not a valid JSON number", buffer)
                );
            }
            return result;
        }

        internal static IDictionary<string, object> ParseObject(IList<char> data, ref int index)
        {
            var result = InitializeBag();

            index++;

            while (index < data.Count - 1)
            {
                var token = NextToken(data, ref index);
                switch (token)
                {
                    case JsonToken.Unknown:
                    case JsonToken.True:
                    case JsonToken.False:
                    case JsonToken.Null:
                    case JsonToken.Colon:
                    case JsonToken.RightBracket:
                    case JsonToken.Number:
                        throw new InvalidJsonException(
                            string.Format(
                                "Invalid JSON found while parsing an object at index {0}.",
                                index
                            )
                        );
                    case JsonToken.RightBrace:
                        index++;
                        return result;

                    case JsonToken.Comma:
                        index++;
                        break;

                    case JsonToken.LeftBrace:
                        var @object = ParseObject(data, ref index);
                        if (@object != null)
                        {
                            result.Add(string.Concat("object", result.Count), @object);
                        }
                        index++;
                        break;
                    case JsonToken.LeftBracket:
                        var @array = ParseArray(data, ref index);
                        if (@array != null)
                        {
                            result.Add(string.Concat("vector", result.Count), @array);
                        }
                        index++;
                        break;
                    case JsonToken.String:
                        var pair = ParsePair(data, ref index);
                        result.Add(pair.Key, pair.Value);
                        break;
                    default:
                        throw new NotSupportedException("Invalid token expected.");
                }
            }

            return result;
        }

        internal static KeyValuePair<string, object> ParsePair(IList<char> data, ref int index)
        {
            var valid = true;

            var name = ParseString(data, ref index);
            if (name == null)
            {
                valid = false;
            }

            if (!ParseToken(JsonToken.Colon, data, ref index))
            {
                valid = false;
            }

            if (!valid)
            {
                throw new InvalidJsonException(
                    string.Format(
                        "Invalid JSON found while parsing a value pair at index {0}.",
                        index
                    )
                );
            }

            index++;
            var value = ParseValue(data, ref index);
            return new KeyValuePair<string, object>(name, value);
        }

        internal static string ParseString(IList<char> data, ref int index)
        {
            var symbol = data[index];
            IgnoreWhitespace(data, ref index, symbol);
            symbol = data[++index];

            var sb = new StringBuilder();
            while (true)
            {
                if (index >= data.Count - 1)
                {
                    return null;
                }
                switch (symbol)
                {
                    case '"':
                        index++;
                        return sb.ToString();
                    case '\\':
                        symbol = data[++index];
                        switch (symbol)
                        {
                            case '/':
                            case '\\':
                            case '"':
                                sb.Append(symbol);
                                break;
                            case 'b':
                                sb.Append('\b');
                                break;
                            case 'f':
                                sb.Append('\f');
                                break;
                            case 'n':
                                sb.Append('\n');
                                break;
                            case 'r':
                                sb.Append('\r');
                                break;
                            case 't':
                                sb.Append('\t');
                                break;
                            case 'u':
                                if (index < data.Count - 5)
                                {
                                    var array = data.ToArray();
                                    var buffer = new char[4];
                                    Array.Copy(array, index + 1, buffer, 0, 4);

                                    var hex = new string(buffer);
                                    var unicode = (char)Convert.ToInt32(hex, 16);
                                    sb.Append(unicode);
                                    index += 4;
                                }
                                else
                                {
                                    break;
                                }
                                break;
                        }
                        break;
                    default:
                        sb.Append(symbol);
                        break;
                }
                symbol = data[++index];
            }
        }

        internal static bool ParseToken(JsonToken token, IList<char> data, ref int index)
        {
            var nextToken = NextToken(data, ref index);
            return token == nextToken;
        }

        internal static object ParseValue(IList<char> data, ref int index)
        {
            var token = NextToken(data, ref index);
            switch (token)
            {
                case JsonToken.RightBracket:
                case JsonToken.RightBrace:
                case JsonToken.Unknown:
                case JsonToken.Colon:
                case JsonToken.Comma:
                    throw new InvalidJsonException(
                        string.Format(
                            "Invalid JSON found while parsing a value at index {0}.",
                            index
                        )
                    );

                case JsonToken.LeftBrace:
                    return ParseObject(data, ref index);
                case JsonToken.LeftBracket:
                    return ParseArray(data, ref index);
                case JsonToken.String:
                    return ParseString(data, ref index);
                case JsonToken.Number:
                    return ParseNumber(data, ref index);
                case JsonToken.True:
                    return true;
                case JsonToken.False:
                    return false;
                case JsonToken.Null:
                    return null;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        internal static void SerializeArray(
            object item,
            StringBuilder sb,
            TransitComplexity complexity = TransitComplexity.Standard
        )
        {
            Type type = item.GetType();
            if (type.IsDefined(typeof(JsonObjectAttribute), false))
            {
                var bag = GetBagForObject(item.GetType(), item, complexity);
                SerializeItem(sb, bag, null, complexity);
            }
            else
            {
                ICollection array = (ICollection)item;

                sb.Append("[");
                var count = 0;

                var total = array.Cast<object>().Count();
                foreach (object _item in array)
                {
                    SerializeItem(sb, _item, null, complexity);
                    count++;
                    if (count < total)
                        sb.Append(",");
                }
                sb.Append("]");
            }
        }

        internal static void SerializeDateTime(StringBuilder sb, object item = null)
        {
            sb.Append("\"" + ((DateTime)item).ToString("yyyy-MM-dd HH:mm:dd") + "\"");
        }

        internal static void SerializeItem(
            StringBuilder sb,
            object item,
            string key = null,
            TransitComplexity complexity = TransitComplexity.Standard
        )
        {
            if (item == null)
            {
                sb.Append("null");
                return;
            }

            if (item is IDictionary)
            {
                SerializeObject(item, sb, false, complexity);
                return;
            }

            if (item is ICollection && !(item is string))
            {
                SerializeArray(item, sb, complexity);
                return;
            }

            if (item is Usid)
            {
                sb.Append("\"" + ((Usid)item).ToString() + "\"");
                return;
            }

            if (item is Uscn)
            {
                sb.Append("\"" + ((Uscn)item).ToString() + "\"");
                return;
            }

            if (item is DateTime)
            {
                sb.Append("\"" + ((DateTime)item).ToString("yyyy-MM-dd HH:mm:dd") + "\"");
                return;
            }

            if (item is Enum)
            {
                sb.Append("\"" + item.ToString() + "\"");
                return;
            }

            if (item is Type)
            {
                sb.Append("\"" + ((Type)item).FullName + "\"");
                return;
            }

            if (item is bool)
            {
                sb.Append(((bool)item).ToString().ToLower());
                return;
            }

            if (item is ValueType)
            {
                sb.Append(item.ToString().Replace(',', '.'));
                return;
            }

            IDictionary<string, object> bag = GetBagForObject(item.GetType(), item, complexity);

            SerializeItem(sb, bag, key, complexity);
        }

        internal static void SerializeObject(
            object item,
            StringBuilder sb,
            bool intAsKey = false,
            TransitComplexity complexity = TransitComplexity.Standard
        )
        {
            sb.Append("{");

            IDictionary nested = (IDictionary)item;
            int i = 0;
            int count = nested.Count;
            foreach (DictionaryEntry entry in nested)
            {
                sb.Append("\"" + entry.Key + "\"");
                sb.Append(":");

                object value = entry.Value;
                if (value is string)
                {
                    SerializeString(sb, value);
                }
                else
                {
                    SerializeItem(sb, entry.Value, entry.Key.ToString(), complexity);
                }
                if (i < count - 1)
                {
                    sb.Append(",");
                }
                i++;
            }

            sb.Append("}");
        }

        internal static void SerializeString(StringBuilder sb, object item)
        {
            char[] symbols = ((string)item).ToCharArray();
            SerializeUnicode(sb, symbols);
        }

        internal static void SerializeUnicode(StringBuilder sb, char[] symbols)
        {
            sb.Append("\"");

            string[] unicodes = symbols.Select(symbol => GetUnicode(symbol)).ToArray();

            foreach (var unicode in unicodes)
                sb.Append(unicode);

            sb.Append("\"");
        }

        private static void DeserializeImpl(
            IEnumerable<PropertyInfo> map,
            IDictionary<string, object> bag,
            object instance
        )
        {
            DeserializeType(map, bag, instance);
        }

        private static void DeserializeImpl<T>(
            IEnumerable<PropertyInfo> map,
            IDictionary<string, object> bag,
            T instance
        )
        {
            DeserializeType(map, bag, instance);
        }
    }

    public static class JsonParserProperties
    {
        public static NumberFormatInfo JsonNumberInfo()
        {
            System.Globalization.NumberFormatInfo nfi = new System.Globalization.NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            return nfi;
        }
    }
}
