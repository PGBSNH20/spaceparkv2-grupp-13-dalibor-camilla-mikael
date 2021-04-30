using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class StarshipResponse
    {
        public int Count { get; set; }
        public List<Starship> Results { get; set; }
    }

    public class Starship
    {
        public string Name { get; set; }
        public decimal Length { get; set; }
        public string Driver { get; set; }
    }
}
