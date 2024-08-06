using System.Runtime.Serialization;
using Undersoft.GDC.Domain.Entities.Enums;
using Undersoft.SDK.Serialization;
using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SDK.Service.Data.Object.Detail;

namespace Undersoft.GDC.Service.Contracts;

[DataContract]
public class Detail : ObjectDetail<Detail, DetailKind>, IJsonDocumentSerializable, IDetail, IContract
{
    public Detail() : base() { }

    public Detail(DetailKind kind) : base(kind) { }
}
