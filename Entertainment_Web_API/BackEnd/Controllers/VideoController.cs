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
        private readonly string apiKey = "AIzaSyB1jP3WJP2QzQgy4OQDMil-y3neNUD_sD0"; // Api key

        [HttpGet("{searchTerm}")]
        public async Task<IActionResult> Get(string searchTerm)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName = this.GetType().ToString()
            });

            var searchRequest = youtubeService.Search.List("snippet");
            searchRequest.Q = searchTerm;
            searchRequest.MaxResults = 8;
            searchRequest.Type = "video";
            searchRequest.VideoCategoryId = "10";
            searchRequest.EventType = SearchResource.ListRequest.EventTypeEnum.None;

            var searchResponse = await searchRequest.ExecuteAsync();

            var videos = new List<Video>();

            foreach (var item in searchResponse.Items)
            {
                var videoRequest = youtubeService.Videos.List("snippet,statistics");
                videoRequest.Id = item.Id.VideoId;

                var videoResponse = await videoRequest.ExecuteAsync();
                var video = videoResponse.Items[0];

                videos.Add(new Video
                {
                    VideoId = video.Id,
                    Title = video.Snippet.Title,
                    ThumbnailUrl = video.Snippet.Thumbnails.High.Url,
                    VideoUrl = $"https://www.youtube.com/watch?v={video.Id}",
                    VideoViews = (int?)video.Statistics.ViewCount.GetValueOrDefault(),
                    VideoPostingTime = video.Snippet.PublishedAtDateTimeOffset
                });
            }

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
                VideoUrl = $"https://www.youtube.com/embed/{item.Id}", // Chỉnh embed để phát được video
                VideoViews = (int?)item.Statistics.ViewCount.GetValueOrDefault(),
                Likes = (int?)item.Statistics.LikeCount.GetValueOrDefault(),
                Dislikes = (int?)item.Statistics.DislikeCount.GetValueOrDefault()
            }).FirstOrDefault();

            return Ok(video);
        }

        [HttpGet("{videoId}")]
        public async Task<IActionResult> GetRelatedVideos(string videoId)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName = this.GetType().ToString()
            });

            // Get the current video's details
            var videoRequest = youtubeService.Videos.List("snippet");
            videoRequest.Id = videoId;
            var videoResponse = await videoRequest.ExecuteAsync();
            var currentVideo = videoResponse.Items[0];

            // Search for related videos
            var searchRequest = youtubeService.Search.List("snippet");
            searchRequest.Q = currentVideo.Snippet.Title; // Use the current video's title as the search term
            searchRequest.MaxResults = 5;
            searchRequest.Type = "video"; // Only search for videos

            var searchResponse = await searchRequest.ExecuteAsync();

            var relatedVideos = new List<Video>();

            foreach (var searchResult in searchResponse.Items)
            {
                // Skip if the video is the same as the current video
                if (searchResult.Id.VideoId != videoId)
                {
                    relatedVideos.Add(new Video
                    {
                        VideoId = searchResult.Id.VideoId,
                        Title = searchResult.Snippet.Title,
                        ThumbnailUrl = searchResult.Snippet.Thumbnails.Default__.Url,
                    });
                }
            }

            return Ok(relatedVideos);
        }
    }
}