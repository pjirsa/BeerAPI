using BeerAppAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerAppAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class BeerController : Controller
    {
        private BeerDBContext _context;

        public BeerController(BeerDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Beer> Get()
        {
            return _context.Beer;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = _context.Beer.FirstOrDefault(b => b.Id == id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody]Beer beer)
        {
            _context.Beer.Add(beer);
            _context.SaveChanges();

            return Created(Request.Path + $"/{beer.Id}", beer);
        }

        [Authorize]
        [HttpPut]
        public IActionResult Put(int id, Beer beer)
        {
            var item = _context.Beer.FirstOrDefault(b => b.Id == id);
            if (item == null) return NotFound();

            item.ABV = beer.ABV;
            item.BreweryId = beer.BreweryId;
            item.Name = beer.Name;

            _context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return Ok(item);
        }

        [Authorize]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var item = _context.Beer.FirstOrDefault(b => b.Id == id);
            if (item == null) return NotFound();
            _context.Beer.Remove(item);
            _context.SaveChanges();

            return Ok(item);
        }
    }
}
