﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1
{
    public class SpaceContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=StoreDB;");
        }
        public virtual DbSet<Parkings> Parkings { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Spaceport> Spaceports { get; set; }
    }
}
