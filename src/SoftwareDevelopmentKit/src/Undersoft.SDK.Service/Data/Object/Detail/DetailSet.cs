using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Undersoft.SDK.Service.Data.Object.Detail;

public class DetailSet<TDto> : KeyedCollection<string, TDto> where TDto : class, IDetail
{
    public DetailSet() { }

    public DetailSet(IEnumerable<TDto> list) { list.ForEach(item => base.Add(item)); }

    protected override string GetKeyForItem(TDto item)
    {
        if (item.Id == 0)
            item.AutoId();
        return item.Name;
    }
    [NotMapped]
    [JsonIgnore]
    [IgnoreDataMember]
    public TDto Single
    {
        get => this.EnsureOne();
    }
    [NotMapped]
    [JsonIgnore]
    [IgnoreDataMember]
    public new TDto this[string key]
    {
        get
        {
            return this[key];
        }
        set
        {
            if (
                Dictionary != null
                && value != null
            )
                Dictionary[key] = value;
        }
    }
}
