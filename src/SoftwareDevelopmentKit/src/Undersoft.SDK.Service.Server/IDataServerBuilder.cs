using Microsoft.EntityFrameworkCore;
using System;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SDK.Service.Server;

public interface IDataServerBuilder<TStore> : IDataServerBuilder where TStore : IDataStore { }

public interface IDataServerBuilder : IDisposable, IAsyncDisposable
{
    string RoutePrefix { get; set; }

    int PageLimit { get; set; }

    void Build();

    IDataServerBuilder AddDataServices<TContext>() where TContext : DbContext;

    IDataServerBuilder AddInvocations<TAuth>() where TAuth : class;
}
