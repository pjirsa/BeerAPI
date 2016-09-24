using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerAppAPI.Models
{
    public class Brewery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public virtual IList<Beer> Beers { get; set; }
    }
}
