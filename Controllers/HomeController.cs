using Lab_01_DeckOfCards.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lab_01_DeckOfCards.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        async public Task<IActionResult> Index()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://deckofcardsapi.com/api/deck/");
                var response = await client.GetAsync("new/shuffle");
                Response res = await response.Content.ReadAsAsync<Response>();

                var cardResponseMessage = await client.GetAsync($"{res.deck_id}/draw/?count=5");
                CardResponse cardResponse = await cardResponseMessage.Content.ReadAsAsync<CardResponse>();
                return View(cardResponse);
            }
            catch
            {
                return View();
            }          
        }

        async public Task<IActionResult> DrawFive(string deck_id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://deckofcardsapi.com/api/deck/");
                var cardResponseMessage = await client.GetAsync($"{deck_id}/draw/?count=5");
                CardResponse cardResponse = await cardResponseMessage.Content.ReadAsAsync<CardResponse>();
                return View("index", cardResponse);
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}