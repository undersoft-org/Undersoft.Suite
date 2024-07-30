// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SDK
// *************************************************


namespace Undersoft.SDK.Updating;

using Invoking;
using Undersoft.SDK.Utilities;

public class Updater<T> : Updater, IUpdater<T>
{
    public Updater() : base(typeof(T).New<T>()) { }

    public Updater(T item) : base(item) { }

    public Updater(T item, IInvoker traceChanges) : base(item, traceChanges) { }

    public T Patch(T item)
    {
        base.Patch(item);
        return item;
    }

    public T PatchFrom(T source)
    {
        source.PatchTo(this.source);
        return (T)base.source.Target;
    }

    public T Put(T item)
    {
        base.Put(item);
        return item;
    }

    public T PutFrom(T source)
    {
        source.PutFrom(this.source);
        return (T)base.source.Target;
    }

    public new T Clone()
    {
        var clone = typeof(T).New<T>();
        var _clone = creator.Generate(clone);
        _clone.PutFrom(Devisor);
        return clone;
    }

    public T Devisor
    {
        get => (T)source.Target;
        set => source.Target = value;
    }
}
