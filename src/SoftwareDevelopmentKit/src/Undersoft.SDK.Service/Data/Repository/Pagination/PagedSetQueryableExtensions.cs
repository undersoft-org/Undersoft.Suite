using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Undersoft.SDK.Service.Data.Repository.Pagination
{
    using Microsoft.EntityFrameworkCore;

    public static class PagedSetQueryableExtensions
    {
        public static async Task<IPagedSet<T>> ToPagedSetAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize, int indexFrom = 0, CancellationToken cancellationToken = default)
        {
            if (indexFrom > pageIndex)
            {
                throw new ArgumentException($"indexFrom: {indexFrom} > pageIndex: {pageIndex}, must indexFrom <= pageIndex");
            }

            var count = await source.CountAsync(cancellationToken).ConfigureAwait(false);
            var items = await source.Skip((pageIndex - indexFrom) * pageSize)
                                    .Take(pageSize).ToListAsync(cancellationToken).ConfigureAwait(false);

            var PagedSet = new PagedSet<T>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                IndexFrom = indexFrom,
                TotalCount = count,
                Items = items,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };

            return PagedSet;
        }

    }
}
