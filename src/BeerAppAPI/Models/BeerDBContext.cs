using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerAppAPI.Models
{
    public class BeerDBContext : DbContext
    {
        public DbSet<Beer> Beer { get; set; }
        public DbSet<Brewery> Brewery { get; set; }

        public BeerDBContext(DbContextOptions<BeerDBContext> options) : base(options)
        {

        }
    }
}
