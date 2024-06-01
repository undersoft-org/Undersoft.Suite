namespace Undersoft.SDK.Service.Application.GUI.Models;

public static class UndersoftColorUtilities
{
    public static readonly UndersoftColors[] AllColors = Enum.GetValues<UndersoftColors>();

    public static UndersoftColors GetRandom(bool skipDefault = true)
    {

        IEnumerable<UndersoftColors>? values = AllColors.Skip(skipDefault ? 1 : 0);

        return values.ElementAt(new Random().Next(values.Count()));
    }


}