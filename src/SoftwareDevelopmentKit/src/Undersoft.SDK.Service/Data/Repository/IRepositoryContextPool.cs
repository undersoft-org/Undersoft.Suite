namespace Undersoft.SDK.Service.Data.Repository
{
    public interface IRepositoryContextPool : ISeries<IRepositoryContext>, IRepositoryContextFactory
    {
        int PoolSize { get; set; }

        void CreatePool();

        IRepositoryContext Rent();

        void Return();

        Task ReturnAsync(CancellationToken cancellationToken = default);
    }

    public interface IRepositoryContextPool<TContext> : IRepositoryContextPool, IRepositoryContextFactory<TContext>
        where TContext : class
    { }

}