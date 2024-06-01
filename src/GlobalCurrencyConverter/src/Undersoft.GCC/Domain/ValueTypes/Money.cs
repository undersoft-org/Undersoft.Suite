using System.Runtime.InteropServices;
using Undersoft.SDK.Instant.Attributes;
using Undersoft.SDK.Uniques;

namespace Undersoft.GCC.Domain.ValueTypes;

[StructLayout(LayoutKind.Sequential)]
public struct Money
{
    public double Value { get; set; }

    [InstantAs(UnmanagedType.ByValTStr, SizeConst = 3)]
    public string Code { get; set; }

    public long CurrencyId { get => !string.IsNullOrEmpty(Code) ? Code.UniqueKey64() : 0; }
}
