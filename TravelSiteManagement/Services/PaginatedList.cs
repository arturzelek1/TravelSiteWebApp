using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelSiteWeb.Models;

namespace TravelSiteWeb.Services
{
    public class PaginatedList<T> : List<T>, IPaginatedList<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        // Public constructor
        public PaginatedList(IEnumerable<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        // Implement IPaginatedList<T> interface method
        public async Task<IPaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }

    public interface IPaginatedListService
    {
        Task<IPaginatedList<T>> CreateAsync<T>(IQueryable<T> source, int pageIndex, int pageSize);


    }
    public class PaginatedListService : IPaginatedListService
    {


        public async Task<IPaginatedList<T>> CreateAsync<T>(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}


