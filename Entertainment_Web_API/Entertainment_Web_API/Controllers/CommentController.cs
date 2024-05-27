using Microsoft.AspNetCore.Mvc;
using BackEnd.Models;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Entertainment_Web_API.Controllers
{
    public class CommentController : Controller
    {

        //[HttpGet]
        //public async Task<IActionResult> GetComment(string videoId)
        //{
        //    using(var http = new HttpClient())
        //    {

        //        var respone = await http.GetAsync("https://localhost:7142/backend/Comment/GetComment/" + videoId);
        //        if (respone.IsSuccessStatusCode)
        //        {
        //            var jsonstring = await respone.Content.ReadAsStringAsync();
        //            var comments = JsonConvert.DeserializeObject<List<Comment>>(jsonstring);
        //            ViewBag.comments = comments;
        //        }
        //        return BadRequest("Comment Not Found");
        //    }
        //}

        [HttpPut]
        public async Task<IActionResult> UpdateComment(string commentId, Comment model, string videoId)
        {
            using (var http = new HttpClient())
            {
                var respone = await http.PutAsJsonAsync("https://localhost:7142/backend/Comment/AddComment/", model);
                if (respone.IsSuccessStatusCode)
                {
                    return RedirectToAction("Video","Home", new { id = videoId});
                }
                return BadRequest("Update fail!");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromForm]Comment model)
        {
            using(var http = new HttpClient())
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var respone = await http.PostAsJsonAsync($"https://localhost:7142/backend/Comment/AddComment/{userId}/{model.VideoId}/{model.Content}", new { });
                if (!respone.IsSuccessStatusCode)
                {
                    return BadRequest("Add comment fail!");

                }
                return RedirectToAction("Video","Home", new {id = model.VideoId});
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComment([FromForm] Comment model)
        {
            using(var http = new HttpClient())
            {
                var respone = await http.DeleteAsync("https://localhost:7142/backend/Comment/DeleteComment/" + model.CommentId);
                if (!respone.IsSuccessStatusCode)
                {
                    return BadRequest("Add comment fail!");

                }
                return RedirectToAction("Video", "Home", new { id = model.VideoId });
            }
        }
    }
}
