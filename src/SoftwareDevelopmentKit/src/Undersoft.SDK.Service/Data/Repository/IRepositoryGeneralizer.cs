using Undersoft.SDK.Service.Data.Object.Detail;

namespace Undersoft.SDK.Service.Data.Repository
{
    public interface IRepositoryGeneralizer<TEntity> where TEntity : class, IOrigin, IInnerProxy
    {
        bool IsGeneralizable<TAttrib>(IInnerProxy contract);

        bool IsGeneralizable(IInnerProxy contract, Type sourceAttribType);

        IEnumerable<IDetail> Generalize(IInnerProxy contract);

        IEnumerable<IDetail> Generalize<TSourceAttrib, TTargetAttrib>(IInnerProxy contract);

        IEnumerable<IDetail> Generalize(
        IInnerProxy contract,
        Type sourceAttribType,
        Type targetAttribType
        );

        IEnumerable<object> Detalize<TAttrib>(IInnerProxy contract);

        IEnumerable<object> Detalize(IInnerProxy contract, Type targetAttribType);
    }
}