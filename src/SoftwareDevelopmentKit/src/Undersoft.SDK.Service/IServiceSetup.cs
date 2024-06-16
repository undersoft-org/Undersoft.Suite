using System.Reflection;

namespace Undersoft.SDK.Service;

public partial interface IServiceSetup
{
    IServiceSetup AddValidators(Assembly[] assemblies = null);
    IServiceSetup AddMediator(Assembly[] assemblies = null);
    IServiceSetup AddCaching();
    IServiceSetup ConfigureServices(
        Type[] clientTypes = null,
        Action<IServiceSetup> services = null
    );
    IServiceSetup AddRepositoryClients();
    IServiceSetup AddImplementations();
    IServiceSetup AddPropertyInjection();
    IServiceSetup MergeServices();
    IServiceRegistry Services { get; }
    IServiceManager Manager { get; }
}
