using BackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Entertainment_Web_API.Controllers
{
    public class HomeController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7142/backend");
        private readonly HttpClient _client;

        public HomeController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;

        }

        public async Task<IActionResult> NoAccount(string searchTerm = "music")
        {
            List<Video> videoList = new List<Video>();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/Video/Get/{searchTerm}");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                videoList = JsonConvert.DeserializeObject<List<Video>>(data);
            }
            return View(videoList);
        }

        //[Authorize]
        public async Task<IActionResult> Index(string searchTerm = "music")
        {
            List<Video> videoList = new List<Video>();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/Video/Get/{searchTerm}");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                videoList = JsonConvert.DeserializeObject<List<Video>>(data);
            }
            return View(videoList);
        }

        //[Authorize]
        public async Task<IActionResult> Video(string id)
        {
            Video video = new Video();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/Video/GetDetails/{id}");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                video = JsonConvert.DeserializeObject<Video>(data);
            }
            return View(video);
        }

        [Authorize]
        public IActionResult Music()
        {
            return View();
        }

        //[Authorize]
        public IActionResult MusicDetail()
        {
            return View();
        }

        [Authorize]
        //[Authorize(Roles = "Admin, User")]
        //[Authorize(Roles = "Admin")]
        public IActionResult Library()
        {
            return View();
        }
    }
}