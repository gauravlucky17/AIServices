using AIServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace AIServices.Controllers
{
    public class ChatController : Controller
    {
        private readonly IAIService _aiService;
        public ChatController(IAIService aiService)
        {
            _aiService = aiService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Ask(string question)
        {
            var response = await _aiService.GetResponseAsync(question);
            ViewBag.Response = response;
            return View("Index");
        }
    }
}
