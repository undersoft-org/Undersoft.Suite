using MediatR;

namespace Undersoft.SDK.Service;

using Invoking;
using Microsoft.Extensions.DependencyInjection;
using Undersoft.SDK.Service.Data.Repository.Client;
using Undersoft.SDK.Service.Data.Repository.Source;

public class Servicer : ServiceManager, IServicer, IMediator
{
    new bool disposedValue;
    protected IMediator mediator;

    public Servicer() : base() { }

    public Servicer(IServiceManager serviceManager) : base(serviceManager) { }

    protected IMediator Mediator => mediator ??= Session.ServiceProvider.GetService<IMediator>();

    public IAsyncEnumerable<TResponse> CreateStream<TResponse>(
        IStreamRequest<TResponse> request,
        CancellationToken cancellationToken = default
    )
    {
        using (var session = CreateSession())
        {
            return session.ServiceProvider
                 .GetService<IMediator>()
                 .CreateStream(request, cancellationToken);
        }
    }

    public IAsyncEnumerable<object> CreateStream(
        object request,
        CancellationToken cancellationToken = default
    )
    {
        using (var session = CreateSession())
        {
            return session.ServiceProvider
                 .GetService<IMediator>()
                 .CreateStream(request, cancellationToken);
        }
    }

    public Lazy<R> LazyServe<T, R>(Func<T, R> function)
        where T : class
        where R : class
    {
        return new Lazy<R>(function.Invoke(base.GetService<T>()));
    }

    public Task<R> Run<T, R>(Func<T, Task<R>> function) where T : class
    {
        return Task.Run(async () => await function.Invoke(base.GetService<T>()));
    }

    public Task Run<T>(Func<T, Task> function) where T : class
    {
        return Task.Run(async () => await function.Invoke(base.GetService<T>()));
    }

    public Task<object> Run<T>(string methodname, params object[] parameters) where T : class
    {
        return new Invoker(base.GetService<T>(), methodname).InvokeAsync(parameters);
    }

    public Task<R> Run<T, R>(string methodname, params object[] parameters) where T : class where R : class
    {
        Invoker deputy = new Invoker(
            base.GetService<T>(),
            methodname,
            parameters.ForEach(p => p.GetType()).Commit()
        );
        return deputy.InvokeAsync<R>(parameters);
    }

    public Task<object> Run<T>(Arguments arguments) where T : class
    {
        Invoker deputy = new Invoker<T>(base.GetService<T>(), arguments);
        return deputy.InvokeAsync(arguments);
    }

    public Task<R> Run<T, R>(Arguments arguments) where T : class where R : class
    {
        Invoker deputy = new Invoker<T>(base.GetService<T>(), arguments);
        return deputy.InvokeAsync<R>(arguments);
    }

    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        using (var servicer = new Servicer(Manager))
        {
            return await servicer.Mediator.Send(request, cancellationToken);
        }
    }

    public async Task<object> Send(object request, CancellationToken cancellationToken = default)
    {
        using (var servicer = new Servicer(Manager))
        {
            return await servicer.Mediator.Send(request, cancellationToken);
        }
    }

    public async Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest
    {
        using (var servicer = new Servicer(Manager))
        {
            await servicer.Mediator.Send(request, cancellationToken);
        }
    }

    public async Task Publish(object notification, CancellationToken cancellationToken = default)
    {
        using (var servicer = new Servicer(Manager))
        {
            await servicer.Mediator.Publish(notification, cancellationToken);
        }
    }

    public async Task Publish<TNotification>(
        TNotification notification,
        CancellationToken cancellationToken = default
    ) where TNotification : INotification
    {
        using (var servicer = new Servicer(Manager))
        {
            await servicer.Mediator.Publish(notification, cancellationToken);
        }
    }

    public async Task<TResponse> Report<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        return await Mediator.Send(request, cancellationToken);
    }

    public async Task<object> Report(object request, CancellationToken cancellationToken = default)
    {
        return await Mediator.Send(request, cancellationToken);
    }

    public async Task<TResponse> Entry<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        return await Mediator.Send(request, cancellationToken);
    }

    public async Task<object> Entry(object request, CancellationToken cancellationToken = default)
    {
        return await Mediator.Send(request, cancellationToken);
    }

    public async Task<TResponse> Perform<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        return await Send(request, cancellationToken);
    }

    public async Task<object> Perform(object request, CancellationToken cancellationToken = default)
    {
        return await Send(request, cancellationToken);
    }

    public async Task<R> Serve<T, R>(Func<T, Task<R>> function) where T : class
    {
        return await Task.Run(async () =>
        {
            using (var session = CreateSession())
            {
                return await function.Invoke(session.ServiceProvider.GetService<T>());
            }
        });
    }

    public async Task Serve<T>(Func<T, Task> function) where T : class
    {
        await Task.Run(async () =>
        {
            using (var session = CreateSession())
            {
                await function.Invoke(session.ServiceProvider.GetService<T>());
            }
        });
    }

    public async Task Serve<T>(string methodname, params object[] parameters) where T : class
    {
        await Task.Run(async () =>
        {
            using (var session = CreateSession())
            {
                return await new Invoker(
                    session.ServiceProvider.GetService<T>(),
                    methodname
                ).InvokeAsync(parameters);
            }
        });
    }

    public async Task<R> Serve<T, R>(string methodname, params object[] parameters) where T : class where R : class
    {
        return await Task.Run(async () =>
        {
            using (var session = CreateSession())
            {
                return await new Invoker(
                    session.ServiceProvider.GetService<T>(),
                    methodname
                ).InvokeAsync<R>(parameters);
            }
        });
    }

    public async Task Serve<T>(string methodname, Arguments arguments) where T : class
    {
        await Task.Run(async () =>
        {
            using (var session = CreateSession())
            {
                return await new Invoker(
                    session.ServiceProvider.GetService<T>(),
                    methodname,
                    arguments.TypeArray
                ).InvokeAsync(arguments);
            }
        });
    }

    public async Task<R> Serve<T, R>(string methodname, Arguments arguments) where T : class where R : class
    {
        return await Task.Run(async () =>
        {
            using (var session = CreateSession())
            {
                return await new Invoker(
                    session.ServiceProvider.GetService<T>(),
                    methodname,
                    arguments.TypeArray
                ).InvokeAsync<R>(arguments);
            }
        });
    }

    public async Task Save(bool asTransaction = false)
    {
        await SaveStores(true);
        await SaveClients(true);
    }

    public async Task<int> SaveClient(IRepositoryClient client, bool asTransaction = false)
    {
        return await client.Save(asTransaction);
    }

    public async Task<int> SaveClients(bool asTransaction = false)
    {
        int changes = 0;
        for (int i = 0; i < Clients.Count; i++)
        {
            changes += await SaveClient(Clients[i], asTransaction);
        }

        return changes;
    }

    public async Task<int> SaveStore(IRepositorySource source, bool asTransaction = false)
    {
        return await source.Save(asTransaction);
    }

    public async Task<int> SaveStores(bool asTransaction = false)
    {
        int changes = 0;
        for (int i = 0; i < Sources.Count; i++)
        {
            changes += await SaveStore(Sources[i], asTransaction);
        }

        return changes;
    }

    public override async ValueTask DisposeAsyncCore()
    {
        await base.DisposeAsyncCore();
    }

    protected override void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                base.Dispose(true);
            }
            disposedValue = true;
        }
    }


}
