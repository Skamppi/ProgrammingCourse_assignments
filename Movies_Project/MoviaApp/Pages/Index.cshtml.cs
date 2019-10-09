using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MovieApi.Models;
using Newtonsoft.Json;
using RestSharp;

namespace MoviaApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public string MyProperty { get; set; }

        [BindProperty]
        public MovieRoot MoviesNowPlaying { get; set; }
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            var client = new RestClient("http://localhost:15931/movie/nowplaying");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Host", "localhost:15931");
            request.AddHeader("Postman-Token", "4976c241-d542-4531-8129-b801379b87e3,78587da2-59c1-441f-95c5-e759a3d04864");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("User-Agent", "PostmanRuntime/7.17.1");
            IRestResponse response = client.Execute(request);

            MoviesNowPlaying = JsonConvert.DeserializeObject<MovieRoot>(response.Content);
        }

        public void OnGetDetails(int id)
        {
            var client = new RestClient("http://localhost:15931/movie/details/" + id.ToString());
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Host", "localhost:15931");
            request.AddHeader("Postman-Token", "ccb85680-7d84-411e-9d61-b43d21515f3e,c7a5c052-41c1-4a68-9c4a-9648e53d5dce");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("User-Agent", "PostmanRuntime/7.17.1");
            IRestResponse response = client.Execute(request);
        }
    }
}
