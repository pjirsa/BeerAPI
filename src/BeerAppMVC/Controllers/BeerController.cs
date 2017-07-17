using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeerAppMVC.Utils;
using BeerAppAPI.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace BeerAppMVC.Controllers
{
    [Authorize]
    public class BeerController : BaseController
    {

        public async Task<IActionResult> Index()
        {
            List<Beer> items = new List<Beer>();

            try
            {
                string token = await HttpContext.GetAuthTokenAsync(Startup.BeerAPIResourceId);
                string response = await GetClientResponseAsync("/mnbeer/beer", token, Startup.APIMSubscriptionId);

                var responseItems = JsonConvert.DeserializeObject<List<Beer>>(response);
                items.AddRange(responseItems);

                return View(items);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> Add(string name, double abv, int breweryId)
        {
            try
            {
                var beer = new Beer
                {
                    Name = name,
                    ABV = abv,
                    BreweryId = breweryId
                };

                var token = await HttpContext.GetAuthTokenAsync(Startup.BeerAPIResourceId);
                var response = await PostClientReponseAsync("/mnbeer/beer", token, JsonConvert.SerializeObject(beer), Startup.APIMSubscriptionId);

                return RedirectToAction("Detail", "Brewery", new { id = breweryId });
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
                var token = await HttpContext.GetAuthTokenAsync(Startup.BeerAPIResourceId);
                var response = await GetClientResponseAsync($"/mnbeer/Beer/{id}", token, Startup.APIMSubscriptionId);

                var item = JsonConvert.DeserializeObject<Beer>(response);

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
                var token = await HttpContext.GetAuthTokenAsync(Startup.BeerAPIResourceId);
                var response = await GetClientResponseAsync($"/mnbeer/Beer/{id}", token, Startup.APIMSubscriptionId);

                var item = JsonConvert.DeserializeObject<Beer>(response);

                var deleteResponse = await DeleteClientResponseAsync($"/mnbeer/Beer/{id}", token, Startup.APIMSubscriptionId);

                return RedirectToAction("Detail", "Brewery", new { id = item.BreweryId });
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
    }
}
