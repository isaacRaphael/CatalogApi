using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.DTOs
{
    public class CreateItemDTO
    {   [Required]
        public string Name { get; set; }
        [Range(1,int.MaxValue)]
        public decimal Price { get; set; }
    }
}
