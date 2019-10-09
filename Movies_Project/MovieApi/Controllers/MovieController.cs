using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Models;
using Newtonsoft.Json;
using RestSharp;

namespace MovieApi.Controllers
{
    public class MovieController : ControllerBase
    {

        [HttpGet]
        [Route("movie/nowplaying")]
        public Task<MovieRoot> NowPlaying()
        {
            var client = new RestClient("https://api.themoviedb.org/3/movie/now_playing?page=1&language=en-US&api_key=47ec7fd89087e33845796b19578d55ad");
            var request = new RestRequest(Method.GET);
            request.AddParameter("undefined", "{}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var eventResponse = JsonConvert.DeserializeObject<MovieRoot>(response.Content);


            return Task.FromResult(eventResponse);
        }

        [HttpGet]
        [Route("movie/details/{id}")]
        public Task<MovieDetail> Details([FromRoute] int id)
        {
            var url = "https://api.themoviedb.org/3/movie/" + id.ToString() + "?language=en-US&api_key=47ec7fd89087e33845796b19578d55ad";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddParameter("undefined", "{}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var eventResponse = JsonConvert.DeserializeObject<MovieDetail>(response.Content);

            return Task.FromResult(eventResponse);
        }

    }
}