namespace Undersoft.SDK.Service.Data.Repository
{
    public interface IRepositoryDetalizer<TEntity> where TEntity : class, IOrigin, IInnerProxy
    {
        bool IsDetalizable<TAttrib>(IInnerProxy contract);

        IEnumerable<object> Detalize(IInnerProxy contract);

        IEnumerable<object> Detalize<TAttrib>(IInnerProxy contract);

        IEnumerable<object> Detalize(IInnerProxy contract, Type targetAttribType);
    }
}