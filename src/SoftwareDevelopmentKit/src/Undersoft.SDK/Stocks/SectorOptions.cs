using System.Runtime.InteropServices;
using Undersoft.SDK.Instant.Series;

namespace Undersoft.SDK.Stocks
{
    public class SectorOptions<T> : SectorOptions
    {
        public SectorOptions() : base(typeof(T)) { }

        public SectorOptions(IInstantSeries instantSeries, ushort clusterId, ushort sectorId)
            : base(instantSeries.InstantType, clusterId, sectorId, instantSeries.InstantSize)
        {
            this.instantSeries = instantSeries;
            Type = instantSeries.Type;
        }

        public SectorOptions(ushort clusterId, ushort sectorId)
            : base(typeof(T), sectorId, sectorId) { }

        public SectorOptions(ushort clusterId, ushort sectorId, int blockSize)
            : base(typeof(T), sectorId, sectorId, blockSize) { }
    }

    public class SectorOptions : StockOptions
    {
        public SectorOptions() { }

        public SectorOptions(IInstantSeries instantSeries, ushort clusterId, ushort sectorId)
            : this(instantSeries.InstantType, clusterId, sectorId, instantSeries.InstantSize)
        {
            this.instantSeries = instantSeries;
            Type = instantSeries.Type;
        }

        public SectorOptions(Type type)
        {
            ItemType = type;
            blocksize = Marshal.SizeOf(type);
        }

        public SectorOptions(Type type, ushort clusterId, ushort sectorId, int blockSize)
        {
            ItemType = type;
            this.clusterId = clusterId;
            this.sectorId = sectorId;
            blocksize = blockSize;
        }

        public SectorOptions(Type type, ushort clusterId, ushort sectorId) : this(type)
        {
            this.clusterId = clusterId;
            this.sectorId = sectorId;
        }

        protected ushort clusterId;
        protected ushort sectorId;

        public ushort ClusterId
        {
            get => clusterId;
            set => clusterId = value;
        }

        public ushort SectorId
        {
            get => sectorId;
            set => sectorId = value;
        }

        public override string FileName => $"{type.Name}.{ClusterId}.{SectorId}.std";

        public string SectorName => $"{BasePath}__{type.FullName}.{ClusterId}.{SectorId}.std";

        public string SectorPath => $"{BasePath}/{type.Name}/{FileName}";
    }
}
