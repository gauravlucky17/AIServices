using AIServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace AIServices.Controllers
{
    public class DocumentationController : Controller
    {
        private readonly IAIService _aiService;
        private readonly ILogger<DocumentationController> _logger;
        public DocumentationController(IAIService aiService, ILogger<DocumentationController> logger)
        {
            _aiService = aiService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Generate(string topic)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(topic))
                {
                    _logger.LogInformation("Documentation generation attempted with empty topic.");
                    ViewBag.Error = "Please enter a topic.";
                    return View("Index");
                }
                _logger.LogInformation("Documentation generation started for topic: {Topic}", topic);
                string prompt = $@""" 
 Generate documentation for: {topic} 
 Include: 
  Introduction 
  Features 
          Advantages 
   Disadvantages 
          Example 
         """;
                string documentation = await _aiService.GetResponseAsync(prompt);
                ViewBag.Documentation = documentation;
                _logger.LogInformation("Documentation generated successfully for topic: {Topic}", topic);
                return View("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while generating documentation for topic: {Topic}", topic);
                ViewBag.Error = "An error occurred while generating the documentation. Please try again later.";
                return View("Index");
            }
        }
    }
}
