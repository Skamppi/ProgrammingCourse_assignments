using GameWebApi.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi.Repositories
{
    public class FileRepository : IRepository
    {
        IConfiguration _config;
        string fileNameToRead = "game-dev.txt";
        public FileRepository(IConfiguration config)
        {
            _config = config;
            fileNameToRead  = _config.GetValue<string>("Filename");
        }

        public Task<Player> Get(Guid id)
        {
            List<Player> items = null;
            Player retPlayer = null;

            using (StreamReader r = new StreamReader(fileNameToRead))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<Player>>(json);
            }
            if (items != null)
            {
                foreach (var p in items)
                {
                    if (p.Id == id)
                    {
                        retPlayer = p;
                        break;
                    }
                }
            }

            return Task.FromResult(retPlayer);
        }

        public Task<Player[]> GetAll()
        {
            // read file
            List<Player> items = null;
            using (StreamReader r = new StreamReader("game-dev.txt"))
            {
                string json = r.ReadToEnd();
                 items = JsonConvert.DeserializeObject<List<Player>>(json);              
            }
            return Task.FromResult(items.ToArray()); ;
        }

        public Task<Player> Create(NewPlayer player)
        {
            var p = new Player();
            p.Id = Guid.NewGuid();
            p.Name = player.Name;

            // write to file

            return Task.FromResult(p);
        }
        public Task<Player> Modify(Guid id, ModifiedPlayer player)
        {
            List<Player> items = null;
            Player pl = null;
            using (StreamReader r = new StreamReader("game-dev.txt"))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<Player>>(json);
            }
            if (items != null)
            {
                foreach (var p in items)
                {
                    if (p.Id == id)
                    {
                        p.Score = player.Score;
                        pl = p;
                    }
                }
            }

            return Task.FromResult(pl);
        }
        public Task<Player> Delete(Guid id)
        {
            return null;
        }

        public Task<Item> CreateItem(Guid playerId, Item item)
        {
            return null;
        }
        public async Task<Item> GetItem(Guid playerId, Guid itemId)
        {
            return null;
        }
        public async Task<Item[]> GetAllItems(Guid playerId)
        {
            return null;
        }
        public async Task<Item> UpdateItem(Guid playerId, Item item)
        {
            return null;
        }
        public async Task<Item> DeleteItem(Guid playerId, Item item)
        {
            return null;
        }

        public async Task<Player> CreatePlayer(Player player)
        {
            throw new NotImplementedException();
        }
        public async Task<Player> GetByName(string name)
        {
            throw new NotImplementedException();
        }
        public async Task<Player[]> GetMinScore(int minScore)
        {
            throw new NotImplementedException();
        }

        public async Task<Player> UpdatePlayer(Player player)
        {
            throw new NotImplementedException();
        }
        public async Task<Player> UpdateScore(Guid playerId, int score)
        {
            throw new NotImplementedException();
        }

        public async Task<Player[]> GetByTag(Tags tag)
        {
            throw new NotImplementedException();
        }

        public async Task<Player[]> GetTop10ByScore()
        {
            throw new NotImplementedException();
        }

        public async Task<Player> SellItem(Guid playerId, Guid itemId, int addToScore) 
        {
            throw new NotImplementedException();
        }

        public async Task<Player[]> FindPlayersByItemLevel(int level)
        {
            throw new NotImplementedException();
        }

        public async Task<Player[]> FindPlayersByItemsSize(int size)
        {
            throw new NotImplementedException();
        }
    }
   

}
