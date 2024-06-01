using Microsoft.EntityFrameworkCore;

namespace Undersoft.SDK.Service.Data.Identifier;
public class IdentifiersMapping : IIdentifiersMapping
{
    private readonly IIdentifiersMapping genericMapping;

    public IdentifiersMapping(Type type, ModelBuilder builder)
    {
        genericMapping = typeof(IdentifiersMapping<>).MakeGenericType(type).New<IIdentifiersMapping>(builder);
    }

    public ModelBuilder Configure()
    {
        return genericMapping.Configure();
    }
}