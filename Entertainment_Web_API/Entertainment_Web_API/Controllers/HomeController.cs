using BackEnd.Models;
//using Google.Apis.YouTube.v3.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Security.Claims;

namespace Entertainment_Web_API.Controllers
{
    public class HomeController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7142/backend");
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(IHttpContextAccessor httpContextAccessor)
        {
            _client = new HttpClient();
            _httpContextAccessor = httpContextAccessor;
            _client.BaseAddress = baseAddress;

        }

        private string GetCurrentUserId()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext.User.Identity.IsAuthenticated)
            {
                return httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
            // Nếu người dùng không đăng nhập, trả về null hoặc xử lý tùy theo yêu cầu
            return null;
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

            // Get related videos
            HttpResponseMessage relatedResponse = await _client.GetAsync(_client.BaseAddress + $"/Video/GetRelatedVideos/{id}");
            if (relatedResponse.IsSuccessStatusCode)
            {
                string relatedData = await relatedResponse.Content.ReadAsStringAsync();
                ViewBag.RelatedVideos = JsonConvert.DeserializeObject<List<Video>>(relatedData);
            }

            // Get playlists
            var userId = GetCurrentUserId();
            List<Playlist> playlists = new List<Playlist>(); // Khai báo biến playlists ở đây
            if (userId != null)
            {
                HttpResponseMessage playlistResponse = await _client.GetAsync(_client.BaseAddress + $"/Playlist/GetPlaylists/{userId}");
                if (playlistResponse.IsSuccessStatusCode)
                {
                    string playlistData = await playlistResponse.Content.ReadAsStringAsync();
                    playlists = JsonConvert.DeserializeObject<List<Playlist>>(playlistData); // Gán giá trị cho biến playlists ở đây
                }
            }

            // Create a new ViewModel
            var viewModel = new VideoPlaylistViewModel
            {
                Video = video,
                Playlists = playlists
            };

            return View(viewModel);
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
        public async Task<IActionResult> Library()
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                // Xử lý trường hợp người dùng không đăng nhập
                return RedirectToAction("Login", "Account");
            }

            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/Playlist/GetPlaylists/{userId}");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var playlists = JsonConvert.DeserializeObject<List<Playlist>>(data);
                return View(playlists);
            }
            
            return View();
        }
    }
}