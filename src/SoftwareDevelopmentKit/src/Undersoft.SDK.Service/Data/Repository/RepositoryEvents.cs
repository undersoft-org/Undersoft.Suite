using System;
using System.Linq;
using Undersoft.SDK.Series;
using System.Threading.Tasks;

namespace Undersoft.SDK.Service.Data.Repository;

using Invoking;

public class EventInvoker : Invoker, IInvoker
{
    public EventInvoker(StateOn eventon) : base()
    {
        StateOn = eventon;
        RepositoryEvents.Registry.Put("On" + eventon.ToString(), this);
    }
    public EventInvoker(string eventon) : base()
    {
        StateOn = Enum.Parse<StateOn>(eventon);
        RepositoryEvents.Registry.Put("On" + eventon, this);
    }

    public override Task Fire(params object[] parameters)
    {
        if (RepositoryEvents.Registry.TryGet("On" + StateOn.ToString(), out ISeriesItem<IInvoker> card))
            return card.ForEachAsync((c) => { c.Fire(true, TargetObject, parameters); });
        return null;
    }

    public override Task Invoke(params object[] parameters)
    {
        if (RepositoryEvents.Registry.TryGet("On" + StateOn.ToString(), out ISeriesItem<IInvoker> card))
            return card.ForEachAsync((c) => { c.Fire(true, TargetObject, parameters); });
        return null;
    }
}

public class RepositoryEvents : IRepositoryEvents
{
    public static ISeries<IInvoker> Registry;

    static RepositoryEvents()
    {
        Registry = new Registry<IInvoker>(true);
        var states = Enum.GetNames<StateOn>();
        var init = new RepositoryEvents();
        states.ForEach((s) => Registry.Add(s.ToString(),
            new EventInvoker(s))).CommitAsync();
    }

    private IInvoker GetPublisher(string eventonName)
    {
        var deputy = Registry.Get(eventonName);
        deputy.TargetObject = this;
        return deputy;
    }

    public IInvoker OnAdding
    {
        get => GetPublisher(nameof(StateOn.Adding));
        set => Registry.Add(nameof(OnAdding), value);
    }
    public IInvoker OnAddComplete
    {
        get => GetPublisher(nameof(StateOn.AddComplete));
        set { Registry.Add(nameof(OnAddComplete), value); }
    }

    public IInvoker OnGetting
    {
        get => GetPublisher(nameof(StateOn.Getting));
        set { Registry.Add(nameof(OnGetting), value); }
    }
    public IInvoker OnGetComplete
    {
        get => GetPublisher(nameof(StateOn.GetComplete));
        set { Registry.Add(nameof(OnGetComplete), value); }
    }

    public IInvoker OnPatching
    {
        get => GetPublisher(nameof(StateOn.Patching));
        set { Registry.Add(nameof(OnPatching), value); }
    }
    public IInvoker OnPatchComplete
    {
        get => GetPublisher(nameof(StateOn.PatchComplete));
        set { Registry.Add(nameof(OnPatchComplete), value); }
    }

    public IInvoker OnUpsert
    {
        get => GetPublisher(nameof(StateOn.Upsert));
        set { Registry.Add(nameof(OnUpsert), value); }
    }
    public IInvoker OnUpsertComplete
    {
        get => GetPublisher(nameof(StateOn.UpsertComplete));
        set { Registry.Add(nameof(OnUpsertComplete), value); }
    }

    public IInvoker OnSetting
    {
        get => GetPublisher(nameof(StateOn.Setting));
        set { Registry.Add(nameof(OnSetting), value); }
    }
    public IInvoker OnSetComplete
    {
        get => GetPublisher(nameof(StateOn.SetComplete));
        set { Registry.Add(nameof(OnSetComplete), value); }
    }

    public IInvoker OnDeleting
    {
        get => GetPublisher(nameof(StateOn.Deleting));
        set { Registry.Add(nameof(OnDeleting), value); }
    }
    public IInvoker OnDeleteComplete
    {
        get => GetPublisher(nameof(StateOn.DeleteComplete));
        set { Registry.Add(nameof(OnDeleteComplete), value); }
    }

    public IInvoker OnSaving
    {
        get => GetPublisher(nameof(StateOn.Saving));
        set { Registry.Add(nameof(OnSaving), value); }
    }
    public IInvoker OnSaveComplete
    {
        get => GetPublisher(nameof(StateOn.SaveComplete));
        set { Registry.Add(nameof(OnSaveComplete), value); }
    }

    public IInvoker OnFiltering
    {
        get => GetPublisher(nameof(StateOn.Filtering));
        set { Registry.Add(nameof(OnFiltering), value); }
    }
    public IInvoker OnFilterComplete
    {
        get => GetPublisher(nameof(StateOn.FilterComplete));
        set { Registry.Add(nameof(OnFilterComplete), value); }
    }

    public IInvoker OnFinding
    {
        get => GetPublisher(nameof(StateOn.Finding));
        set { Registry.Add(nameof(OnFinding), value); }
    }
    public IInvoker OnFindComplete
    {
        get => GetPublisher(nameof(StateOn.FindComplete));
        set { Registry.Add(nameof(OnFindComplete), value); }
    }

    public IInvoker OnMapping
    {
        get => GetPublisher(nameof(StateOn.Mapping));
        set { Registry.Add(nameof(OnMapping), value); }
    }
    public IInvoker OnMapComplete
    {
        get => GetPublisher(nameof(StateOn.MapComplete));
        set { Registry.Add(nameof(OnMapComplete), value); }
    }

    public IInvoker OnExist
    {
        get => GetPublisher(nameof(StateOn.Exist));
        set { Registry.Add(nameof(OnExist), value); }
    }
    public IInvoker OnExistComplete
    {
        get => GetPublisher(nameof(StateOn.ExistComplete));
        set { Registry.Add(nameof(OnExistComplete), value); }
    }

    public IInvoker OnNonExist
    {
        get => GetPublisher(nameof(StateOn.NonExist));
        set { Registry.Add(nameof(OnNonExist), value); }
    }
    public IInvoker OnNonExistComplete
    {
        get => GetPublisher(nameof(StateOn.NonExistComplete));
        set { Registry.Add(nameof(OnNonExistComplete), value); }
    }

    public IInvoker OnValidating
    {
        get => GetPublisher(nameof(StateOn.Validating));
        set { Registry.Add(nameof(OnValidating), value); }
    }
    public IInvoker OnValidateComplete
    {
        get => GetPublisher(nameof(StateOn.ValidateComplete));
        set { Registry.Add(nameof(OnValidateComplete), value); }
    }
}
