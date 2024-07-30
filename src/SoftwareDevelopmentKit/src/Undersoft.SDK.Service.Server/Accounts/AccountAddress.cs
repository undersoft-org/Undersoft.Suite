namespace Undersoft.SDK.Service.Server.Accounts;

public class AccountAddress : DataObject
{
    public string Country { get; set; }

    public string State { get; set; }

    public string City { get; set; }

    public string Street { get; set; }

    public string Building { get; set; }

    public string Apartment { get; set; }

    public string Postcode { get; set; }

    public long? AccountId { get; set; }
    public virtual Account Account { get; set; }
}
