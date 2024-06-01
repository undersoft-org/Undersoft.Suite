using Microsoft.EntityFrameworkCore;

namespace Undersoft.SDK.Service.Data.Identifier
{
    public interface IIdentifiersMapping
    {
        ModelBuilder Configure();
    }
}