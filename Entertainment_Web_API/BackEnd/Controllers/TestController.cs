using BackEnd.Data;
using BackEnd.Models;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Controllers
{
    [Route("backend/[controller]/[action]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        //private readonly string apiKey = "AIzaSyAuZA_D-OtOTUf_ONND6E949BO8jWwi-BQ"; // Api key
        //private readonly EntertainmentContext _context;

        //public LikeController(EntertainmentContext context)
        //{
        //    _context = context;
        //}

        //private async Task<Video> GetOrCreateVideoAsync(string videoId)
        //{
        //    // Kiểm tra xem video đã tồn tại trong cơ sở dữ liệu hay chưa
        //    var existingVideo = await _context.Videos.FindAsync(videoId);
        //    if (existingVideo != null)
        //    {
        //        return existingVideo;
        //    }

           
        //        // Lấy thông tin video từ YouTube API
        //        var youtubeService = new YouTubeService(new BaseClientService.Initializer()
        //        {
        //            ApiKey = apiKey,
        //            ApplicationName = this.GetType().ToString()
        //        });

        //        var videoRequest = youtubeService.Videos.List("snippet,statistics");
        //        videoRequest.Id = videoId;

        //        var videoResponse = await videoRequest.ExecuteAsync();
        //        var video = videoResponse.Items[0];

        //        // Tạo mới Video
        //        var newVideo = new Video
        //        {
        //            VideoId = video.Id,
        //            Title = video.Snippet.Title,
        //            ThumbnailUrl = video.Snippet.Thumbnails.High.Url,
        //            VideoUrl = $"https://www.youtube.com/watch?v={video.Id}",
        //            VideoPostingTime = video.Snippet.PublishedAtDateTimeOffset,
        //            Likes = 0,
        //            Dislikes = 0
        //        };

        //        // Thêm Video vào cơ sở dữ liệu
        //        _context.Videos.Add(newVideo);
        //        await _context.SaveChangesAsync();

        //        return newVideo;
        //}

        //[HttpPost("{videoId}")]
        //public async Task<IActionResult> Like(string videoId)
        //{
        //    var video = await GetOrCreateVideoAsync(videoId);

        //    // Cập nhật số lượt thích
        //    video.Likes = video.Likes;

        //    _context.Videos.Update(video);
        //    await _context.SaveChangesAsync();

        //    return Ok(new { video.Likes }); // Trả về số lượt thích mới
        //}

        //[HttpPost("{videoId}")]
        //public async Task<IActionResult> Dislike(string videoId)
        //{
        //    var video = await GetOrCreateVideoAsync(videoId);

        //    // Cập nhật số lượt không thích
        //    video.Dislikes++; _context.Videos.Update(video);
        //    await _context.SaveChangesAsync();

        //    return Ok(new { Dislikes = video.Dislikes }); // Trả về số lượt không thích mới
        //}
    }
}
