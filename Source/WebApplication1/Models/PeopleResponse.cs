using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Swapi;

namespace WebApplication1.Models
{
    public class PeopleResponse
    {
        public int Count { get; set; }
        public List<Person> Results { get; set; }
    }

    public class Person : StarwarsPeople
    {
        public string Name { get; set; }
    }
}
