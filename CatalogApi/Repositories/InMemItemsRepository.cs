using CatalogApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Repositories
{
    public class InMemItemsRepository : IItemsRepository
    {
        private readonly List<Item> items = new()
        {
            new Item { Id = Guid.NewGuid(), Name = "potion", Price = 9, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Diamond Sword", Price = 20, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Iron Shield", Price = 16, CreatedDate = DateTimeOffset.UtcNow }
        };


        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await Task.FromResult(items);
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            var item =  items.Where(item => item.Id == id).SingleOrDefault();
            return await Task.FromResult(item);
        }

        public async Task<Item> CreateItemAsync(Item item)
        {
            items.Add(item);
            var Toreturn = item;
            return await Task.FromResult(Toreturn);
        }

        public async  Task UpdateItemAsync(Item item)
        {
            var index =await Task.Run(()=> items.FindIndex(dtem => dtem.Id == item.Id));
            items[index] = item;
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var item = items.Where(itm => itm.Id == id).SingleOrDefault();
            if(item is not null)
            {
                await Task.Run(() => items.Remove(item));
            }
        }
    }
}
