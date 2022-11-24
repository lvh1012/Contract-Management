using API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class QueryExtension
    {
        public static async Task<List<T>> GetPage<T>(this IQueryable<T> query, Page page = null)
        {
            page ??= new Page();
            var totalRow = await query.CountAsync();
            var totalPage = (int)Math.Ceiling(totalRow / (double)page.PageSize);
            if (page.PageIndex > totalPage)
            {
                page.PageIndex = totalPage;
            }

            page.TotalRow = totalRow;
            page.TotalPage = totalPage;

            var data = await query.Skip((page.PageIndex - 1) * page.PageSize).Take(page.PageSize).ToListAsync();
            return data;
        }
    }
}