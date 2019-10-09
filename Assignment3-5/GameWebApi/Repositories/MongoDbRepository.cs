using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameWebApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace GameWebApi.Repositories
{
    public class MongoDbRepository : IRepository
    {
        IMongoDatabaseSettings _settings;
        IMongoDatabase _database;

        //private IMongoCollection<Player> _players; // TODO: toimiiko näin? EI TOIMI

        private  IMongoCollection<BsonDocument> _players; // TODO: toimiiko näin? TOIMII

        public MongoDbRepository(IMongoDatabaseSettings settings)
        {
            _settings = settings;
            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(settings.DatabaseName);


            //_players = _database.GetCollection<Player>(settings.PlayersCollectionName);
            //BsonClassMap.RegisterClassMap<Type>(cm =>
            //{
            //    cm.AutoMap();
            //    cm.MapIdMember(c => c.GUID);
            //});

            _players = _database.GetCollection<BsonDocument>(settings.PlayersCollectionName);

        }

        public Task<Player> Create(NewPlayer player)
        {
            // _players.InsertOne(player);
            var newGuid = Guid.NewGuid().ToString().ToUpper();
            var document = new BsonDocument
            {
                { "Id", newGuid },
                { "Name", player.Name },
                { "Score", 0 },
                { "Level", 0 },
                { "Isbanned", "false" },
                { "CreationTime", DateTime.Now.ToString() },
            };

            _players.InsertOne(document);
            return null;
        }

        public Task<Item> CreateItem(Guid playerId, Item item)
        {
            // get player

            // add items

            throw new NotImplementedException();
        }

        public Task<Player> Delete(Guid id)
        {
            // MUST BE UPPERCASE
            var sid = id.ToString().ToUpper();

            //var filter = Builders<BsonDocument>.Filter.Eq("Id", "3A287E9D-3F0F-4A19-AF36-FFD1CB4C5B88");

            var filter = Builders<BsonDocument>.Filter.Eq("Id", sid);
            _players.DeleteOne(filter);

            // DONT work, why????
            //var x = _players.DeleteOne(p => p.Id == id);
            
            return null;
        }

        public Task<Item> DeleteItem(Guid playerId, Item item)
        {
            throw new NotImplementedException();
        }

        public Task<Player> Get(Guid id)
        {          
            var first = _players.Find(new BsonDocument()).FirstOrDefault();

            var filter = Builders<BsonDocument>.Filter.Eq("Id", "AE0DE1E1-62B7-45CC-88AB-D2B0730C56D7");

            var document2 = _players.Find(filter).First();


            // These dont work !!!!
            //var x = _players.Find<Player>(p => p.Id == id).FirstOrDefault();
            //var xx = _players.Find<Player>(p => p.Name == "jack bauer").FirstOrDefault();

            // TODO: must return Player
            
            throw new NotImplementedException();
        }

        public Task<Player[]> GetAll()
        {
            var all = _players.Find(player => true).ToList();
            throw new NotImplementedException();
        }

        public Task<Item[]> GetAllItems(Guid playerId)
        {
            // get player

            // return items
            throw new NotImplementedException();
        }

        public Task<Item> GetItem(Guid playerId, Guid itemId)
        {
            throw new NotImplementedException();
        }

        public Task<Player> Modify(Guid id, ModifiedPlayer player)
        {
            //_players.ReplaceOne(p => p.Id == id, player);

            var filter = Builders<BsonDocument>.Filter.Eq("Id", id.ToString().ToUpper());
            var update = Builders<BsonDocument>.Update.Set("Score", player.Score);

            _players.UpdateOne(filter, update);

            return null;
            //return Task.FromResult(player);
        }

        public Task<Item> UpdateItem(Guid playerId, Item item)
        {


            throw new NotImplementedException();
            
        }
    }
}
