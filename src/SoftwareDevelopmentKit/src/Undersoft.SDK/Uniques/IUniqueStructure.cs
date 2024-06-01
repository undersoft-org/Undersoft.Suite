namespace Undersoft.SDK.Uniques
{
    using System.Collections.Specialized;

    public interface IUniqueStructure
        : IIdentifiable,
            IEquatable<BitVector32>,
            IEquatable<DateTime>,
            IEquatable<IUniqueStructure>
    {
        ushort BlockZ { get; set; }

        ushort BlockY { get; set; }

        ushort BlockX { get; set; }

        byte Priority { get; set; }

        byte Flags { get; set; }

        long Time { get; set; }

        ulong GetBlockId();

        ulong SetBlockId(ulong index);

        ulong GetBlockId(ulong vectorZ, ulong vectorY);

        ulong SetBlockId(ulong vectorZ, ulong vectorY, ulong index);
    }
}
