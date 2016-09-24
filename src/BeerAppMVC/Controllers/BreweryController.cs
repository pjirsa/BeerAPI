using BeerAppAPI.Models;
using BeerAppMVC.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BeerAppMVC.Controllers
{
    [Authorize]
    public class BreweryController : BaseController
    {

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            List<Brewery> items = new List<Brewery>();

            try
            {
                string token = await HttpContext.GetAuthTokenAsync(Startup.BeerAPIResourceId);
                string response = await GetClientResponseAsync("/api/brewery", token);

                var responseItems = JsonConvert.DeserializeObject<List<Brewery>>(response);
                items.AddRange(responseItems);

                return View(items);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                string token = await HttpContext.GetAuthTokenAsync(Startup.BeerAPIResourceId);
                string response = await GetClientResponseAsync($"/api/brewery/{id}", token);

                var responseItem = JsonConvert.DeserializeObject<Brewery>(response);

                return View(responseItem);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> Add(string name, string website, string address)
        {
            try
            {
                var brewery = new Brewery
                {
                    Name = name,
                    Website = website,
                    Address = address
                };

                string token = await HttpContext.GetAuthTokenAsync(Startup.BeerAPIResourceId);
                string response = await PostClientReponseAsync("/api/Brewery", token, JsonConvert.SerializeObject(brewery));

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                string token = await HttpContext.GetAuthTokenAsync(Startup.BeerAPIResourceId);
                string response = await GetClientResponseAsync($"/api/Brewery/{id}", token);

                var item = JsonConvert.DeserializeObject<Brewery>(response);

                return View(item);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> DeleteConfirm(int id)
        {
            try
            {
                string token = await HttpContext.GetAuthTokenAsync(Startup.BeerAPIResourceId);
                string response = await DeleteClientResponseAsync($"/api/Brewery?id={id}", token);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

    }
}
