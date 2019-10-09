using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameWebApi.Models;
using GameWebApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameWebApi.Controllers
{

    public class ItemsController : ControllerBase
    {
        IRepository _FileRepository;

        public ItemsController(IRepository fileRepository)
        {
            _FileRepository = fileRepository;
        }

    /// <summary>
    /// example: players/AE0DE1E1-62B7-45CC-88AB-D2B0730C56D7/items/add
    /// {
    ///"Level": 0,
    ///"Type": 0,
    ///"CreationDate": "2019-10-08T19:50:48.956Z"
    ///}
    /// </summary>
    /// <param name="playerId"></param>
    /// <param name="itm"></param>
    /// <returns></returns>
    [HttpPost]
        [Route("players/{playerId}/items/add")]
        
        public async Task<Player> Add([FromRoute]Guid playerId, [FromBody]NewItem itm)
        {
            if (ModelState.IsValid == false)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                       .Where(y => y.Count > 0)
                       .ToList();

                //throw new Exception("New item is not valid");

                //HOW TO RETURN ERROR???????
                //return await Task.FromResult(errors);
            }
            else
            {
                // get player and check level
                var p = await _FileRepository.Get(playerId);
                if (p != null)
                {
                    if (p.Level < 3 && itm.Type == ItemType.SWORD)
                    {
                        // return error
                        throw new LevelException("Sword is not allowed for a Player below level 3.");
                    }
                }
            }


            //var p = await _FileRepository.AddPlayerItems(itm);
            //return p; 

            return null;
        }
    }
}