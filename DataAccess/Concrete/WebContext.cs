using ENTITIES;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Text;

namespace DATA.Concrete
{

    public class WebContext : DbContext
    {
        public DbSet<MEDICINE> MEDICINE { get; set; }
        public DbSet<PHARMACIES> PHARMACIES { get; set; }
        public DbSet<PRESCRIPTION> PRESCRIPTIONS { get; set; }


        public DbSet<STOCK> STOCK { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-VISABIIJ;Database=Eczane;User Id=sa;Password=berra123;");
        }

    }
}

