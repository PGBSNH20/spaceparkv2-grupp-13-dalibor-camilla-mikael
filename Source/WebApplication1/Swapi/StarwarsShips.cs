using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Swapi
{
    public class StarwarsShips
    {
        private const string baseURL = "http://swapi.dev/api/";
        public static async Task<List<Starship>> GetStarships()
        {
            var client = new RestClient(baseURL);
            string requestUrl = "http://swapi.dev/api/starships/";
            StarshipResponse response;
            List<Starship> starships = new List<Starship>();

            while (requestUrl != null)
            {
                string resource = requestUrl.Substring(baseURL.Length);
                var request = new RestRequest(resource, DataFormat.Json);
                response = await client.GetAsync<StarshipResponse>(request);

                starships.AddRange(response.Results);
                requestUrl = response.Next;
            }
            return starships;
        }
    }
}
