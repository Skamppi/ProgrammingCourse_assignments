using GameWebApi.Models;
using GameWebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GameWebApi.Controllers
{
    public class PlayersController : ControllerBase
    {
        IRepository _FileRepository;

        public PlayersController(IRepository fileRepository)
        {
            _FileRepository = fileRepository;
        }

        [HttpGet]
        [Route("players/gettest")]
        public Task<Player> GetTest(Guid id)
        {
            return _FileRepository.Get(id);
        }


        [HttpGet]
        [Route("players/get")]
        public Task<Player> Get(Guid id)
        {
            var p = _FileRepository.Get(id);
            return p;
        }


        [HttpGet]
        [Route("players/getall")]
        public Task<Player[]> GetAll()
        {
            var xxx = _FileRepository.GetAll();

            return xxx;
        }

        [HttpPost]
        [Route("players/create")]
        public Task<Player> Create([FromBody]NewPlayer player)
        {
            var p = _FileRepository.Create(player);
            return p; ;
        }


        // NEW
        [HttpPost]
        [Route("players/createplayer")]
        public async Task<Player> CreatePlayer([FromBody]Player player)
        {
            if (player != null)
            {
                player.CreationTime = DateTime.Now;
                var p = await _FileRepository.CreatePlayer(player);
                return p;
            }
            return null;
        }
        // NEW
        [HttpPost]
        [Route("players/updateplayer")]
        public async Task<Player> UpdatePlayer([FromBody]Player player)
        {
            if (player != null)
            {
                var p = await _FileRepository.UpdatePlayer(player);
                return p;
            }
            return null;
        }
        // NEW
        [HttpGet]
        [Route("players/getitem")]
        public async Task<Item> GetItem(Guid playerId, Guid itemId)
        {
            var itm = await _FileRepository.GetItem(playerId, itemId);
            return itm;
        }
        // NEW
        [HttpPost]
        [Route("players/updatescore")]
        public async Task<Player> UpdateScore(Guid playerId, int score)
        {
            var itm = await _FileRepository.UpdateScore(playerId, score);
            return itm;
        }


        [HttpPost]
        [Route("players/modify/{id}")]
        public Task<Player> Modify([FromRoute]Guid id, [FromBody]ModifiedPlayer player)
        {
            var p = _FileRepository.Modify(id,player);
            return p;
        }

        [HttpDelete]
        [Route("players/delete/{id}")]
        public Task<Player> Delete([FromRoute]Guid id)
        {
            return _FileRepository.Delete(id);
        }

        /// <summary>
        /// Assignment 6: 1. Ranges
        /// </summary>
        /// <param name="minScore"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("players/")]
        public async Task<Player[]> MinScore([FromQuery]Int32 minScore)
        {
            return await _FileRepository.GetMinScore(minScore);
        }

        /// <summary>
        /// Assignment 6: Selector matching
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]

        //[Route("players/{name:alpha}")] // string
        [Route("players/getbyname/{name}")] // string
        public async Task<Player> GetByName([FromRoute]string name)
        {
            var p = await _FileRepository.GetByName(name);
            return p;
        }

        /// <summary>
        /// Assignment 6: Selector matching
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("players/{id:Guid}")] // string
        public async Task<Player> GetById([FromRoute]Guid id)
        {
            var p = await _FileRepository.Get(id);
            return p;
        }

        [HttpPost]
        [Route("players/additem")] // string
        public async Task<Item> AddItem([FromQuery]Guid playerId,[FromBody]Item itm)
        {
            var p = await _FileRepository.CreateItem(playerId, itm);
            return p;
        }

        [HttpGet]
        [Route("players/top10")] // string
        public async Task<Player[]> GetTop10()
        {
            var p = await _FileRepository.GetTop10ByScore();
            return p;
        }

        [HttpDelete]
        [Route("players/{playerId}/items/sellitem")] // string
        public async Task<Player> SellItem([FromRoute]Guid playerId, [FromQuery]Guid itemId, [FromQuery]int addToScore)
        {
            var p = await _FileRepository.SellItem(playerId, itemId, addToScore);
            return p;
        }

        [HttpGet]
        [Route("players/getbyitemlevel")] // string
        public async Task<Player[]> GetPlayerByItemLevel([FromQuery]int level)
        {
            var p = await _FileRepository.FindPlayersByItemLevel(level);
            return p;
        }

        [HttpGet]
        [Route("players/getbyitemssize")] // string
        public async Task<Player[]> GetPlayerByItemsSize([FromQuery]int size)
        {
            var p = await _FileRepository.FindPlayersByItemsSize(size);
            return p;
        }
    }
}
