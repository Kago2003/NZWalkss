using Microsoft.AspNetCore.Mvc;
using NZWalkss.UI.Models;
using NZWalkss.UI.Models.DTO;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace NZWalkss.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task <IActionResult> Index()
        {
            List<RegionsDto> response = new List<RegionsDto>();
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7124/api/regions");

                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange (await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionsDto>>());
            }
            catch (Exception)
            {

            }
            return View(response);
        }

        [HttpGet]
        public IActionResult Add() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionsViewModel model)
        {
            var client = httpClientFactory.CreateClient();

            var httpsRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7124/api/regions"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

             var httpResponseMessage = await client.SendAsync(httpsRequestMessage);

             httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionsDto>();

            if(response == null)
            {
                return RedirectToAction("Index", "Regions");
            }

            return View();
        }

        [HttpGet]
        public async Task <IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<RegionsDto>($"https://localhost:7124/api/regions/{id.ToString()}");

            if(response is not null) 
            {
                return View(response);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegionsDto request)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7124/api/regions/{request}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8 ,"application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionsDto>();

            if(response is not null) 
            {
                return RedirectToAction("Edit", "Regions");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RegionsDto request)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync($"https://localhost:7124/api/regions/{request}");

                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Regions");
            }
            catch (Exception ex)
            {
                //console
            }
            return View("Edit");
        }
    }
}
