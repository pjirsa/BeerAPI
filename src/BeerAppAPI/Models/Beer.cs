using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerAppAPI.Models
{
    public class Beer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double ABV { get; set; }
        public int BreweryId { get; set; }
        public virtual Brewery Brewery { get; set; }
    }
}
