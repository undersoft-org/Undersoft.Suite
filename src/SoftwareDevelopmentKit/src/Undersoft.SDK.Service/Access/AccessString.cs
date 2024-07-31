using System.Text;

namespace Undersoft.SDK.Service.Access
{
    [Serializable]
    public class AccessString : IAccessString
    {
        private string _value;
        private string _type;
        private string _prefix;

        public AccessString() { }

        public AccessString(string value, string prefix = null, string type = null)
        {
            _value = value;
            _type = type;
            _prefix = prefix;
        }

        private void Set(string value)
        {
            var encoding = Encoding.GetEncoding("iso-8859-1");
            _value = Convert.ToBase64String(encoding.GetBytes(value));
        }

        private string Get(string value)
        {
            var encoding = Encoding.GetEncoding("iso-8859-1");
            return encoding.GetString(Convert.FromBase64String(value));
        }

        public string Decoded { get => Get(_value); set => Set(value); }

        public string Encoded { get => _prefix + _value; set => _value = value; }

    }
}
