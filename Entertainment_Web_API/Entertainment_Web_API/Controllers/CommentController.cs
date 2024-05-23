using Microsoft.AspNetCore.Mvc;
using BackEnd.Models;

namespace Entertainment_Web_API.Controllers
{
    public class CommentController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> AddComment([FromForm]Comment model)
        {
            using(var http = new HttpClient())
            {
                var respone = await http.PostAsJsonAsync("https://localhost:7142/backend/Comment/AddComment/", model);
                if (!respone.IsSuccessStatusCode)
                {
                    return BadRequest("Add comment fail!");

                }
                return RedirectToAction("Video","Home", new {id = model.VideoId});
            }

        }
        [HttpGet]
        public async Task<IActionResult> DeleteComment(string commentId, string videoId)
        {
            using(var http = new HttpClient())
            {
                var respone = await http.DeleteAsync("https://localhost:7142/backend/Comment/DeleteComment/" + commentId);
                if (!respone.IsSuccessStatusCode)
                {
                    return BadRequest("Add comment fail!");

                }
                return RedirectToAction("Video", "Home", new { id = videoId });
            }
        }
    }
}
