﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class PeopleResponse
    {
        public int Count { get; set; }
        public List<Person> Results { get; set; }
    }

    public class Person
    {
        public string Name { get; set; }
    }
}
