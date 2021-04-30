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
        public List<string> PeopleList { get; set; }
        private const string basicURL = "http://swapi.dev/api/";

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
        }
    }
}
