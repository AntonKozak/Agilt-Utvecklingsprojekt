using System.Net.Http;
using System.Text.Json;
using agilt_projekt.web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace agilt_projekt.web.Controllers;

[Route("events")]
    public class EventsController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClient;
        private readonly JsonSerializerOptions _options;
        
        private readonly string _baseUrl;
        
    public EventsController(IConfiguration config, IHttpClientFactory httpClient)
    {
            _httpClient = httpClient;
            _config = config;
            _baseUrl = _config.GetSection("apiSettings:baseUrl").Value;
            _options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
    }

    

    //TASK make method awaited resultat the sane IActionResult(result HTTP req.)
    [HttpGet()]
        public async Task<IActionResult> Index()
        {
          using var client = _httpClient.CreateClient();
          var response = await client.GetAsync($"{_baseUrl}/Event");
        
        if (!response.IsSuccessStatusCode) return Content("Error");

        var json = await response.Content.ReadAsStringAsync();

        var listOfEvents = JsonSerializer.Deserialize<IList<EventsListViewModel>>(json, _options);

            return View("Index", listOfEvents);
        }
    }