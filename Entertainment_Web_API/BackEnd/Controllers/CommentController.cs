using BackEnd.Data;
using BackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackEnd.Controllers
{
    [Route("backend/[controller]/[action]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly EntertainmentContext _context;
        public CommentController(EntertainmentContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> AddComment([FromForm]Comment model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var video = await _context.Videos.FindAsync(model.VideoId);
            if (video != null)
            {
                video = new Video
                {
                    VideoId = video.VideoId,
                    Id = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };

                _context.Videos.Add(video);
                await _context.SaveChangesAsync();
            }

            var comment = new Comment
            {
                CommentId = Guid.NewGuid().ToString(),
                Content = model.Content,
                CommentPostingTime = DateOnly.FromDateTime(DateTime.Now),
                VideoId = model.VideoId,
                Id = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };
            _context.Comments.Add(comment);
            var result = await _context.SaveChangesAsync();
            if(result <= 0)
            {
                return BadRequest("Save fail!");
            }
            return Ok(comment); 

        }
        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteComment(string commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId);
            if(comment == null)
            {
                return BadRequest("Not found");
                
            }
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();

            return Ok("delete successfully!");
            
            
            
        }
    }
}
