using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Domain.Shared.Entities
{
    public class PaginatedList<T>
    {
        public int PageIndex { get; }
        public int TotalPages { get; set; }
        public List<T> Items { get; set; }

        public PaginatedList() { }

        public PaginatedList(IEnumerable<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            Items = new List<T>();
            Items.AddRange(items);
        }

        public bool PreviousPage => (PageIndex > 1);

        public bool NextPage => (PageIndex < TotalPages);

        public async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
