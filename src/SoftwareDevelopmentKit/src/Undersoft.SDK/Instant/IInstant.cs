namespace Undersoft.SDK.Instant
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;
    using Undersoft.SDK.Rubrics;
    using Uniques;


    public interface IInstant : IUnique, INotifyPropertyChanged
    {
        [JsonIgnore]
        [IgnoreDataMember]
        [NotMapped]
        object this[string propertyName] { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        [NotMapped]
        object this[int fieldId] { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        Uscn Code { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        IRubrics Changes { get; set; }
    }

    public interface IValueArray
    {
        [JsonIgnore]
        [IgnoreDataMember]
        [NotMapped]
        object[] ValueArray { get; set; }
    }

    public interface IByteable
    {
        byte[] GetBytes();
    }
}
