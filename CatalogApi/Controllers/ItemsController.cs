using CatalogApi.Entities;
using CatalogApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using CatalogApi.Extensions;
using System.Threading.Tasks;
using CatalogApi.DTOs;

namespace CatalogApi.Controllers
{

    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository repo;

        public ItemsController(IItemsRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDTO>> GetItems()
        {
            var items = await repo.GetItemsAsync();
            var itemsDTOs = items.Select(item => item.AsDTO());
            return itemsDTOs;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDTO>> GetItem(Guid id)
        {
            var item =  await repo.GetItemAsync(id);

            if (item is null)
            {
                return NotFound();
            }
            return item.AsDTO();
        }

        [HttpPost]
        public async Task<ActionResult<ItemDTO>> CreateItem(CreateItemDTO createItemDTO)
        {
            var item = new Item()
            {
                CreatedDate = DateTimeOffset.UtcNow, 
                Id = Guid.NewGuid(),
                Name = createItemDTO.Name,
                Price = createItemDTO.Price
            };
            await repo.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetItem), new { Id = item.Id }, item.AsDTO());
    }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItem (Guid id, UpdateItemDTO updateItemDTO)
        {
            var existingItem = await repo.GetItemAsync(id);
            if(existingItem is null)
            {
                return NotFound();
            }

            existingItem = existingItem with
            {
                Name = updateItemDTO.Name,
                Price = updateItemDTO.Price
            };
            await repo.UpdateItemAsync(existingItem);

            return NoContent();

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem (Guid id)
        {
            await repo.DeleteItemAsync(id);
            return NoContent();
        }

    }
}
