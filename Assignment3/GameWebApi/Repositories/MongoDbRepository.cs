using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameWebApi.Models;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace GameWebApi.Repositories
{
    public class MongoDbRepository : IRepository
    {
        IMongoDatabaseSettings _settings;
        IMongoDatabase _database;

        private  IMongoCollection<Player> _players; // TODO: toimiiko näin?
        public MongoDbRepository(IMongoDatabaseSettings settings)
        {
            _settings = settings;
            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(settings.DatabaseName);

            
            _players = _database.GetCollection<Player>(settings.PlayersCollectionName);

            BsonClassMap.RegisterClassMap<Type>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(c => c.GUID);
            });
        }

        public Task<Player> Create(NewPlayer player)
        {
           // _players.InsertOne(player);
            throw new NotImplementedException();
        }

        public Task<Item> CreateItem(Guid playerId, Item item)
        {
            
            throw new NotImplementedException();
        }

        public Task<Player> Delete(Guid id)
        {
           
            var x = _players.DeleteOne(p => p.Id == id);
            
            return null;
        }

        public Task<Item> DeleteItem(Guid playerId, Item item)
        {
            throw new NotImplementedException();
        }

        public Task<Player> Get(Guid id)
        {
            //var x = _players.Find<Player>(p => p.Id == id).FirstOrDefault();
            var xx = _players.Find<Player>(p => p.Name == "jack bauer").FirstOrDefault();
            throw new NotImplementedException();
        }

        public Task<Player[]> GetAll()
        {
            _players.Find(player => true).ToList();
            throw new NotImplementedException();
        }

        public Task<Item[]> GetAllItems(Guid playerId)
        {
            throw new NotImplementedException();
        }

        public Task<Item> GetItem(Guid playerId, Guid itemId)
        {
            throw new NotImplementedException();
        }

        public Task<Player> Modify(Guid id, ModifiedPlayer player)
        {
            //_players.ReplaceOne(p => p.Id == id, player);
            throw new NotImplementedException();
        }

        public Task<Item> UpdateItem(Guid playerId, Item item)
        {
            throw new NotImplementedException();
        }
    }
}
