using BackEnd.Models;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("backend/[controller]/[action]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly string apiKey = "AIzaSyC-hldqefETpVzbO8cToIsH9v5PmbP1y-0"; //api key

        [HttpGet("{searchTerm}")]
        public async Task<IActionResult> Get(string searchTerm)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName = this.GetType().ToString()
            });

            var searchRequest = youtubeService.Search.List("snippet");
            searchRequest.Q = searchTerm; // Sử dụng từ khóa từ client
            searchRequest.MaxResults = 50;
            searchRequest.Type = "video"; // Chỉ tìm kiếm video
            searchRequest.VideoCategoryId = "10"; // Chỉ tìm kiếm video thuộc thể loại âm nhạc
            searchRequest.EventType = SearchResource.ListRequest.EventTypeEnum.None; // Loại trừ video trực tiếp

            var searchResponse = await searchRequest.ExecuteAsync();

            var videos = searchResponse.Items.Select(item => new Video
            {
                VideoId = item.Id.VideoId,
                Title = item.Snippet.Title,
                ThumbnailUrl = item.Snippet.Thumbnails.High.Url,
                VideoUrl = $"https://www.youtube.com/watch?v={item.Id.VideoId}" // Lưu URL của video
            }).ToList();

            return Ok(videos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(string id)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName = this.GetType().ToString()
            });

            var videoRequest = youtubeService.Videos.List("snippet,statistics");
            videoRequest.Id = id;

            var videoResponse = await videoRequest.ExecuteAsync();

            var video = videoResponse.Items.Select(item => new Video
            {
                VideoId = item.Id,
                Title = item.Snippet.Title,
                ThumbnailUrl = item.Snippet.Thumbnails.High.Url,
                VideoUrl = $"https://www.youtube.com/embed/{item.Id}",
                VideoViews = (int?)item.Statistics.ViewCount.GetValueOrDefault(),
                Likes = (int?)item.Statistics.LikeCount.GetValueOrDefault(),
                Dislikes = (int?)item.Statistics.DislikeCount.GetValueOrDefault()
            }).FirstOrDefault();

            return Ok(video);
        }
    }
}
