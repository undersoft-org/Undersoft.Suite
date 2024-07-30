// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SDK
// *************************************************


using System.Collections;
using Undersoft.SDK.Series;
using Undersoft.SDK.Uniques;

namespace Undersoft.SDK.Updating;

using Invoking;
using Rubrics;
using System.ComponentModel;
using Undersoft.SDK;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Utilities;

public class Updater : IUpdater
{
    protected ProxyGenerator creator;
    protected IProxy source;
    protected Type type;
    protected int counter = 0;
    protected bool traceable;

    public IProxy Source
    {
        get => source;
        set => source = value;
    }

    public Action<Updater, object> ForUpdate { get; set; }

    public IInvoker TraceEvent { get; set; }

    public Updater() { }

    public Updater(object item, Type type, IInvoker traceChanges)
    {
        if (traceChanges != null)
        {
            TraceEvent = traceChanges;
            traceable = true;
        }

        if (type.IsAssignableTo(typeof(IProxy)))
            Combine(item as IProxy);
        else if (type.IsAssignableTo(typeof(IInnerProxy)))
            Combine(item as IInnerProxy);
        else
            Combine(item);
    }

    public Updater(object item, IInvoker traceChanges)
    {
        if (traceChanges != null)
        {
            TraceEvent = traceChanges;
            traceable = true;
        }

        type = item.GetType();

        if (type.IsAssignableTo(typeof(IProxy)))
            Combine(item as IProxy);
        else if (type.IsAssignableTo(typeof(IInnerProxy)))
            Combine(item as IInnerProxy);
        else
            Combine(item);
    }

    public Updater(object item) : this(item, null) { }

    public Updater(IInnerProxy proxy, IInvoker traceChanges)
    {
        if (traceChanges != null)
        {
            TraceEvent = traceChanges;
            traceable = true;
        }

        Combine(proxy);
    }

    public Updater(IProxy proxy, IInvoker traceChanges)
    {
        if (traceChanges != null)
        {
            TraceEvent = traceChanges;
            traceable = true;
        }

        Combine(proxy);
    }

    public Updater(IProxy proxy)
    {
        Combine(proxy);
    }

    protected virtual ProxyGenerator GetGenerator(Type type)
    {
        return creator ??= ProxyGeneratorFactory.CreateGenerator(type);
    }

    protected virtual void Combine(IProxy proxy)
    {
        type = proxy.Target.GetType();
        if (proxy.Rubrics == null)
            proxy.Rubrics = GetGenerator(type).Rubrics;
        source = proxy;
    }

    protected virtual void Combine(IInnerProxy proxy)
    {
        if (type == null)
            type = proxy.GetType();
        source = proxy.Proxy;
    }

    protected virtual void Combine(object item)
    {
        source = GetGenerator(type ??= item.GetType()).Generate(item);
    }

    public object Patch(object item)
    {
        ForUpdate = (o, t) => o.Patch(t);

        IProxy target = item.ToProxy();
        if (item.GetType() != type)
            PatchNotEqualTypes(target);
        else
            PatchEqualTypes(target);

        return item;
    }

    public E Patch<E>(E item)
    {
        ForUpdate = (o, t) => o.Patch(t);

        IProxy target = item.ToProxy();
        if (typeof(E) != type)
            PatchNotEqualTypes(target);
        else
            PatchEqualTypes(target);

        return item;
    }

    public E Patch<E>()
    {
        return Patch(typeof(E).New<E>());
    }

    public object Put(object item)
    {
        ForUpdate = (o, t) => o.Put(t);

        IProxy target = item.ToProxy();
        if (target != null)
        {
            if (item.GetType() != type)
                PutNotEqualTypes(target);
            else
                PutEqualTypes(target);
        }
        return item;
    }

    public E Put<E>(E item)
    {
        ForUpdate = (o, t) => o.Put(t);

        IProxy target = item.ToProxy();
        if (target != null)
        {
            if (typeof(E) != type)
                PutNotEqualTypes(target);
            else
                PutEqualTypes(target);
        }
        return item;
    }

    public E Put<E>()
    {
        return Put(typeof(E).New<E>());
    }

    public object Clone()
    {
        var clone = type.New();
        var _clone = creator.Generate(clone);
        _clone.PutFrom(source);
        return clone;
    }

    protected void PatchEqualTypes(IProxy target)
    {
        counter = 0;
        var _target = target;

        Rubrics
            .Where(r => !r.IsKey && !r.RubricName.Equals("proxy"))
            .ForEach(
                (rubric) =>
                {
                    var targetndex = rubric.RubricId;
                    var originValue = Source[targetndex];
                    var targetValue = _target[targetndex];

                    if (
                        !originValue.NullOrEquals(targetValue)
                        && !RecursiveUpdate(originValue, targetValue, target, rubric, rubric)
                    )
                    {
                        _target[targetndex] = originValue;
                    }
                }
            );
    }

    protected void PatchNotEqualTypes(IProxy target)
    {
        counter = 0;
        var _target = target;

        Rubrics
            .Where(r => !r.IsKey && !r.RubricName.Equals("proxy"))
            .ForEach(
                (originRubric) =>
                {
                    var name = originRubric.Name;
                    if (_target.Rubrics.TryGet(name, out MemberRubric targetRubric))
                    {
                        var originValue = Source[originRubric.RubricId];
                        var targetIndex = targetRubric.RubricId;
                        var targetValue = _target[targetIndex];

                        if (
                            !originValue.NullOrEquals(targetValue)
                            && !RecursiveUpdate(
                                originValue,
                                targetValue,
                                target,
                                originRubric,
                                targetRubric
                            )
                        )
                        {
                            if (targetRubric.RubricType.IsAssignableTo(originRubric.RubricType))
                            {
                                _target[targetIndex] = originValue;
                            }
                        }
                    }
                }
            );
    }

    protected void PutEqualTypes(IProxy target)
    {
        counter = 0;
        var _target = target;

        Rubrics
            .Where(r => !r.RubricName.Equals("proxy"))
            .ForEach(
                (rubric) =>
                {
                    var targetndex = rubric.RubricId;
                    var originValue = Source[targetndex];
                    var targetValue = _target[targetndex];

                    if (
                        originValue != null
                        && !RecursiveUpdate(originValue, targetValue, target, rubric, rubric)
                    )
                    {
                        _target[targetndex] = originValue;
                    }
                }
            );
    }

    protected void PutNotEqualTypes(IProxy target)
    {
        counter = 0;
        var _target = target;

        Rubrics
            .Where(r => !r.RubricName.Equals("proxy"))
            .ForEach(
                (originRubric) =>
                {
                    var name = originRubric.Name;
                    if (_target.Rubrics.TryGet(name, out MemberRubric targetRubric))
                    {
                        var originValue = Source[originRubric.RubricId];
                        if (originValue == null)
                            return;

                        var targetIndex = targetRubric.RubricId;
                        var targetValue = _target[targetIndex];

                        if (
                            originValue != null
                            && !RecursiveUpdate(
                                originValue,
                                targetValue,
                                target,
                                originRubric,
                                targetRubric
                            )
                        )
                        {
                            if (targetRubric.RubricType.IsAssignableTo(originRubric.RubricType))
                            {
                                _target[targetIndex] = originValue;
                            }
                        }
                    }
                }
            );
    }

    private bool RecursiveUpdate(
        object originValue,
        object targetValue,
        IProxy target,
        IRubric originRubric,
        IRubric targetRubric
    )
    {
        var originType = originRubric.RubricType;
        var targetType = targetRubric.RubricType;

        if (originType.IsValueType || originType == typeof(string) || originType.IsArray)
            return false;

        if (targetValue == null)
        {
            target[targetRubric.RubricId] = targetValue = targetType.New();

            if (traceable)
                targetValue = TraceEvent.Invoke(
                    target.Target,
                    targetRubric.RubricName,
                    null,
                    targetType
                );
        }

        if (originValue == null)
        {
            originValue = originType.New();
        }

        if (originType.IsAssignableTo(typeof(IEnumerable)))
        {
            IEnumerable originItems = (IEnumerable)originValue;
            var originItemType = originType.GetEnumerableElementType();
            if (originItemType == null || !originItemType.IsValueType)
            {
                if (targetType.IsAssignableTo(typeof(IEnumerable)))
                {
                    IEnumerable targetItems = (IEnumerable)targetValue;
                    var targetItemType = targetType.GetEnumerableElementType();
                    if (targetItemType == null || !targetItemType.IsValueType)
                    {
                        if (
                            !targetType.IsAssignableTo(typeof(IFindable))
                            || !originItemType.IsAssignableTo(typeof(IIdentifiable))
                        )
                            GreedyLookup(originItems, targetItems, target, originItemType, targetItemType);
                        else
                        {
                            IFindable targetFindable = (IFindable)targetValue;

                            foreach (var originItem in originItems)
                            {
                                var targetItem = targetFindable[originItem];
                                if (targetItem != null)
                                {
                                    if (traceable)
                                        targetItem = TraceEvent.Invoke(
                                            target.Target,
                                            targetRubric.RubricName,
                                            targetItem,
                                            null
                                        );

                                    ForUpdate(new Updater(originItem, originType, TraceEvent), targetItem);
                                }
                                else if (originItemType != targetItemType)
                                {
                                    targetItem = targetItemType.New();

                                    ((IIdentifiable)targetItem).Id = ((IIdentifiable)originItem).Id;

                                    originItem.PatchTo(targetItem, TraceEvent);

                                    if (traceable)
                                        targetItem = TraceEvent.Invoke(
                                             target.Target,
                                            targetRubric.RubricName,
                                            targetItem,
                                            null
                                        );

                                    ((IList)targetItems).Add(targetItem);
                                }
                                else
                                {
                                    if (traceable)
                                        targetItem = TraceEvent.Invoke(
                                            target.Target,
                                            targetRubric.RubricName,
                                            originItem,
                                            null
                                        );

                                    ((IList)targetItems).Add(originItem);
                                }
                            }

                            return true;
                        }
                    }
                }
            }
        }

        if (traceable)
            targetValue = TraceEvent.Invoke(
                target.Target,
                targetRubric.RubricName,
                targetValue,
                targetType
            );

        ForUpdate(new Updater(originValue, originType, TraceEvent), targetValue);

        return false;
    }

    private bool GreedyLookup(
        IEnumerable originItems,
        IEnumerable targetItems,
        object target,
        Type originItemType,
        Type targetItemType
    )
    {
        if (
            !originItemType.IsAssignableTo(typeof(IIdentifiable))
            || !targetItemType.IsAssignableTo(typeof(IIdentifiable))
        )
            return false;

        foreach (var originItem in originItems)
        {
            bool founded = false;
            foreach (var targetItem in targetItems)
            {
                if (((IIdentifiable)originItem).Id == ((IIdentifiable)targetItem).Id)
                {
                    var _targetItem = targetItem;

                    if (traceable)
                        _targetItem = TraceEvent.Invoke(target, "Id", _targetItem, null, null);

                    ForUpdate(new Updater(originItem, TraceEvent), _targetItem);

                    founded = true;
                    break;
                }
            }

            if (!founded)
            {
                object targetItem = null;
                if (originItemType != targetItemType)
                {
                    targetItem = targetItemType.New();
                    ((IIdentifiable)targetItem).Id = ((IIdentifiable)originItem).Id;
                    if (traceable)
                        targetItem = TraceEvent.Invoke(target, "Id", targetItem, null);
                    ((IList)targetItems).Add(originItem.PatchTo(targetItem, TraceEvent));
                }
                else
                {
                    if (traceable)
                        targetItem = TraceEvent.Invoke(target, "Id", originItem, null);
                    ((IList)targetItems).Add(originItem);
                }
            }
        }

        return true;
    }

    public object ShallowPatch(object item)
    {
        IProxy target = item.ToProxy();
        if (item.GetType() != type)
            ShallowPatchNotEqualTypes(target);
        else
            ShallowPatchEqualTypes(target);

        return item;
    }

    public E ShallowPatch<E>(E item) where E : class
    {
        IProxy target = item.ToProxy();
        if (typeof(E) != type)
            ShallowPatchNotEqualTypes(target);
        else
            ShallowPatchEqualTypes(target);

        return item;
    }

    public E ShallowPatch<E>() where E : class
    {
        return ShallowPatch(typeof(E).New<E>());
    }

    public object ShallowPut(object item)
    {
        IProxy target = item.ToProxy();
        if (target != null)
        {
            if (item.GetType() != type)
                ShallowPutNotEqualTypes(target);
            else
                ShallowPutEqualTypes(target);
        }
        return item;
    }

    public E ShallowPut<E>(E item) where E : class
    {
        IProxy target = item.ToProxy();
        if (target != null)
        {
            if (typeof(E) != type)
                ShallowPutNotEqualTypes(target);
            else
                ShallowPutEqualTypes(target);
        }
        return item;
    }

    public E ShallowPut<E>() where E : class
    {
        return ShallowPut(typeof(E).New<E>());
    }

    protected void ShallowPatchEqualTypes(IProxy target)
    {
        counter = 0;
        var _target = target;

        Rubrics
            .Where(r => !r.IsKey && !r.RubricName.Equals("proxy"))
            .ForEach(
                (rubric) =>
                {
                    var targetndex = rubric.RubricId;
                    var originValue = Source[targetndex];
                    var targetValue = _target[targetndex];

                    if (!originValue.NullOrEquals(targetValue))
                    {
                        _target[targetndex] = originValue;
                    }
                }
            );
    }

    protected void ShallowPatchNotEqualTypes(IProxy target)
    {
        counter = 0;
        var _target = target;

        Rubrics
            .Where(r => !r.IsKey && !r.RubricName.Equals("proxy"))
            .ForEach(
                (originRubric) =>
                {
                    var name = originRubric.Name;
                    if (_target.Rubrics.TryGet(name, out MemberRubric targetRubric))
                    {
                        var originValue = Source[originRubric.RubricId];
                        var targetIndex = targetRubric.RubricId;
                        var targetValue = _target[targetIndex];

                        if (!originValue.NullOrEquals(targetValue))
                        {
                            if (targetRubric.RubricType.IsAssignableTo(originRubric.RubricType))
                            {
                                _target[targetIndex] = originValue;
                            }
                        }
                    }
                }
            );
    }

    protected void ShallowPutEqualTypes(IProxy target)
    {
        counter = 0;
        var _target = target;

        Rubrics
            .Where(r => !r.IsKey && !r.RubricName.Equals("proxy"))
            .ForEach(
                (rubric) =>
                {
                    var targetndex = rubric.RubricId;
                    var originValue = Source[targetndex];

                    if (originValue != null)
                    {
                        _target[targetndex] = originValue;
                    }
                }
            );
    }

    protected void ShallowPutNotEqualTypes(IProxy target)
    {
        counter = 0;
        var _target = target;

        Rubrics
            .Where(r => !r.IsKey && !r.RubricName.Equals("proxy"))
            .ForEach(
                (originRubric) =>
                {
                    var name = originRubric.Name;
                    if (_target.Rubrics.TryGet(name, out MemberRubric targetRubric))
                    {
                        var originValue = Source[originRubric.RubricId];
                        if (originValue == null)
                            return;

                        var targetIndex = targetRubric.RubricId;

                        if (originValue != null)
                        {
                            if (targetRubric.RubricType.IsAssignableTo(originRubric.RubricType))
                            {
                                _target[targetIndex] = originValue;
                            }
                        }
                    }
                }
            );
    }

    private static HashSet<string> excludedRubrics;

    public event PropertyChangedEventHandler PropertyChanged;

    public HashSet<string> ExcludedRubrics
    {
        get => excludedRubrics ??= new HashSet<string>(new string[] { "proxy", "valuearray" });
    }

    public bool Equals(IUnique other)
    {
        return source.Equals(other);
    }

    public int CompareTo(IUnique other)
    {
        return source.CompareTo(other);
    }

    public long Id
    {
        get => source.Id;
        set => source.Id = value;
    }

    public long TypeId
    {
        get => source.TypeId;
        set => source.TypeId = value;
    }

    public object this[string propertyName]
    {
        get => source[propertyName];
        set => source[propertyName] = value;
    }

    public object this[int fieldId]
    {
        get => source[fieldId];
        set => source[fieldId] = value;
    }

    public Uscn Code
    {
        get => source.Code;
        set => source.Code = value;
    }

    public IRubrics Rubrics
    {
        get => source.Rubrics;
        set => source.Rubrics = value;
    }

    public IRubrics Changes
    {
        get => source.Changes;
        set => source.Changes = value;
    }

    public object Target
    {
        get => source.Target;
        set => source.Target = value;
    }
}
