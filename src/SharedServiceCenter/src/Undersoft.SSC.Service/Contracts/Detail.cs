using System.Runtime.Serialization;
using Undersoft.SDK.Serialization;
using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SDK.Service.Data.Object.Detail;
using Undersoft.SSC.Domain.Entities.Enums;

namespace Undersoft.SSC.Service.Contracts;

[DataContract]
public class Detail : ObjectDetail<Detail, DetailKind>, ISerializableJsonDocument, IDetail, IContract
{
    public Detail() : base() { }

    public Detail(DetailKind kind) : base(kind) { }


}
