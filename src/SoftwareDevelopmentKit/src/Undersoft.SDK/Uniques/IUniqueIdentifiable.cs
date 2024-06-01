//using System.Collections.Specialized;

//namespace Undersoft.SDK.Uniques
//{
//    using Undersoft.SDK;
//    using Undersoft.SDK.Proxies;

//    public interface IUniqueIdentifiable :
//            IEquatable<IUniqueIdentifiable>,
//            IComparable<IUniqueIdentifiable>,
//            IEquatable<BitVector32>,
//            IEquatable<DateTime>,
//            IEquatable<IUniqueStructure>,
//            IInnerProxy,
//            IOrigin
//    {
//        bool Obsolete { get; set; }
//        byte Priority { get; set; }
//        bool Inactive { get; set; }
//        bool Locked { get; set; }

//        byte Flags { get; set; }

//        void SetFlag(ushort position);
//        void ClearFlag(ushort position);
//        void SetFlag(bool flag, ushort position);
//        bool GetFlag(ushort position);

//        IOrigin Sign();
//        IOrigin Stamp();

//        TEntity Stamp<TEntity>() where TEntity : class, IOrigin;
//        TEntity Sign<TEntity>(TEntity entity, object id) where TEntity : class, IOrigin;

//        byte SetPriority(byte priority);
//        byte ComparePriority(IOrigin entity);
//    }
//}
