using System.Linq;
using System.Collections.Generic;


namespace Northwind.Repository
{
    public class PagedData<T>
    {
        public static PagedData<T> Create(IEnumerable<T> input, PageInfo pageInfo)
        {
            return new PagedData<T>
                {
                    Total = input.Count(),
                    Data = input.Skip((pageInfo.PageNumber - 1)*pageInfo.PageSize).Take(pageInfo.PageSize)
                };
        }

        public int Total { get; set; }
        public IEnumerable<T> Data { get; set; }    
    }
}