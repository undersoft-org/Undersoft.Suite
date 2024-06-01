namespace Undersoft.SDK.Service.Application.GUI.Models;

public class Months
{
    public class MonthItem
    {
        public string Index { get; set; } = "00";
        public string Name { get; set; } = "";

        public override string ToString() => $"{Index:00} {Name}";
    }

    public static MonthItem[] AllMonths = Enumerable
        .Range(0, 12)
        .Select(i => new MonthItem { Index = $"{i + 1:00}", Name = GetMonthName(i) })
        .ToArray();

    private static string GetMonthName(int index)
    {
        return System.Globalization.DateTimeFormatInfo.CurrentInfo.MonthNames.ElementAt(index % 12);
    }
}
