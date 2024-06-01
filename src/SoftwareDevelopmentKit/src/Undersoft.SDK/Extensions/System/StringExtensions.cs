namespace System
{
    public static unsafe class StringExtensions
    {
        public static string FirstDelimited(this string delimitedString, char delimeter)
        {
            int index = delimitedString.IndexOf(delimeter);

            if (index < 0)
                return default!;

            fixed (char* p = delimitedString)
                return new string(p, 0, index);
        }

        public static string LastDelimited(this string delimitedString, char delimeter)
        {
            int index = delimitedString.LastIndexOf(delimeter);

            if (index < 0)
                return default!;

            fixed (char* p = delimitedString)
                return new string(p, index + 1, (index + 1) - delimitedString.Length);
        }
    }
}
