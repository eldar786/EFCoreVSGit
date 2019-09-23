using EFCore2017.EntityModels;
using EFCore2017.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore2017.EF
{
    public class MyContext : DbContext
    {
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Igra> Igre { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=EFCoreDB;Trusted_Connection=False;MultipleActiveResultSets=true;User ID=sa;Password=Lutrija1;");
        }
    }
}
