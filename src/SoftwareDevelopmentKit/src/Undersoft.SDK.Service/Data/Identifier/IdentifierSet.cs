using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.SDK.Service.Data.Identifier;

public class IdentifierSet<TObject> : KeyedCollection<long, Identifier<TObject>>, IFindable<Identifier<TObject>>, IIdentifiers, IIdentifiers<TObject> where TObject : IDataObject
{
    [NotMapped]
    [JsonIgnore]
    [IgnoreDataMember]
    private static ISeries<Identifier<TObject>> _identifiers = new Registry<Identifier<TObject>>(true);

    [NotMapped]
    [JsonIgnore]
    [IgnoreDataMember]
    public Identifier<TObject> this[IdKind kind]
    {
        get => this.FirstOrDefault(o => o.Kind == kind);
        set => ((IIdentifier<TObject>)value).PutTo(this.FirstOrDefault(o => o.Kind == kind));
    }

    [NotMapped]
    [JsonIgnore]
    [IgnoreDataMember]
    object IFindable.this[object key]
    {
        get => _identifiers[key];
        set => ((IFindable)_identifiers)[key] = value;
    }

    public IdentifierSet() : base()
    {
    }

    public Identifier<TObject> Search(object id)
    {
        return _identifiers[id];
    }

    [NotMapped]
    [JsonIgnore]
    [IgnoreDataMember]
    public Identifier<TObject> this[object id]
    {
        get => _identifiers[id];
        set => _identifiers[id] = value;
    }

    [NotMapped]
    [JsonIgnore]
    [IgnoreDataMember]
    public new Identifier<TObject> this[int id]
    {
        get => _identifiers[id];
        set => _identifiers[id] = value;
    }

    protected override long GetKeyForItem(Identifier<TObject> item)
    {
        if (item.Id == 0)
            item.AutoId();

        return item.Id;
    }

    [NotMapped]
    [JsonIgnore]
    [IgnoreDataMember]
    protected new KeyedCollection<long, Identifier<TObject>> Dictionary { get => this; }

    protected override void ClearItems()
    {
        _identifiers.Clear();
        base.ClearItems();
    }

    protected override void SetItem(int index, Identifier<TObject> item)
    {
        _identifiers[index] = item;
        base.SetItem(index, item);
    }

    protected override void InsertItem(int index, Identifier<TObject> item)
    {
        _identifiers.Put(item);
        base.InsertItem(index, item);
    }

    protected override void RemoveItem(int index)
    {
        _identifiers.Remove(this[index].Key);
        Remove(index);
    }


    public void UpdateIdentifiers(TObject model)
    {
        var rubrics = ((IInnerProxy)model).Proxy.Rubrics;
        rubrics.KeyRubrics.ForEach(r =>
        {
            Add(
                new Identifier<TObject>()
                {
                    ObjectId = model.Id,
                    Kind = Enum.TryParse(r.Name, true, out IdKind kind)
                        ? kind
                        : IdKind.None,
                    Value = this[r.RubricId].ToString(),
                    Name = r.Name
                }
            );
        });
    }
}

public interface IIdentifiers
{
}

public interface IIdentifiers<TObject> : IIdentifiers where TObject : IDataObject
{
    Identifier<TObject> this[int id] { get; set; }
    Identifier<TObject> this[object id] { get; set; }
    Identifier<TObject> this[IdKind kind] { get; set; }

    Identifier<TObject> Search(object id);
}
