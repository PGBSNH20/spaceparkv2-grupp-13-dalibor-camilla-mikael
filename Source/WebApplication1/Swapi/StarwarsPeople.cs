using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Swapi
{
    public class StarwarsPeople
    {
        private static async Task<PeopleResponse> GetPersonPage(int page) // Get one page from API
        {
            var client = new RestClient("https://swapi.dev/api/");
            var request = new RestRequest($"people/?page={page}", DataFormat.Json);
            var response = await client.GetAsync<PeopleResponse>(request);
            return response;
        }
        private static Task<PeopleResponse> CreateTask(int i) // Create task to run several requests async
        {
            return Task.Run(() => GetPersonPage(i));
        }
        public async Task<List<Person>> GetAllPersons() // Return list of all persons in API
        {
            var p = await GetPersonPage(1);
            int numberOfPersons = p.Results.Count;
            int personsPerOnePage = p.Results.Count;
            int numberOfPages = (int)Math.Ceiling(1.0 * numberOfPersons / personsPerOnePage); // Use the number of persons and persons per page to calculate number of pages

            var tasks = new List<Task<PeopleResponse>>();
            for (int i = 1; i < numberOfPages + 1; i++) // For each page create a task to run all pages at the same time.
            {
                tasks.Add(CreateTask(i));
            }

            List<PeopleResponse> temp = new();
            List<Person> results = new();

            for (int i = 0; i < numberOfPages; i++)
            {
                temp.AddRange((IEnumerable<PeopleResponse>)tasks[i].Result.Results); // Add each page result to list
            }
            foreach(var people in temp)
            {
                results.AddRange(people.Results);
            }
            return results;
        }
        /* public List<string> PeopleList { get; set; }

         public StarwarsPeople()
         {
             PeopleList = new List<string>();
         }

         // Download one page of names to PeopleList.
         public async Task<PeopleResponse> GetPeopleFromPageAsync(int page)
         {
             var client = new RestClient(basicURL);
             var request = new RestRequest($"people/?page={page}", DataFormat.Json);

             var peopleResponse = await client.GetAsync<PeopleResponse>(request);

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
                 var peopleResponse = Task.Run(async () => await client.GetAsync<PeopleResponse>(request));

                 if (peopleResponse.Result.Count != 0)
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
         public async void DownloadPeopleAsync(IEnumerable<int> pages)
         {
             List<Task<PeopleResponse>> tasks = new List<Task<PeopleResponse>>();

             foreach (var page in pages)
             {
                 Task<PeopleResponse> task = GetPeopleFromPageAsync(page);
                 tasks.Add(task);
             }

             Task.WaitAll(tasks.ToArray());

             foreach (var task in tasks)
             {
                 foreach (var p in task.Result.Results)
                 {
                     this.PeopleList.Add(p.Name);
                 }
             }
         }*/
    }
}
