using AIServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace AIServices.Controllers
{
    public class EmailGeneratorController : Controller
    {
        private readonly IAIService _aiService;
        public EmailGeneratorController(IAIService aiService)
        {
            _aiService = aiService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Generate(string recipient, string subject, string purpose)
        {
            var prompt = $@"Generate a professional email. 
                 Recipient: {recipient} 
                 Subject: {subject} 
                 Purpose: {purpose} 
                 Provide only the email content.";
            var emailContent = await _aiService.GetResponseAsync(prompt);
            ViewBag.GeneratedEmail = emailContent;
            return View("Index");
        }
    }

}