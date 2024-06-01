using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Undersoft.SDK.Service.Data.Identifier;

using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Object;

[DataContract]
public class Identifier<TObject> : Identifier, IIdentifier<TObject> where TObject : IOrigin, IInnerProxy
{
    [JsonIgnore]
    [IgnoreDataMember]
    public virtual TObject Object { get; set; }
}

[StructLayout(LayoutKind.Sequential)]
public class Identifier : DataObject, IIdentifier
{
    private long _code;

    private string _lastvalue;
    private string _value;

    public virtual long ObjectId { get; set; }

    public virtual IdKind Kind { get; set; }

    public virtual string Name { get; set; }

    public virtual string Value
    {
        get => _value;
        set => _value = value;
    }

    public long Key
    {
        get
        {
            if (_value != _lastvalue)
            {
                _code = _value.UniqueKey();
                _lastvalue = _value;
            }
            return _code;
        }
        set
        {

            if (_value != null)
            {
                long code;
                if (_lastvalue == null)
                    code = Key;
                else
                    code = _code;

                if (_value == _lastvalue
                    && code != value)
                {
                    _code = value;
                }
            }
        }
    }
}

public enum IdKind
{
    None = 0,
    Id = 1,
    ExtId = 3,
    Email = 7,
    Phone = 11,
    SSO = 13,
    EAN = 17,
    UPC = 19,
    PAN = 23,
    ISBN = 31,
    Card = 37,
    Chip = 43,
    IBAN = 51,
    NFC = 61,
    VAT = 91,
    Tax = 123,
    Personal = 137,
    National = 143,
    IdCard = 151,
    Passport = 167,
    Category = 179,
    Group = 181,
    IP = 191,
    URI = 193,
    Datetime = 197,
    Money = 199,
    Score = 211,
    CodeNumber = 223,
    Number = 227,
    FullName = 245,
    Name = 173,
    TypeName = 299,
    ZipCode = 432,
    Role = 433,
    Street = 892,
    CountryCode = 883,
    Module = 923,
    Key = 2322,
    TypeKey = 3423,
    Modifier = 4231,
    Modified = 4565,
    Creator = 5232,
    Created = 6543,
    Label = 7878,
    Kind = 8989
}
