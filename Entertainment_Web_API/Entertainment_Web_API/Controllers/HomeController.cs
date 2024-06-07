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

using CommentModel = BackEnd.Models.Comment;

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
        public async Task<IActionResult> Video(string videoId)
        {
            var userId = GetCurrentUserId();

            Video video = new Video();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/Video/GetDetails/{videoId}");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                video = JsonConvert.DeserializeObject<Video>(data);
            }

            // Lấy các vid liên quan
            HttpResponseMessage relatedResponse = await _client.GetAsync(_client.BaseAddress + $"/Video/GetRelatedVideos/{videoId}");
            if (relatedResponse.IsSuccessStatusCode)
            {
                string relatedData = await relatedResponse.Content.ReadAsStringAsync();
                ViewBag.RelatedVideos = JsonConvert.DeserializeObject<List<Video>>(relatedData);
            }

            // Get playlists
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

            // Lấy các comment
            List<Comment> comments = new List<Comment>(); // Khai báo biến comments ở đây
            if (userId != null)
            {
                HttpResponseMessage commentResponse = await _client.GetAsync(_client.BaseAddress + $"/Comment/GetComments/{videoId}");
                if (commentResponse.IsSuccessStatusCode)
                {
                    string commentData = await commentResponse.Content.ReadAsStringAsync();
                    comments = JsonConvert.DeserializeObject<List<Comment>>(commentData); // Gán giá trị cho biến comments ở đây

                    // Load thông tin người dùng cho mỗi comment
                    foreach (var comment in comments)
                    {
                        comment.AppUser.Id = GetCurrentUserId();
                    }
                }
            }


            // Tạo ViewModel
            var viewModel = new VideoViewModel
            {
                Video = video,
                Playlists = playlists,
                Comments = comments
            };

            return View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> PlaylistDetail(string playlistId)
        {
            var userId = GetCurrentUserId();

            // Lấy các vid trong playlist
            List<Video> videos = new List<Video>();
            if (userId != null && playlistId != null)
            {
                HttpResponseMessage videoResponse = await _client.GetAsync(_client.BaseAddress + $"/Playlist/GetPlaylistVideos/{userId}/{playlistId}");
                if (videoResponse.IsSuccessStatusCode)
                {
                    string videoData = await videoResponse.Content.ReadAsStringAsync();
                    videos = JsonConvert.DeserializeObject<List<Video>>(videoData);
                }
            }

            // Tạo view model
            var viewModel = new PlaylistViewModel
            {
                Videos = videos
            };

            return View(viewModel);
        }

        //[Authorize]
        //public IActionResult MusicDetail()
        //{
        //    return View();
        //}

        [Authorize]
        //[Authorize(Roles = "Admin, User")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Library()
        {
            var userId = GetCurrentUserId();

            // Get playlists
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

            // Tạo view model
            var viewModel = new PlaylistViewModel
            {
                Playlists = playlists
            };

            return View(viewModel);
        }

		public IActionResult Videolike()
		{
			return View();
		}
	}
}