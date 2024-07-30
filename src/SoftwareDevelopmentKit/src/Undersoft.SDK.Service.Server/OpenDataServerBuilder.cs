using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Batch;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using ServiceStack;
using System.Collections;
using System.Reflection;
using Undersoft.SDK.Service.Data.Client.Attributes;
using Undersoft.SDK.Service.Data.Model;

namespace Undersoft.SDK.Service.Server;

public class OpenDataServerBuilder<TStore> : DataServerBuilder, IDataServerBuilder<TStore>
    where TStore : IDataStore
{
    IServiceRegistry _registry;
    protected ODataConventionModelBuilder odataBuilder;
    protected IEdmModel edmModel;
    protected static bool actionSetAdded;

    public OpenDataServerBuilder(IServiceRegistry registry) : base()
    {
        _registry = registry;
        odataBuilder = new ODataConventionModelBuilder();
        StoreType = typeof(TStore);
    }

    public OpenDataServerBuilder(IServiceRegistry registry, string routePrefix, int pageLimit)
        : this(registry)
    {
        RoutePrefix += "/" + routePrefix;
        PageLimit = pageLimit;
    }

    public override void Build()
    {
        BuildEdm();
        _registry.MergeServices(true);
    }

    public object EntitySet(Type entityType)
    {
        var ets = odataBuilder.EntitySets.FirstOrDefault(e => e.EntityType.ClrType == entityType);
        if (ets != null)
            return ets;

        var entitySetName = entityType.Name;
        if (entityType.IsGenericType && entityType.IsAssignableTo(typeof(Identifier)))
            entitySetName = entityType.GetGenericArguments().FirstOrDefault().Name + "Identifier";

        var etc = odataBuilder.AddEntityType(entityType);
        etc.Name = entitySetName;
        ets = odataBuilder.AddEntitySet(entitySetName, etc);
        ets.EntityType.HasKey(entityType.GetProperty("Id"));

        SubEntitySet(entityType);

        return ets;
    }

    public object EntitySet<TDto>() where TDto : class
    {
        return odataBuilder.EntitySet<TDto>(typeof(TDto).Name);
    }

    public void AddInvocations(Type entityType)
    {
        var method = this.GetType().GetGenericMethod("AddInvocations");
        var methodInfo = method.MakeGenericMethod(entityType);
        methodInfo.Invoke(this, new object[0]);
    }

    public void SubEntitySet(Type subEntityType)
    {
        subEntityType
            .GetProperties()
            .Select(p => p.PropertyType)
            .ForEach(
                pt => pt.IsAssignableTo(typeof(IEnumerable)) ? pt.GetEnumerableElementType() : pt
            )
            .Where(p => p.IsAssignableTo(typeof(IContract)) || p.IsAssignableTo(typeof(IViewModel)))
            .ForEach(subType =>

             EntitySet(subType))

            .Commit();
    }

    public IEdmModel GetEdm()
    {
        if (edmModel == null)
        {
            odataBuilder.DataServiceVersion = new Version("4.0");
            odataBuilder.MaxDataServiceVersion = new Version("4.01");
            odataBuilder.BindingOptions = NavigationPropertyBindingOption.Auto;
            edmModel = odataBuilder.GetEdmModel();
            odataBuilder.ValidateModel(edmModel);
        }
        return edmModel;
    }

    public void BuildEdm()
    {
        Assembly[] asm = AppDomain.CurrentDomain.GetAssemblies();
        var controllerTypes = asm.SelectMany(
                a =>
                    a.GetTypes()
                        .Where(
                            type =>
                                type.GetCustomAttribute<OpenDataAttribute>() != null
                                || type.GetCustomAttribute<OpenServiceAttribute>() != null
                                || type.GetCustomAttribute<OpenDataRemoteAttribute>() != null
                                || type.GetCustomAttribute<OpenServiceRemoteAttribute>() != null
                        )
            )
            .ToArray();

        foreach (var controllerType in controllerTypes)
        {
            var genTypes = controllerType.BaseType.GenericTypeArguments;

            if (
                genTypes.Length > 4
                && genTypes[1].IsAssignableTo(StoreType)
                && genTypes[2].IsAssignableTo(StoreType)
            )
                EntitySet(genTypes[4]);
            else if (genTypes.Length > 3)
            {
                if (
                    genTypes[3].IsAssignableTo(typeof(IIdentifiable))
                    && (
                        genTypes[1].IsAssignableTo(StoreType)
                        || genTypes[0].IsAssignableTo(StoreType)
                    )
                )
                    EntitySet(genTypes[3]);
                else
                    continue;
            }
            else if (genTypes.Length > 2)
                if (
                    genTypes[2].IsAssignableTo(typeof(IIdentifiable))
                    && genTypes[0].IsAssignableTo(StoreType)
                )
                    EntitySet(genTypes[2]);
                else
                    continue;
        }
    }

    public IMvcBuilder AddODataServicer(IMvcBuilder mvc)
    {
        var model = GetEdm();
        var route = GetRoutes();
        var defaultBatchHandler = new DefaultODataBatchHandler();
        defaultBatchHandler.MessageQuotas.MaxNestingDepth = 5;
        defaultBatchHandler.MessageQuotas.MaxOperationsPerChangeset = 20;
        mvc.AddOData(b =>
        {
            b.RouteOptions.EnableQualifiedOperationCall = true;
            b.RouteOptions.EnableUnqualifiedOperationCall = true;
            b.RouteOptions.EnableKeyInParenthesis = true;
            b.RouteOptions.EnableControllerNameCaseInsensitive = true;
            b.RouteOptions.EnableActionNameCaseInsensitive = true;
            b.RouteOptions.EnablePropertyNameCaseInsensitive = true;
            b.RouteOptions.EnableKeyAsSegment = false;
            b.EnableContinueOnErrorHeader = true;
            b.EnableQueryFeatures(PageLimit)
                .AddRouteComponents(
                    route,
                    model,
                    ODataVersion.V401,
                    s =>
                    {
                        s.AddSingleton(
                                typeof(ODataPayloadValueConverter),
                                new CustomODataPayloadConverter()
                            )
                            .AddSingleton((IServiceProvider sp) => defaultBatchHandler);
                    }
                );
        });
        AddODataSupport(mvc);
        _registry.MergeServices(true);
        return mvc;
    }

    private IMvcBuilder AddODataSupport(IMvcBuilder mvc)
    {
        mvc.AddMvcOptions(options =>
        {
            foreach (
                OutputFormatter outputFormatter in options.OutputFormatters
                    .OfType<OutputFormatter>()
                    .Where(x => x.SupportedMediaTypes.Count == 0)
            )
            {
                outputFormatter.SupportedMediaTypes.Add(
                    new MediaTypeHeaderValue("_builder/prs.odatatestxx-odata")
                );
            }

            foreach (
                InputFormatter inputFormatter in options.InputFormatters
                    .OfType<InputFormatter>()
                    .Where(x => x.SupportedMediaTypes.Count == 0)
            )
            {
                inputFormatter.SupportedMediaTypes.Add(
                    new MediaTypeHeaderValue("_builder/prs.odatatestxx-odata")
                );
            }
        });
        return mvc;
    }

    protected override string GetRoutes()
    {
        if (StoreType == typeof(IEventStore))
        {
            return StoreRoutes.OpenEventRoute;
        }
        else if (StoreType == typeof(IAccountStore))
        {
            return StoreRoutes.OpenAuthRoute;
        }
        else
        {
            return StoreRoutes.OpenDataRoute;
        }
    }

    public override IDataServerBuilder AddInvocations<TAuth>() where TAuth : class
    {
        SetFunctionAndAction<TAuth>();
        return base.AddInvocations<TAuth>();
    }

    private void SetFunctionAndAction<TAuth>() where TAuth : class
    {
        var name = typeof(TAuth).Name;

        var action = odataBuilder.EntitySet<TAuth>(name).EntityType.Collection.Action("Action");

        var access = odataBuilder.EntitySet<TAuth>(name).EntityType.Collection.Action("Access");

        var setup = odataBuilder.EntitySet<TAuth>(name).EntityType.Collection.Action("Setup");
    }
}

public class CustomODataPayloadConverter : ODataPayloadValueConverter
{
    public override object ConvertFromPayloadValue(object value, IEdmTypeReference edmTypeReference)
    {
        if (edmTypeReference.PrimitiveKind() == EdmPrimitiveTypeKind.DateTimeOffset)
        {
            var str = value.ToString();
            if (str.Contains("0001"))
                return new DateTimeOffset(DateTime.Now);
            return new DateTimeOffset(DateTime.Parse(str));
        }

        if (edmTypeReference.IsEnum())
        {
            return value.ToString().ToTitleCase();
        }


        return base.ConvertFromPayloadValue(value, edmTypeReference);
    }
}
