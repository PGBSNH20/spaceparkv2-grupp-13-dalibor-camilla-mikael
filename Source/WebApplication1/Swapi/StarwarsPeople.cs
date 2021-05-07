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
        private const string baseURL = "http://swapi.dev/api/";

        //Fetch people from API
        public static async Task<List<Person>> GetPeople()
        {
            var client = new RestClient(baseURL);
            string requestUrl = $"http://swapi.dev/api/people/";
            PeopleResponse response;
            List<Person> persons = new List<Person>();

            while (requestUrl != null)
            {
                string resource = requestUrl.Substring(baseURL.Length);
                var request = new RestRequest(resource, DataFormat.Json);
                response = await client.GetAsync<PeopleResponse>(request);

                persons.AddRange(response.Results);
                requestUrl = response.Next;
            }
            return persons;
        }
    }
}
