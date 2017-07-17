using BeerAppAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerAppAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class BreweryController : Controller
    {
        private BeerDBContext _context;

        public BreweryController(BeerDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Brewery> Get()
        {
            return _context.Brewery.Include(b => b.Beers).OrderBy(b => b.Name);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = _context.Brewery.Include(b => b.Beers).FirstOrDefault(x => x.Id == id);
            if (item == null) return NotFound();

            return Ok(item);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody]Brewery model)
        {
            _context.Brewery.Add(model);
            _context.SaveChanges();

            return Created(Request.Path + $"/{model.Id}", model);
        }

        [Authorize]
        [HttpPut]
        public IActionResult Put(int id, [FromBody]Brewery model)
        {
            var item = _context.Brewery.FirstOrDefault(x => x.Id == id);

            if (item == null)
                return NotFound();

            item.Address = model.Address;
            item.Name = model.Name;
            item.Website = model.Website;

            _context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return Ok(item);
        }

        [Authorize]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var item = _context.Brewery.FirstOrDefault(x => x.Id == id);
            if (item == null) return NotFound();
            _context.Brewery.Remove(item);
            _context.SaveChanges();
            return Ok(item);
        }
    }
}
