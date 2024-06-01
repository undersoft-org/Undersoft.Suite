using Undersoft.SDK.Series;

namespace Undersoft.SDK.Service.Application.GUI.Models;

public record Country(string Code, string Name);

public class Countries
{
    private Listing<string> names = new Listing<string>(
        _countries
            .Select(c => new SeriesItem<string>(c.Key.ToUpper(), c.Key))
            .Concat(
                _countries
                    .Select(c => new SeriesItem<string>(c.Value.Code.ToUpper(), c.Key))
                    .ToArray()
            )
            .ToArray()
    );
    public Listing<string> Names => names;

    private Listing<string> codes = new Listing<string>(
        _countries.Select(c => new SeriesItem<string>(c.Key.ToUpper(), c.Value.Code)).ToArray()
    );
    public Listing<string> Codes => codes;

    private readonly static Dictionary<string, Country> _countries = new Dictionary<
        string,
        Country
    >()
    {
        { "Argentina", new Country("ar", "Argentina") },
        { "Armenia", new Country("am", "Armenia") },
        { "Australia", new Country("au", "Australia") },
        { "Austria", new Country("at", "Austria") },
        { "Azerbaijan", new Country("az", "Azerbaijan") },
        { "Bahamas", new Country("bs", "Bahamas") },
        { "Bahrain", new Country("bh", "Bahrain") },
        { "Belarus", new Country("by", "Belarus") },
        { "Belgium", new Country("be", "Belgium") },
        { "Bermuda", new Country("bm", "Bermuda") },
        { "Botswana", new Country("bw", "Botswana") },
        { "Brazil", new Country("br", "Brazil") },
        { "Bulgaria", new Country("bg", "Bulgaria") },
        { "Burkina Faso", new Country("bf", "Burkina Faso") },
        { "Canada", new Country("ca", "Canada") },
        { "Chinese Taipei", new Country("tpe", "Taipei") },
        { "Colombia", new Country("co", "Colombia") },
        { "Côte d'Ivoire", new Country("ci", "Côte d'Ivoire") },
        { "Croatia", new Country("hr", "Croatia") },
        { "Cuba", new Country("cu", "Cuba") },
        { "Czech Republic", new Country("cz", "Czech") },
        { "Denmark", new Country("dk", "Denmark") },
        { "Dominican Republic", new Country("do", "Dominicana") },
        { "Ecuador", new Country("ec", "Ecuador") },
        { "Egypt", new Country("eg", "Egypt") },
        { "Estonia", new Country("ee", "Estonia") },
        { "Ethiopia", new Country("et", "Ethiopia") },
        { "Fiji", new Country("fj", "Fiji") },
        { "Finland", new Country("fi", "Finland") },
        { "France", new Country("fr", "France") },
        { "Georgia", new Country("ge", "Georgia") },
        { "Germany", new Country("de", "Germany") },
        { "Ghana", new Country("gh", "Ghana") },
        { "UK", new Country("uk", "United Kingdom") },
        { "Greece", new Country("gr", "Greece") },
        { "Grenada", new Country("gd", "Grenada") },
        { "Hong Kong", new Country("hk", "Hong Kong") },
        { "Hungary", new Country("hu", "Hungary") },
        { "India", new Country("in", "India") },
        { "Indonesia", new Country("id", "Indonesia") },
        { "Ireland", new Country("ie", "Ireland") },
        { "Iran", new Country("ir", "Iran") },
        { "Israel", new Country("il", "Israel") },
        { "Italy", new Country("it", "Italy") },
        { "Jamaica", new Country("jm", "Jamaica") },
        { "Japan", new Country("jp", "Japan") },
        { "Jordan", new Country("jo", "Jordan") },
        { "Kazakhstan", new Country("kz", "Kazakhstan") },
        { "Kenya", new Country("ke", "Kenya") },
        { "Kosovo", new Country("xk", "Kosovo") },
        { "Kuwait", new Country("kw", "Kuwait") },
        { "Kyrgyzstan", new Country("kg", "Kyrgyzstan") },
        { "Latvia", new Country("lv", "Latvia") },
        { "Lithuania", new Country("lt", "Lithuania") },
        { "Malaysia", new Country("my", "Malaysia") },
        { "Mexico", new Country("mx", "Mexico") },
        { "Mongolia", new Country("mn", "Mongolia") },
        { "Morocco", new Country("ma", "Morocco") },
        { "Namibia", new Country("na", "Namibia") },
        { "Netherlands", new Country("nl", "Netherlands") },
        { "New Zealand", new Country("nz", "New Zealand") },
        { "Nigeria", new Country("ng", "Nigeria") },
        { "North Macedonia", new Country("mk", "North Macedonia") },
        { "Norway", new Country("no", "Norway") },
        { "China", new Country("cn", "China") },
        { "Philippines", new Country("ph", "Philippines") },
        { "Poland", new Country("pl", "Poland") },
        { "Portugal", new Country("pt", "Portugal") },
        { "Puerto Rico", new Country("pr", "Puerto Rico") },
        { "Qatar", new Country("qa", "Qatar") },
        { "South Korea", new Country("kr", "South Korea") },
        { "Republic of Moldova", new Country("md", "Moldova") },
        { "ROC", new Country("roc", "ROC") },
        { "Romania", new Country("ro", "Romania") },
        { "San Marino", new Country("sm", "San Marino") },
        { "Saudi Arabia", new Country("sa", "Saudi Arabia") },
        { "Serbia", new Country("rs", "Serbia") },
        { "Slovakia", new Country("sk", "Slovakia") },
        { "Slovenia", new Country("si", "Slovenia") },
        { "South Africa", new Country("za", "South Africa") },
        { "Spain", new Country("es", "Spain") },
        { "Sweden", new Country("se", "Sweden") },
        { "Switzerland", new Country("ch", "Switzerland") },
        { "Syrian Arab Republic", new Country("sy", "Syria") },
        { "Thailand", new Country("th", "Thailand") },
        { "Tunisia", new Country("tn", "Tunisia") },
        { "Turkey", new Country("tr", "Turkey") },
        { "Turkmenistan", new Country("tm", "Turkmenistan") },
        { "Uganda", new Country("ug", "Uganda") },
        { "Ukraine", new Country("ua", "Ukraine") },
        { "USA", new Country("us", "USA") },
        { "Uzbekistan", new Country("uz", "Uzbekistan") },
        { "Venezuela", new Country("ve", "Venezuela") }
    };
}
