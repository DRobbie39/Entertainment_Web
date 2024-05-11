using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Entertainment_Web_API.Controllers
{
    public class PlaylistController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7142/backend");
        private readonly HttpClient _client;

        public PlaylistController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;

        }

        public async Task<IActionResult> GetPlaylists()
        {
            List<Playlist> playlistList = new List<Playlist>(); // Initialized as an empty list
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/Playlist/GetPlaylists");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                playlistList = JsonConvert.DeserializeObject<List<Playlist>>(data);
            }

            return View(playlistList); // Will return an empty list if no playlists were found
        }

        //FromBody ở đây sẽ lấy phần thân của bên HTTP gửi qua để truyền vào thuộc tính PlaylistName
        public async Task<IActionResult> CreatePlaylist([FromBody] string playlistName)
        {
            var playlist = new Playlist { PlaylistName = playlistName };
            HttpResponseMessage response = await _client.PostAsJsonAsync(_client.BaseAddress + "/Playlist/CreatePlaylist", playlist);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetPlaylists");
            }
            return View(playlist);
        }

        public async Task<IActionResult> UpdatePlaylist(string id, [FromBody] string newPlaylistName)
        {
            var playlist = new Playlist { PlaylistId = id, PlaylistName = newPlaylistName };
            HttpResponseMessage response = await _client.PutAsJsonAsync(_client.BaseAddress + $"/Playlist/UpdatePlaylist/{id}", playlist);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetPlaylists");
            }
            return View(playlist);
        }

        public async Task<IActionResult> DeletePlaylist(string id)
        {
            HttpResponseMessage response = await _client.DeleteAsync(_client.BaseAddress + $"/Playlist/DeletePlaylist/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetPlaylists");
            }
            return NotFound();
        }
    }
}
