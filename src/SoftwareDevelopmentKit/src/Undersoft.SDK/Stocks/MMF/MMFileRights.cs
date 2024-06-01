namespace Undersoft.SDK.Stocks.MMF
{
#if !NET40Plus

    [Flags]
    public enum MMFileRights : uint
    {
        Write = 0x02,

        Read = 0x04,

        ReadWrite = Write | Read,
    }
#endif
}