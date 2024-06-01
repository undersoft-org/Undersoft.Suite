namespace Undersoft.SDK.Stocks
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct StockHeader
    {
        #region Fields

        public long FreeMemorySize;
        public long ItemCapacity;
        public int ItemCount;
        public int ItemSize;
        public ushort SectorId;
        public long SharedMemorySize;
        public volatile int Shutdown;
        public ushort ClusterId;
        public long UsedMemorySize;

        #endregion
    }
}
