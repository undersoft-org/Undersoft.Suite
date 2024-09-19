using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Exporter;
using Undersoft.SDK.Service.Server.Accounts.Identity;

namespace Undersoft.SDK.Service.Server;

using System.Diagnostics.Metrics;
using System.Text.Json;
using System.Text.Json.Serialization;
using Accounts;
using Accounts.Email;
using Documentation;
using IdentityModel;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Access.MultiTenancy;
using Undersoft.SDK.Service.Configuration;
using Undersoft.SDK.Service.Data.Repository.Source;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Hosting;
using Undersoft.SDK.Service.Server.Accounts.Identity;
using Undersoft.SDK.Service.Server.Accounts.Tokens;
using Undersoft.SDK.Utilities;
using Role = Role;

public partial class ServerSetup : ServiceSetup, IServerSetup
{
    protected IMvcBuilder _mvc;
    protected ITenant _tenant;

    public ServerSetup(ITenant tenant)
        : base(new ServiceCollection())
    {
        _tenant = tenant;
    }

    public ServerSetup(IServiceCollection services, IMvcBuilder mvcBuilder = null)
        : base(services)
    {
        if (mvcBuilder != null)
            _mvc = mvcBuilder;
        else
            _mvc = services.AddControllers();

        registry.MergeServices(services);
    }

    public ServerSetup(IServiceCollection services, IConfiguration configuration)
        : base(services, configuration)
    {
        _mvc = services.AddControllers();
        registry.MergeServices(services);
    }

    public IServerSetup AddDataServer<TServiceStore>(
        DataServerTypes dataServerTypes,
        Action<DataServerBuilder> builder = null
    )
        where TServiceStore : IDataStore
    {
        DataServerBuilder.ServiceTypes = dataServerTypes;
        if ((dataServerTypes & DataServerTypes.OData) > 0)
        {
            var ds = new OpenDataServerBuilder<TServiceStore>(registry);
            if (builder != null)
                builder.Invoke(ds);
            ds.Build();
            ds.AddODataServicer(_mvc);
        }
        if ((dataServerTypes & DataServerTypes.Grpc) > 0)
        {
            var ds = new GrpcDataServerBuilder<TServiceStore>(registry);
            if (builder != null)
                builder.Invoke(ds);
            ds.Build();
            ds.AddGrpcServicer();
        }
        if ((dataServerTypes & DataServerTypes.Rest) > 0)
        {
            var ds = new RestDataServerBuilder<TServiceStore>(registry);
            if (builder != null)
                builder.Invoke(ds);
            ds.Build();
        }

        registry.MergeServices(true);
        return this;
    }

    public IServiceSetup ConfigureTenant(IServicer mainServicer)
    {
        var mainManager = mainServicer.GetManager();
        mainManager.AddKeyedObject<ITenant>(_tenant.Id, _tenant);
        mainManager.AddKeyedObject<IServiceManager>(_tenant.Id, manager);
        manager.AddKeyedObject<ITenant>(_tenant.Id, _tenant);

        AddSourceProviderConfiguration();

        AddRepositorySources();

        AddDataStoreImplementations();

        base.ConfigureServices(null);

        AddValidators(null);

        AddMediator(null);

        registry.Configure<DataProtectionTokenProviderOptions>(o =>
            o.TokenLifespan = TimeSpan.FromHours(1)
        );
        registry.AddTransient<IAccountManager, AccountManager>();

        AddServerSetupCqrsImplementations();

        AddServerSetupInvocationImplementations();

        AddServerSetupRemoteCqrsImplementations();

        AddServerSetupRemoteInvocationImplementations();

        Services.AddOpenDataRemoteImplementations();

        Services.MergeServices(true);
        manager.BuildInternalProvider();

        mainManager.Registry.MergeServices(true);
        mainManager.BuildInternalProvider();

        return this;
    }

    public override IServiceSetup AddSourceProviderConfiguration()
    {
        var sspc = new ServerSourceProviderConfiguration(manager.Registry);
        registry.AddObject<ISourceProviderConfiguration>(sspc);
        ServiceManager.AddRootObject<ISourceProviderConfiguration>(sspc);

        return this;
    }

    public IServiceSetup AddJsonOptions()
    {
        _mvc.AddJsonOptions(json =>
        {
            var options = json.JsonSerializerOptions;
            options.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.NumberHandling = JsonNumberHandling.AllowReadingFromString;
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.IgnoreReadOnlyProperties = true;
            options.IgnoreReadOnlyProperties = true;
            options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, true));
            //options.Converters.Add(new BinaryJsonConverter());
        });
        return this;
    }

    public IServiceSetup AddHealthChecks()
    {
        services.AddHealthChecks();
        return this;
    }

    public IServiceSetup AddOpenTelemetry()
    {
        var config = configuration;

        Action<ResourceBuilder> configureResource = r =>
            r.AddService(
                serviceName: config.GetValue<string>("ServiceName"),
                serviceVersion: Environment.Version.ToString(),
                serviceInstanceId: Environment.MachineName
            );

        var tracingExporter = config.GetValue<string>("UseTracingExporter").ToLowerInvariant();
        var histogramAggregation = config
            .GetValue<string>("HistogramAggregation")
            .ToLowerInvariant();
        var metricsExporter = config.GetValue<string>("UseMetricsExporter").ToLowerInvariant();

        registry.AddSingleton<Instrumentation>();

        services
            .AddOpenTelemetry()
            .ConfigureResource(configureResource)
            .WithTracing(builder =>
            {
                switch (tracingExporter)
                {
                    case "jaeger":
                        builder.AddJaegerExporter();

                        builder.ConfigureServices(services =>
                        {
                            // Use IConfiguration binding for Jaeger exporter options.
                            services.Configure<JaegerExporterOptions>(config.GetSection("Jaeger"));

                            // Customize the HttpClient that will be used when JaegerExporter is configured for HTTP transport.
                            services.AddHttpClient(
                                "JaegerExporter",
                                configureClient: (client) =>
                                    client.DefaultRequestHeaders.Add(
                                        "X-Title",
                                        config.Name
                                            + " ,OS="
                                            + Environment.OSVersion
                                            + ",ServiceName="
                                            + Environment.MachineName
                                            + ",Domain="
                                            + Environment.UserDomainName
                                    )
                            );
                        });
                        break;

                    case "zipkin":
                        builder.AddZipkinExporter();

                        builder.ConfigureServices(services =>
                        {
                            // Use IConfiguration binding for Zipkin exporter options.
                            services.Configure<ZipkinExporterOptions>(config.GetSection("Zipkin"));
                        });
                        break;

                    case "otlp":
                        builder.AddOtlpExporter(otlpOptions =>
                        {
                            // Use IConfiguration directly for Otlp exporter source option.
                            otlpOptions.Endpoint = new Uri(config.GetValue<string>("Otlp:Source"));
                        });
                        break;

                    default:
                        builder.AddConsoleExporter();
                        break;
                }
            })
            .WithMetrics(builder =>
            {
                // Metrics

                // Ensure the MeterProvider subscribes to any custom Meters.
                builder.AddRuntimeInstrumentation().AddHttpClientInstrumentation();
                //.AddAspNetCoreInstrumentation();

                switch (histogramAggregation)
                {
                    case "exponential":
                        builder.AddView(instrument =>
                        {
                            return
                                instrument.GetType().GetGenericTypeDefinition()
                                == typeof(Histogram<>)
                                ? new ExplicitBucketHistogramConfiguration()
                                : null;
                        });
                        break;
                    default:
                        // Explicit bounds histogram is the default.
                        // No additional configuration necessary.
                        break;
                }

                switch (metricsExporter)
                {
                    case "otlp":
                        builder.AddOtlpExporter(otlpOptions =>
                        {
                            // Use IConfiguration directly for Otlp exporter source option.
                            otlpOptions.Endpoint = new Uri(config.GetValue<string>("Otlp:Source"));
                        });
                        break;
                    default:
                        builder.AddConsoleExporter();
                        break;
                }
            });

        return this;
    }

    public IServerSetup AddAccessServer<TContext, TAccount>()
        where TContext : DbContext
        where TAccount : class, IOrigin, IAuthorization
    {
        registry
            .Services.AddIdentity<AccountUser, Role>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Tokens.ProviderMap.Add(
                    "AccountEmailConfirmationTokenProvider",
                    new TokenProviderDescriptor(
                        typeof(AccountEmailConfirmationTokenProvider<AccountUser>)
                    )
                );
                options.Tokens.EmailConfirmationTokenProvider =
                    "AccountEmailConfirmationTokenProvider";
                options.Tokens.ProviderMap.Add(
                    "AccountPasswordResetTokenProvider",
                    new TokenProviderDescriptor(
                        typeof(AccountPasswordResetTokenProvider<AccountUser>)
                    )
                );
                options.Tokens.PasswordResetTokenProvider = "AccountPasswordResetTokenProvider";
                options.Tokens.ProviderMap.Add(
                    "AccountChangeEmailTokenProvider",
                    new TokenProviderDescriptor(
                        typeof(AccountChangeEmailTokenProvider<AccountUser>)
                    )
                );
                options.Tokens.ChangeEmailTokenProvider = "AccountChangeEmailTokenProvider";
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<TContext>();

        registry.AddTransient<AccountEmailConfirmationTokenProvider<AccountUser>>();
        registry.AddTransient<AccountPasswordResetTokenProvider<AccountUser>>();
        registry.AddTransient<AccountChangeEmailTokenProvider<AccountUser>>();
        registry.AddTransient<AccountRegistrationProcessTokenProvider<AccountUser>>();

        registry.AddTransient<IAccountManager, AccountManager>();
        registry.AddTransient<AccountService<TAccount>>();
        registry.AddTransient<IEmailSender, AccountEmailSender>();
        registry.Configure<AccountEmailSenderOptions>(configuration);

        return this;
    }

    public IServerSetup AddAuthentication()
    {
        var jwtOptions = new AccountTokenOptions();
        var jwtFactory = new AccountTokenGenerator(30, jwtOptions);
        registry.Configure<DataProtectionTokenProviderOptions>(o =>
            o.TokenLifespan = TimeSpan.FromHours(1)
        );

        registry.AddObject(jwtFactory);

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                //x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    //RoleClaimType = JwtClaimTypes.Role,
                    //NameClaimType = JwtClaimTypes.Email,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(jwtOptions.SecurityKey),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true
                };
            });
        return this;
    }

    public IServerSetup AddAuthorization()
    {
        var ic = configuration.AccessOptions;

        services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();

            ic.Scopes.ForEach(s => options.AddPolicy(s, policy => policy.RequireClaim("scope", s)));

            ic.Roles.ForEach(s => options.AddPolicy(s, policy => policy.RequireRole(s)));

            ic.Claims.ForEach(s => options.AddPolicy(s, policy => policy.RequireClaim(s)));
        });

        return this;
    }

    public IServerSetup AddSwagger()
    {
        string ver = configuration.Version;
        var ao = configuration.Name;

        //registry.AddApiVersioning(options =>
        //{
        //    options.AssumeDefaultVersionWhenUnspecified = true;
        //    options.DefaultApiVersion = new ApiVersion(1, 0);
        //    options.ReportApiVersions = true;
        //});
        //registry.AddVersionedApiExplorer(
        //        options =>
        //        {
        //            options.GroupNameFormat = "'v'VVV";
        //        });

        //registry.AddTransient<IConfigureOptions<SwaggerGenOptions>, OpenApiOptions>();

        registry.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(
                configuration.Version,
                new OpenApiInfo { Title = configuration.Name, Version = configuration.Version }
            );

            //options.OperationFilter<OpenApiDefaultValues>();
            options.OperationFilter<JsonIgnoreFilter>();
            options.DocumentFilter<IgnoreApiDocument>();

            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "Enter your JWT token in this field",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            };

            options.AddSecurityDefinition("Bearer", securityScheme);

            var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            };

            options.AddSecurityRequirement(securityRequirement);

            //options.OperationFilter<AuthorizeCheckOperationFilter>();

            //var filePath = Path.Combine(System.AppContext.BaseDirectory, "Undersoft.SSC.Service.Server.xml");
            //options.IncludeXmlComments(filePath);
            //options.IncludeGrpcXmlComments(filePath, includeControllerXmlComments: true);
        });

        return this;
    }

    public IServiceSetup AddRepositorySources()
    {
        var sourceTypes = configuration
            .Sources()
            .ForEach(c => AssemblyUtilities.FindType(c.Key))
            .Commit();
        return AddRepositorySources(sourceTypes);
    }

    public IServiceSetup AddRepositorySources(Type[] storeTypes)
    {
        IServiceConfiguration config = configuration;
        IEnumerable<IConfigurationSection> sources = config.Sources();

        RepositorySources repoSources = new RepositorySources();
        registry.AddSingleton(registry.AddObject<IRepositorySources>(repoSources).Value);

        var providerNotExists = new HashSet<string>();

        foreach (IConfigurationSection source in sources)
        {
            string connectionString = config.SourceConnectionString(source);
            if (_tenant != null)
                connectionString = connectionString.Replace(
                    "-db;",
                    $"-{_tenant.Id.ToString()}-db;"
                );
            SourceProvider provider = config.SourceProvider(source);
            int poolsize = config.SourcePoolSize(source);
            Type contextType = storeTypes.Where(t => t.FullName == source.Key).FirstOrDefault();

            if (
                (provider == SourceProvider.None)
                || (connectionString == null)
                || (contextType == null)
            )
            {
                continue;
            }

            if (providerNotExists.Add(provider.ToString()))
            {
                registry.RegisterEntityFrameworkSourceProvider(provider);
                registry.MergeServices(true);
            }

            Type iRepoType = typeof(IRepositorySource<>).MakeGenericType(contextType);
            Type repoType = typeof(RepositorySource<>).MakeGenericType(contextType);
            Type repoOptionsType = typeof(DbContextOptions<>).MakeGenericType(contextType);
            Type repoOptionsBuilderType = typeof(DbContextOptionsBuilder<>).MakeGenericType(
                contextType
            );

            var builder = registry.GetObject<ISourceProviderConfiguration>();
            var options = builder
                .BuildOptions(
                    repoOptionsBuilderType.New<DbContextOptionsBuilder>(),
                    provider,
                    connectionString
                )
                .Options;

            IRepositorySource repoSource = (IRepositorySource)repoType.New(options);

            Type storeDbType = typeof(DataStoreContext<>).MakeGenericType(
                DataStoreRegistry.GetStoreType(contextType)
            );

            Type storeOptionsType = typeof(DbContextOptions<>).MakeGenericType(storeDbType);
            Type storeRepoType = typeof(RepositorySource<>).MakeGenericType(storeDbType);

            IRepositorySource storeSource = (IRepositorySource)storeRepoType.New(repoSource);

            Type istoreRepoType = typeof(IRepositorySource<>).MakeGenericType(storeDbType);
            Type ipoolRepoType = typeof(IRepositoryContextPool<>).MakeGenericType(storeDbType);
            Type ifactoryRepoType = typeof(IRepositoryContextFactory<>).MakeGenericType(
                storeDbType
            );
            Type idataRepoType = typeof(IRepositoryContext<>).MakeGenericType(storeDbType);

            repoSources.Add(repoSource);

            AddDatabaseConfiguration(repoSource.Context);

            registry.AddObject(contextType, repoSource.Context);

            registry.AddObject(iRepoType, repoSource);
            registry.AddObject(repoType, repoSource);
            registry.AddObject(repoOptionsType, repoSource.Options);

            registry.AddObject(istoreRepoType, storeSource);
            registry.AddObject(ipoolRepoType, storeSource);
            registry.AddObject(ifactoryRepoType, storeSource);
            registry.AddObject(idataRepoType, storeSource);
            registry.AddObject(storeRepoType, storeSource);
            registry.AddObject(storeOptionsType, storeSource.Options);

            repoSource.PoolSize = poolsize;
            repoSource.CreatePool();
        }

        return this;
    }

    private void AddDatabaseConfiguration(IDataStoreContext context)
    {
        DbContext _context = context as DbContext;
        _context.ChangeTracker.AutoDetectChangesEnabled = true;
        _context.ChangeTracker.LazyLoadingEnabled = false;
        _context.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;
    }

    private string AddDataServiceStorePrefix(Type contextType, string routePrefix = null)
    {
        Type iface = DataStoreRegistry.GetStoreType(contextType);
        return GetStoreRoutes(iface, routePrefix);
    }

    public IServerSetup AddApiVersions(string[] apiVersions)
    {
        this.apiVersions = apiVersions;
        return this;
    }

    public IServerSetup ConfigureServer(
        bool includeSwagger = true,
        Type[] sourceTypes = null,
        Type[] clientTypes = null
    )
    {
        Assemblies = AppDomain.CurrentDomain.GetAssemblies();

        AddJsonOptions();

        AddSourceProviderConfiguration();

        Services.AddHttpContextAccessor();

        if (sourceTypes != null)
            AddRepositorySources(sourceTypes);
        else
            AddRepositorySources();

        AddDataStoreImplementations();

        base.ConfigureServices(clientTypes);

        AddValidators(Assemblies);

        AddMediator(Assemblies);

        AddServerSetupCqrsImplementations();

        AddServerSetupInvocationImplementations();

        AddServerSetupRemoteCqrsImplementations();

        AddServerSetupRemoteInvocationImplementations();

        if (includeSwagger)
            AddSwagger();

        AddAuthentication();
        AddAuthorization();

        Services.MergeServices(true);

        return this;
    }

    public IServerSetup UseServiceClients()
    {
        this.LoadOpenDataEdms().ConfigureAwait(true);
        return this;
    }
}
