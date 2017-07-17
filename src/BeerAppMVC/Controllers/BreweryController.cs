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
                string response = await GetClientResponseAsync("/mnbeer/brewery", token, Startup.APIMSubscriptionId);

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
                string response = await GetClientResponseAsync($"/mnbeer/brewery/{id}", token, Startup.APIMSubscriptionId);

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
                string response = await PostClientReponseAsync("/mnbeer/Brewery", token, JsonConvert.SerializeObject(brewery), Startup.APIMSubscriptionId);

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
                string response = await GetClientResponseAsync($"/mnbeer/Brewery/{id}", token, Startup.APIMSubscriptionId);

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
                string response = await DeleteClientResponseAsync($"/mnbeer/Brewery?id={id}", token, Startup.APIMSubscriptionId);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

    }
}
