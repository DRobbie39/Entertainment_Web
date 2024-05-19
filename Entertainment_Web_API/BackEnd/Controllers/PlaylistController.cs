using BackEnd.Data;
using BackEnd.Models;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BackEnd.Controllers
{
    [Route("backend/[controller]/[action]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly EntertainmentContext _context;
        private readonly string apiKey = "AIzaSyB1jP3WJP2QzQgy4OQDMil-y3neNUD_sD0"; // Api key

        public PlaylistController(EntertainmentContext context)
        {
            _context = context;
        }

        // GET: api/Playlists
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetPlaylists(string userId)
        {          
            var playlists = await _context.Playlist.Where(p => p.Id == userId).ToListAsync();

            if (playlists == null || playlists.Count == 0)
            {
                return NotFound();
            }

            return Ok(playlists);
        }

        [HttpPost("{userId}/{videoId}/{playlistName}")]
        public async Task<IActionResult> CreatePlaylist(string userId, string videoId, string playlistName)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName = this.GetType().ToString()
            });

            // Lấy thông tin video từ YouTube API
            var videoRequest = youtubeService.Videos.List("snippet,statistics");
            videoRequest.Id = videoId;

            var videoResponse = await videoRequest.ExecuteAsync();
            var video = videoResponse.Items[0];

            // Kiểm tra xem video đã tồn tại trong cơ sở dữ liệu hay chưa
            var existingVideo = await _context.Videos.FindAsync(videoId);

            if (existingVideo == null)
            {
                // Tạo mới Video
                var newVideo = new Video
                {
                    VideoId = video.Id,
                    Title = video.Snippet.Title,
                    ThumbnailUrl = video.Snippet.Thumbnails.High.Url,
                    VideoUrl = $"https://www.youtube.com/watch?v={video.Id}",
                    VideoViews = (int?)video.Statistics.ViewCount.GetValueOrDefault(),
                    VideoPostingTime = video.Snippet.PublishedAtDateTimeOffset,
                    Id = userId
                };

                // Thêm Video vào cơ sở dữ liệu
                _context.Videos.Add(newVideo);
            }

            // Tạo mới playlist
            var newPlaylist = new Playlist
            {
                PlaylistId = Guid.NewGuid().ToString(),
                PlaylistName = playlistName,
                Id = userId
            };

            // Thêm playlist vào cơ sở dữ liệu
            _context.Playlist.Add(newPlaylist);

            // Tạo mới VideoPlaylist
            var newVideoPlaylist = new VideoPlaylist
            {
                VideoId = videoId,
                PlaylistId = newPlaylist.PlaylistId
            };

            // Thêm VideoPlaylist vào cơ sở dữ liệu
            _context.VideoPlaylists.Add(newVideoPlaylist);

            await _context.SaveChangesAsync();

            return Ok(newPlaylist);
        }

        // PUT: api/Playlists/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlaylist(string id, [FromBody] string newPlaylistName)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the user's ID
            var playlist = await _context.Playlist.FindAsync(id);
            if (playlist == null || playlist.Id != userId)
            {
                return NotFound();
            }

            playlist.PlaylistName = newPlaylistName;
            _context.Entry(playlist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaylistExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Playlists/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaylist(string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the user's ID
            var playlist = await _context.Playlist.FindAsync(id);
            if (playlist == null || playlist.Id != userId)
            {
                return NotFound();
            }

            _context.Playlist.Remove(playlist);
            await _context.SaveChangesAsync();

            return Ok(playlist);
        }

        private bool PlaylistExists(string id)
        {
            return _context.Playlist.Any(e => e.PlaylistId == id);
        }
    }
}
