using Undersoft.SDK.Service.Data.Object.Detail;

namespace Undersoft.SDK.Service.Data.Repository
{
    public interface IRepositoryDocument<TEntity> where TEntity : class, IOrigin, IInnerProxy
    {
        bool IsDocumentDeserializable<TAttrib>(IInnerProxy contract);

        IEnumerable<object> DeserializeDocuments(IInnerProxy contract);

        IEnumerable<object> DeserializeDocuments<TAttrib>(IInnerProxy contract);

        IEnumerable<object> DeserializeDocuments(IInnerProxy contract, Type targetAttribType);

        bool IsDocumentSerializable<TAttrib>(IInnerProxy contract);

        bool IsDocumentSerializable(IInnerProxy contract, Type sourceAttribType);

        IEnumerable<IDetail> SerializeDocuments(IInnerProxy contract);

        IEnumerable<IDetail> SerializeDocuments<TSourceAttrib, TTargetAttrib>(IInnerProxy contract);

        IEnumerable<IDetail> SerializeDocuments(
        IInnerProxy contract,
        Type sourceAttribType,
        Type targetAttribType
        );
    }
}