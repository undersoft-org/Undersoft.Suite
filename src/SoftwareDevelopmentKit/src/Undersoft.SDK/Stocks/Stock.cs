using System.Runtime.InteropServices;
using Undersoft.SDK.Instant;
using Undersoft.SDK.Instant.Series;
using Undersoft.SDK.Series;
using Undersoft.SDK.Uniques;

namespace Undersoft.SDK.Stocks;

public class Stock<T> : Stock
{
    public Stock() : this(new InstantSeriesCreator<T>().Create()) { }

    public Stock(IInstantSeries instantSeries) : base(new StockOptions<T>(instantSeries)) { }

    public Stock(StockOptions<T> options) : base(options) { }

    public Stock(Action<StockOptions<T>> options)
    {
        var _options = new StockOptions<T>();
        _options.Type = typeof(T);
        options.Invoke(_options);
        _options.BlockSize = Marshal.SizeOf(_options.Type);
        clustersize = _options.ClusterSize;
        sectorsize = _options.SectorSize;
        this.options = _options;
    }
}

public class Stock : IStock
{
    protected StockOptions options;
    protected IInstantSeries instantSeries;
    protected ISeries<ITableStock[]> clusters;
    protected ISeries<IStock> registries;
    protected ushort clustersize;
    protected ushort sectorsize;
    protected int counter;

    public virtual object this[int index, string propertyName]
    {
        get
        {
            var rubric = instantSeries.Rubrics[propertyName];
            return this[index, rubric.RubricId, rubric.RubricType];
        }
        set
        {
            var rubric = instantSeries.Rubrics[propertyName];
            this[index, rubric.RubricId, rubric.RubricType] = value;
        }
    }
    public virtual object this[int index, int fieldId]
    {
        get
        {
            var rubric = instantSeries.Rubrics[fieldId];
            return this[index, fieldId, rubric.RubricType];
        }
        set
        {
            var rubric = instantSeries.Rubrics[fieldId];
            this[index, fieldId, rubric.RubricType] = value;
        }
    }
    public virtual object this[int index, int field, Type type]
    {
        get
        {
            var sector = GetSector((uint)index, out ushort[] zyx);
            return sector[zyx[2], field, type];
        }
        set
        {
            var sector = GetSector((uint)index, out ushort[] zyx);
            sector[zyx[2], field, type] = value;
        }
    }
    public virtual object this[int index]
    {
        get
        {
            return GetSector((uint)index, out ushort[] zyx)[zyx[2]];
        }
        set
        {
            GetSector((uint)index, out ushort[] zyx)[zyx[2]] = value;
        }
    }
    public virtual object this[Uscn serialcode]
    {
        get => Get(serialcode);
        set
        {
            if (!serialcode.GetFlagBit(0))
            {
                var sector = GetSector(serialcode);
                if (sector != null)
                    sector[serialcode.BlockX] = value;
            }
        }
    }

    public Stock() { }

    public Stock(IInstantSeries instantSeries) : this(new StockOptions(instantSeries)) { }

    public Stock(Type type) : this(new InstantSeriesCreator(type, InstantType.Reference).Create()) { }

    public Stock(StockOptions options)
    {
        this.options = options;
        clustersize = options.ClusterSize;
        sectorsize = options.SectorSize;
        instantSeries = options.InstantSeriesCreator;
        if (options.Type != null && options.BlockSize == 0)
            options.BlockSize = Marshal.SizeOf(options.ItemType);
    }

    public Stock(Action<StockOptions> options)
    {
        var _options = new StockOptions();
        options.Invoke(_options);
        if (_options.Type != null && _options.BlockSize == 0)
            _options.BlockSize = Marshal.SizeOf(_options.ItemType);
        clustersize = _options.ClusterSize;
        sectorsize = _options.SectorSize;
        instantSeries = _options.InstantSeriesCreator;
        this.options = _options;
    }

    public void SetInstantSeriesCreator(IInstantSeries instantSeries)
    {
        options.InstantSeriesCreator = instantSeries;
        options.Type = instantSeries.Type;
        options.BlockSize = instantSeries.InstantSize;
    }

    public void Open()
    {
        if (clusters == null)
            clusters = new Registry<ITableStock[]>();

        if (registries == null)
            registries = new Registry<IStock>();

        string[] files = null;
        if (!Directory.Exists(options.StockPath))
            Directory.CreateDirectory(options.StockPath);
        else
            files = Directory.GetFiles(options.StockPath);
        if (files != null)
        {
            foreach (string file in files)
                if (file.Contains($".{options.SectorSuffix}"))
                {
                    string[] ids = file.Split('.');
                    int length = ids.Length;
                    var _options = new SectorOptions(
                        instantSeries,
                        ushort.Parse(ids[length - 3]),
                        ushort.Parse(ids[length - 2])
                    );
                    ITableStock sector = new TableStock(_options);
                    ITableStock[] cluster = GetCluster(sector.ClusterId);
                    cluster[sector.SectorId] = sector;
                }
                else if (file.Contains($".{options.RegistrySuffix}")) { }
        }
        else
        {
            var _options = new SectorOptions(instantSeries, 0, 0);
            ITableStock sector = new TableStock(_options);
            ITableStock[] cluster = GetCluster(sector.ClusterId);
            cluster[sector.SectorId] = sector;
        }
    }

    public void Close()
    {
        if (clusters != null)
            foreach (IStock[] cluster in clusters)
                foreach (IStock sector in cluster)
                    if (sector != null)
                        sector.Close();
        clusters = null;
    }

    public virtual object Get(Uscn serialcode)
    {
        if (!serialcode.GetFlagBit(0))
        {
            var sector = GetSector(serialcode);
            if (sector != null)
                return sector[serialcode.BlockX];
        }
        return null;
    }

    public virtual object Set(IInstant figure)
    {
        var serialcode = figure.Code;
        if (!serialcode.GetFlagBit(0))
        {
            var sector = GetSector(serialcode);
            if (sector != null)
                return sector[serialcode.BlockX] = figure;
        }
        return null;
    }

    public ITableStock GetSector(ulong index, out ushort[] zyx)
    {
        ulong vectorYZ = (uint)(sectorsize * clustersize);
        ulong blockZdiv = index / vectorYZ;
        ulong blockYsub = index - blockZdiv * vectorYZ;
        ulong blockYdiv = blockYsub / sectorsize;
        ulong blockZ = blockZdiv > 0 && index % vectorYZ > 0 ? blockZdiv + 1 : blockZdiv;
        ulong blockY = blockYdiv > 0 && index % sectorsize > 0 ? blockYdiv + 1 : blockYdiv;
        ulong blockX = index % sectorsize;
        zyx = new ushort[] { (ushort)blockZ, (ushort)blockY, (ushort)blockX };
        return GetSector(zyx[0], zyx[1]);
    }

    public ITableStock GetSector(Uscn serialcode)
    {
        return GetSector(serialcode.BlockZ, serialcode.BlockY);
    }

    public ITableStock GetSector(IInstant figure)
    {
        return GetSector(figure.Code);
    }

    private ITableStock[] GetCluster(ushort clusterId)
    {
        if (clusters == null)
            Open();

        if (!clusters.TryGet(clusterId, out ITableStock[] cluster))
        {
            cluster = new ITableStock[options.ClusterSize];
            clusters.Add(clusterId, cluster);
        }
        return cluster;
    }

    private ITableStock GetSector(ushort clusterId, ushort sectorId)
    {
        IStock[] _cluster = GetCluster(clusterId);
        ITableStock _sector = (ITableStock)_cluster?[sectorId];
        if (_sector == null)
        {
            _sector = new TableStock(new SectorOptions(instantSeries, clusterId, sectorId));
            if (!_sector.Exists)
            {
                _sector.ClusterId = clusterId;
                _sector.SectorId = sectorId;
                _sector.ItemSize = instantSeries.InstantSize;
                _sector.ItemCapacity = sectorsize;
                _sector.WriteHeader();
            }
            _cluster[sectorId] = _sector;
        }
        return _sector;
    }
}
