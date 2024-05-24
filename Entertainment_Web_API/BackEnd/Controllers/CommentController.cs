using BackEnd.Data;
using BackEnd.Models;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
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
        private readonly string apiKey = "AIzaSyBl_ZIe-m8ry0ajAO3-hvchkDlTT6kkgy0"; // Api key
        public CommentController(EntertainmentContext context)
        {
            _context = context;
        }

        [HttpPost("{userId}/{videoId}/{content}")]
        public async Task<IActionResult> AddComment(string userId, string videoId, string content)
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

            // Tạo mới comment
            var newComment = new Comment
            {
                CommentId = Guid.NewGuid().ToString(),
                Content = content,
                CommentPostingTime = DateOnly.FromDateTime(DateTime.Now),
                Id = userId,
                VideoId = videoId
            };

            // Thêm comment vào cơ sở dữ liệu
            _context.Comments.Add(newComment);

            await _context.SaveChangesAsync();

            return Ok(newComment);
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
