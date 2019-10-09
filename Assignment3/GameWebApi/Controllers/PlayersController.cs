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
            var xxx =  _FileRepository.GetAll();

            return xxx;
        }

        [HttpPost]
        [Route("players/create")]
        public Task<Player> Create([FromBody]NewPlayer player)
        {
            var p =  _FileRepository.Create(player);
            return p; ;
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
        public Task<Player> MinScore([FromQuery]Int32 minScore)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Assignment 6: Selector matching
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("players/{name:alpha}")] // string
        public Task<Player> GetByName([FromRoute]string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Assignment 6: Selector matching
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("players/{id:Guid}")] // string
        public Task<Player> GetById([FromRoute]Guid id)
        {
            var p = _FileRepository.Get(id);
            return p;
        }
    }
}
