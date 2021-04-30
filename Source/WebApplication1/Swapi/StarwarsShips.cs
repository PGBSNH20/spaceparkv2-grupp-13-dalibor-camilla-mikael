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
        public List<string> ShipListList { get; set; }
        private const string basicURL = "http://swapi.dev/api/";

        public StarwarsShips()
        {
            ShipListList = new List<string>();
        }

        // Download one page of names to PeopleList.
        public async Task<StarshipResponse> GetShipsFromPageAsync(int page)
        {
            var client = new RestClient(basicURL);
            var request = new RestRequest($"people/?page={page}", DataFormat.Json);

            var peopleResponse = await client.GetAsync<StarshipResponse>(request);

            return peopleResponse;

        }

        // Get number of pages avalible from swapi.
        public async Task<IEnumerable<int>> GetPagesAsync()
        {
            bool finnishedList = true;
            IList<int> pages = new List<int>();
            int page = 1;
            var client = new RestClient(basicURL);
            //var request = new RestRequest($"people/?page={page}", DataFormat.Json);
            //var peopleResponse = await client.GetAsync<PeopleResponse>(request);

            while (finnishedList)
            {
                var request = new RestRequest($"people/?page={page}", DataFormat.Json);
                var starshipResponse = Task.Run(async () => await client.GetAsync<StarshipResponse>(request));

                if (starshipResponse.Result.Count != 0)
                {
                    pages.Add(page);
                    page++;
                }
                else
                {
                    finnishedList = false;
                }
            }
            return pages;
        }


        // Add people to list async.
        public async void DownloadShipsAsync(IEnumerable<int> pages)
        {
            List<Task<StarshipResponse>> tasks = new List<Task<StarshipResponse>>();

            foreach (var page in pages)
            {
                Task<StarshipResponse> task = GetShipsFromPageAsync(page);
                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());

            foreach (var task in tasks)
            {
                foreach (var p in task.Result.Results)
                {
                    this.ShipListList.Add(p.Name);
                }
            }
        }
    }
}
