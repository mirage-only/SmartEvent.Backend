using SmartEvent.Backend.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace SmartEvent.Backend.Application.Common.Extensions;

public static class QueryableExtensions
{
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize)
    {
        var count = await query.CountAsync();
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        return new PagedResult<T>(items, count, pageNumber, pageSize);
    } 
}