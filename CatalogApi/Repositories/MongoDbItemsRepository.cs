using CatalogApi.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Repositories
{
    public class MongoDbItemsRepository : IItemsRepository
    {
        private const string databaseName = "catalog";
        private const string collectionName = "items";
        private IMongoCollection<Item> _itemCollection;
        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

        public MongoDbItemsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            _itemCollection = database.GetCollection<Item>(collectionName);
        }
        public async Task<Item> CreateItemAsync(Item item)
        {
            await _itemCollection.InsertOneAsync(item);
            return item;
        }

        public async  Task DeleteItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            await _itemCollection.DeleteOneAsync(filter);
        }
         
        public async  Task<Item> GetItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            var item = await  _itemCollection.Find(filter).SingleOrDefaultAsync();
            return item;
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await _itemCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateItemAsync(Item item)
        {
            var filter = filterBuilder.Eq(existingItem => existingItem.Id, item.Id);
           await  _itemCollection.ReplaceOneAsync(filter, item);
        }
    }
}
