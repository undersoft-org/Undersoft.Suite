using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Undersoft.SDK.Service.Data.Identifier;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Undersoft.SDK.Instant.Attributes;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Object;

[DataContract]
public class Identifier<TObject> : Identifier, IIdentifier<TObject>
    where TObject : IOrigin, IInnerProxy
{
    private long _typeId;
    public string _typeName;

    public Identifier() : base() { }

    public Identifier(string name, object value) : base(name, value) { }

    public Identifier(TObject item, string name, object value) : base(name, value)
    {
        Object = item;
        ObjectId = item.Id;
    }

    [IdentityRubric(Order = 1)]
    [DataMember(Order = 2)]
    [Column(Order = 2)]
    public override long TypeId
    {
        get => Object != null ? Object.TypeId : _typeId;
        set
        {
            if (Object != null)
                Object.TypeId = value;
            else
                _typeId = value;
        }
    }

    [StringLength(768)]
    [DataMember(Order = 5)]
    [Column(Order = 5)]
    [InstantAs(UnmanagedType.ByValTStr, SizeConst = 768)]
    public override string TypeName
    {
        get => Object != null ? Object.TypeName : _typeName;
        set
        {
            if (Object != null)
                Object.TypeName = value;
            else
                _typeName = value;
        }
    }

    [DataMember(Order = 17)]
    public virtual TObject Object { get; set; }
}

[DataContract]
[StructLayout(LayoutKind.Sequential)]
public class Identifier : DataObject, IIdentifier
{
    private long _code;

    private string _lastvalue;
    private string _value;

    public Identifier() { }

    public Identifier(string name, object value)
    {
        Name = name;
        Value = value.ToString();
    }

    [DataMember(Order = 12)]
    public virtual long ObjectId { get; set; }

    [DataMember(Order = 13)]
    public virtual IdKind Kind { get; set; }

    [DataMember(Order = 14)]
    public virtual string Name { get; set; }

    [DataMember(Order = 15)]
    public virtual string Value
    {
        get => _value;
        set => _value = value;
    }

    [DataMember(Order = 16)]
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

                if (_value == _lastvalue && code != value)
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
    Organization = 139,
    Professional = 140,
    Address = 141,
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
    Postcode = 432,
    Role = 433,
    Street = 892,
    Country = 931,
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
