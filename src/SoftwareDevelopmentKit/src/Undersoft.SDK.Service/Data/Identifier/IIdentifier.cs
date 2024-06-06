using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Undersoft.SDK.Service.Data.Identifier;

public interface IIdentifier<TEntity> : IIdentifier
{
    [JsonIgnore]
    [IgnoreDataMember]
    TEntity Object { get; set; }
}

public interface IIdentifier : IInnerProxy
{
    long ObjectId { get; set; }

    IdKind Kind { get; set; }

    string Name { get; set; }

    string Value { get; set; }

    long Key { get; set; }
}