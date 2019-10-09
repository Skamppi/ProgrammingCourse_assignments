using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieApi.Models;
using Newtonsoft.Json;
using RestSharp;

namespace MoviaApp.Pages
{
    public class DetailModel : PageModel
    {
        [BindProperty]
        public MovieDetail Detaildata { get; set; }

        [BindProperty]
        public string Message { get; set; }
        public void OnGet()
            
        {
            var id = Request.QueryString.Value;
            string[] x = id.Split('=');
            var url = "http://localhost:15931/movie/details/" + x[1];
            var client = new RestClient("http://localhost:15931/movie/details/" + x[1]);
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

            try
            {
                Detaildata = JsonConvert.DeserializeObject<MovieDetail>(response.Content);
            }
            catch (Exception)
            {
                Message = "error happened";
            }
            

        }
    }
}