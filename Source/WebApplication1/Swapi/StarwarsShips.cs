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
        private static async Task<StarshipResponse> GetShipPage(int page) // Get one page from API
        {
            var client = new RestClient("https://swapi.dev/api/");
            var request = new RestRequest($"people/?page={page}", DataFormat.Json);
            var response = await client.GetAsync<StarshipResponse>(request);
            return response;
        }
        private static Task<StarshipResponse> CreateTask(int i) // Create task to run several requests async
        {
            return Task.Run(() => GetShipPage(i));
        }
        public async Task<List<Starship>> GetAllShips() // Return list of all ships in API
        {
            var p = await GetShipPage(1);
            int numberOfShips = p.Results.Count;
            int shipsPerOnePage = p.Results.Count;
            int numberOfPages = (int)Math.Ceiling(1.0 * numberOfShips / shipsPerOnePage); // Use the number of ships and ships per page to calculate number of pages

            var tasks = new List<Task<StarshipResponse>>();
            for (int i = 1; i < numberOfPages + 1; i++) // For each page create a task to run all pages at the same time.
            {
                tasks.Add(CreateTask(i));
            }

            List<StarshipResponse> temp = new();
            List<Starship> results = new();

            for (int i = 0; i < numberOfPages; i++)
            {
                temp.AddRange((IEnumerable<StarshipResponse>)tasks[i].Result.Results); // Add each page result to list
            }

            foreach (var ships in temp)
            {
                results.AddRange(ships.Results);
            }
            return results;
        }
        /* public List<string> ShipListList { get; set; }

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
         }*/
    }
}
