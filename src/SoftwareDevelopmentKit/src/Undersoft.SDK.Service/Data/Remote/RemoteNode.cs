using System.Collections.ObjectModel;

namespace Undersoft.SDK.Service.Data.Remote;

using Undersoft.SDK.Proxies;

public class RemoteNode<TLeft, TRight> : KeyedCollection<long, IRemoteLink<TLeft, TRight>>, IFindable, IRemoteNodeSet<TLeft, TRight> where TLeft : class, IOrigin, IInnerProxy where TRight : class, IOrigin, IInnerProxy
{
    public RemoteNode() : base()
    {

    }
    public RemoteNode(IEnumerable<IRemoteLink<TLeft, TRight>> list)
    {
        foreach (var item in list)
            Add(item);
    }

    protected override long GetKeyForItem(IRemoteLink<TLeft, TRight> item)
    {
        return item.Id == 0 ? item.AutoId() : item.Id;
    }

    public IRemoteLink<TLeft, TRight> Single
    {
        get => this.FirstOrDefault();
    }

    public object this[object key]
    {
        get
        {
            TryGetValue(key.UniqueKey64(), out IRemoteLink<TLeft, TRight> result);
            return result;
        }
        set
        {
            Dictionary[key.UniqueKey64()] = (IRemoteLink<TLeft, TRight>)value;
        }
    }

    protected override void InsertItem(int index, IRemoteLink<TLeft, TRight> item)
    {
        base.InsertItem(index, item);
    }
}