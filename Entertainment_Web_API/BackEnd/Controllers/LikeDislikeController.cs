using BackEnd.Data;
using BackEnd.Models;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("backend/[controller]/[action]")]
    [ApiController]
    public class LikeDislikeController : ControllerBase
    {
        private readonly EntertainmentContext _context;
        private readonly string apiKey = "AIzaSyBl_ZIe-m8ry0ajAO3-hvchkDlTT6kkgy0"; // Api key

        public LikeDislikeController(EntertainmentContext context)
        {
            _context = context;
        }

        [HttpGet("{videoId}")]
        public async Task<IActionResult> LikeVideo(string videoId)
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
                    Likes = 0,
                    Dislikes = 0,
                };

                // Thêm Video vào cơ sở dữ liệu
                _context.Videos.Add(newVideo);
                await _context.SaveChangesAsync(); // Lưu thay đổi
            }

            var findVideo = await _context.Videos.FindAsync(videoId);
            if (findVideo == null) // Kiểm tra findVideo
            {
                return NotFound();
            }

            findVideo.Likes += 1;
            await _context.SaveChangesAsync();

            return Ok(findVideo.Likes);
        }

        [HttpGet("{videoId}")]
        public async Task<IActionResult> UndoLikeVideo(string videoId)
        {
            var findVideo = await _context.Videos.FindAsync(videoId);
            if (findVideo == null) // Kiểm tra findVideo
            {
                return NotFound();
            }

            findVideo.Likes -= 1;
            await _context.SaveChangesAsync();

            return Ok(findVideo.Likes);
        }

        [HttpGet("{videoId}")]
        public async Task<IActionResult> DislikeVideo(string videoId)
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
                    VideoPostingTime = video.Snippet.PublishedAtDateTimeOffset
                };

                // Thêm Video vào cơ sở dữ liệu
                _context.Videos.Add(newVideo);
            }

            var findVideo = await _context.Videos.FindAsync(videoId);
            if (video == null)
            {
                return NotFound();
            }

            findVideo.Dislikes += 1;
            await _context.SaveChangesAsync();

            return Ok(findVideo.Dislikes);
        }

        [HttpGet("{videoId}")]
        public async Task<IActionResult> UndoDislikeVideo(string videoId)
        {
            var findVideo = await _context.Videos.FindAsync(videoId);
            if (findVideo == null) // Kiểm tra findVideo
            {
                return NotFound();
            }

            findVideo.Dislikes -= 1;
            await _context.SaveChangesAsync();

            return Ok(findVideo.Dislikes);
        }
    }
}
