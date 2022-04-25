using CatalogApi.DTOs;
using CatalogApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Extensions
{
    public static class Extensions
    {
        public static ItemDTO AsDTO (this Item item)
        {
            return new ItemDTO
            {
                Id = item.Id,
                Name = item.Name,
                CreatedDate = item.CreatedDate,
                Price = item.Price
            };
        }
    }
}
