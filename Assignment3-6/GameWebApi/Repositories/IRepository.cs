using GameWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi.Repositories
{
    public interface IRepository
    {
        Task<Player> Get(Guid id);
        Task<Player[]> GetAll();
        Task<Player> Create(NewPlayer player);
        Task<Player> Modify(Guid id, ModifiedPlayer player);
        Task<Player> Delete(Guid id);

        Task<Item> CreateItem(Guid playerId, Item item);
        Task<Item> GetItem(Guid playerId, Guid itemId);
        Task<Item[]> GetAllItems(Guid playerId);
        Task<Item> UpdateItem(Guid playerId, Item item);
        Task<Item> DeleteItem(Guid playerId, Item item);

        // new async stuff
        Task<Player> CreatePlayer(Player player);
        Task<Player>UpdatePlayer(Player player);
        Task<Player> GetByName(string name);
        Task<Player[]> GetMinScore(int minScore);
        Task<Player> UpdateScore(Guid playerId, int score);
        Task<Player[]> GetByTag(Tags tag);
        Task<Player[]> GetTop10ByScore();
        Task<Player> SellItem(Guid playerId, Guid itemId, int addToScore);
        Task<Player[]> FindPlayersByItemLevel(int level);
        Task<Player[]> FindPlayersByItemsSize(int size);
    }
}
