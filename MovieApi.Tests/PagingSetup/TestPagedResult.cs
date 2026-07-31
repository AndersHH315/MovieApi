using MovieApi.Core.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApi.Tests.PagingSetup
{
    public static class TestPagedResult
    {
        public static PagedResult<T> Create<T>(IEnumerable<T> items,int currentPage = 1,int pageSize = 10)
        {
            var list = items.ToList();

            return new PagedResult<T>
            {
                Data = list,
                Meta = new PagingMeta
                {
                    CurrentPage = currentPage,
                    PageSize = pageSize,
                    TotalItems = list.Count,
                    TotalPages = (int)Math.Ceiling(list.Count / (double)pageSize),

                }
            };
        }
    }
}
