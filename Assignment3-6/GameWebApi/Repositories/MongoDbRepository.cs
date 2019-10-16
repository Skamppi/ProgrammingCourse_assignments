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

        private  IMongoCollection<Player> _players; // TODO: toimiiko näin?
        public MongoDbRepository(IMongoDatabaseSettings settings)
        {
            _settings = settings;
            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(settings.DatabaseName);            
            _players = _database.GetCollection<Player>(settings.PlayersCollectionName);
        }

        public async Task<Player> Create(NewPlayer player)
        {
            // _players.InsertOne(player);
            Player newp = new Player();
            newp.CreationTime = DateTime.Now;
            newp.Level = 0;
            newp.IsBanned = false;
            newp.Score = 0;
            newp.Name = player.Name;

            await _players.InsertOneAsync(newp);
            return newp;
        }

        public async Task<Player> CreatePlayer(Player player)
        {
            await _players.InsertOneAsync(player);
            return player;
        }

        public async Task<Player> UpdatePlayer(Player player)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq(p => p.Id, player.Id);
            await _players.ReplaceOneAsync(filter, player);
            return player;
        }

        public async Task<Item> CreateItem(Guid playerId, Item item)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            var player = await _players.Find(filter).FirstOrDefaultAsync();
            player.Items.Add(item);
            await _players.ReplaceOneAsync(filter, player);
            return item;
        }

        public async Task<Player> Delete(Guid id)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq(p => p.Id, id);
            return await _players.FindOneAndDeleteAsync(filter);

        }

        public Task<Item> DeleteItem(Guid playerId, Item item)
        {
            throw new NotImplementedException();
        }

        public async Task<Player> Get(Guid id)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq(p => p.Id, id);
            var x = await _players.Find(filter).FirstOrDefaultAsync();
            return x;
        }

        public async Task<Player[]> GetAll()
        {
            var players = await _players.Find(new BsonDocument()).ToListAsync();
            return players.ToArray();
        }

        public Task<Item[]> GetAllItems(Guid playerId)
        {
            throw new NotImplementedException();
        }

        public async Task<Item> GetItem(Guid playerId, Guid itemId)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            var p = await _players.Find(filter).FirstOrDefaultAsync();

            Item ret = null;
            foreach (Item item in p.Items)
            {
                if (item.Id == itemId)
                {
                    ret = item;
                    break;
                }
            }

            return ret;
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

        public async Task<Player> GetByName(string name)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq(p => p.Name, name);
            var x = await _players.Find(filter).FirstOrDefaultAsync();
            return x;
        }

        public async Task<Player[]> GetMinScore(int minScore)
        {
            var filter = Builders<Player>.Filter.Gte(p => p.Score, minScore);
            var players = await _players.Find(filter).ToListAsync();
            return players.ToArray();
        }

        public async Task<Player> UpdateScore(Guid playerId, int score)
        {
            var filter = Builders<Player>.Filter.Eq(x => x.Id, playerId);
            var update = Builders<Player>.Update.Set(x => x.Score, score);
            var x = await _players.FindOneAndUpdateAsync(filter, update);
            return x;
        }

        public async Task<Player[]> GetByTag(Tags tag)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq(p =>p.Tag, tag);
            var players = await _players.Find(filter).ToListAsync();
            return players.ToArray();
        }

        public async Task<Player[]> GetTop10ByScore()
        {
            var list = await _players.Find(new BsonDocument())
                       .Sort(Builders<Player>.Sort.Descending("Score"))
                       .ToListAsync();
            return list.ToArray();
        }

        public async Task<Player> SellItem(Guid playerId, Guid itemId, int addToScore)
        {

            FilterDefinition <Player> filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            var player = await _players.Find(filter).FirstOrDefaultAsync();
            // remove item
            player.Items.RemoveAll(s => s.Id == itemId);

            // update score
            player.Score += addToScore;

            // update players
            await _players.ReplaceOneAsync(filter, player);
            return player;
        }

        public async Task<Player[]> FindPlayersByItemLevel(int level)
        {
            var filter = Builders<Player>.Filter.ElemMatch(x => x.Items, x => x.Level == level);
            var res = await _players.Find(filter).ToListAsync();
            return res.ToArray();
        }

        public async Task<Player[]> FindPlayersByItemsSize(int size)
        {
            var f2 = Builders<Player>.Filter.Size(px => px.Items, size);
            var res = await _players.Find(f2).ToListAsync();
            return res.ToArray();
        }
    }
}
