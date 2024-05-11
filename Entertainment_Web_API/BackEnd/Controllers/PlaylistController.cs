using BackEnd.Data;
using BackEnd.Models;
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

        public PlaylistController(EntertainmentContext context)
        {
            _context = context;
        }

        // GET: api/Playlists
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetPlaylists(string userId)
        {
            try
            {
                var playlists = await _context.Playlist
                    .Where(p => p.Id == userId)
                    .ToListAsync();

                if (playlists == null || playlists.Count == 0)
                {
                    return NotFound();
                }

                return Ok(playlists);
            }
            catch (Exception ex)
            {
                // Log lỗi và trả về lỗi server nếu có vấn đề
                return StatusCode(500, "Có lỗi xảy ra khi lấy danh sách playlist");
            }
        }

        //FromBody ở đây sẽ gửi PlaylistName qua bên backend xử lý
        // POST: api/Playlists
        [HttpPost]
        public async Task<IActionResult> CreatePlaylist([FromBody] string playlistName)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the user's ID
            var playlist = new Playlist { PlaylistName = playlistName, Id = userId };
            _context.Playlist.Add(playlist);
            await _context.SaveChangesAsync();

            return Ok(playlist);
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
