using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ProtoBuf.Grpc.Configuration;
using ProtoBuf.Grpc.Server;
using System;
using System.Linq;
using System.Reflection;

namespace Undersoft.SDK.Service.Server;

using Controller;
using Undersoft.SDK.Service.Server.Controller.Stream;
using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Data.Client.Attributes;

public class GrpcDataServerBuilder<TServiceStore>
    : DataServerBuilder,
        IDataServerBuilder<TServiceStore> where TServiceStore : IDataStore
{
    static bool grpcadded = false;
    IServiceRegistry _registry;

    public GrpcDataServerBuilder(IServiceRegistry registry) : base()
    {
        _registry = registry;
        StoreType = typeof(TServiceStore);
    }

    public void AddControllers()
    {
        Assembly[] asm = AppDomain.CurrentDomain.GetAssemblies();
        var controllerTypes = asm.SelectMany(
                a =>
                    a.GetTypes()
                        .Where(type => type.GetCustomAttribute<StreamDataAttribute>() != null)
                        .ToArray()
            )
            .Where(
                b =>
                    !b.IsAbstract
                    && b.BaseType.IsGenericType
                    && b.BaseType.GenericTypeArguments.Length > 3
            )
            .ToArray();

        foreach (var controllerType in controllerTypes)
        {
            Type contractType = null;

            var genTypes = controllerType.BaseType.GenericTypeArguments;

            if (
                genTypes.Length > 4
                && genTypes[1].IsAssignableTo(StoreType)
                && genTypes[2].IsAssignableTo(StoreType)
            )
                contractType = typeof(IStreamDataController<>).MakeGenericType(new[] { genTypes[4] });
            else if (genTypes.Length > 3)
                if (
                    genTypes[3].IsAssignableTo(typeof(IContract))
                    && genTypes[1].IsAssignableTo(StoreType)
                )
                    contractType = typeof(IStreamDataController<>).MakeGenericType(
                        new[] { genTypes[3] }
                    );
                else
                    continue;

            GrpcDataServerRegistry.ServiceContracts.Add(contractType);

            _registry.AddSingleton(contractType, controllerType);
        }
    }

    public override void Build()
    {
        AddControllers();
        _registry.MergeServices(true);
    }

    protected override string GetRoutes()
    {
        if (StoreType == typeof(IEventStore))
        {
            return StoreRoutes.StreamEventRoute;
        }
        else
        {
            return StoreRoutes.StreamDataRoute;
        }
    }

    public virtual void AddGrpcServicer()
    {
        if (!grpcadded)
        {
            _registry
                .AddCodeFirstGrpc(config =>
                {
                    config.ResponseCompressionLevel = System
                        .IO
                        .Compression
                        .CompressionLevel
                        .Optimal;
                });
                //.AddJsonTranscoding();

            _registry.AddSingleton(
                BinderConfiguration.Create(binder: new GrpcDataServerBinder(_registry))
            );

            _registry.AddCodeFirstGrpcReflection();

            _registry.MergeServices(true);

            grpcadded = true;
        }
    }
}
