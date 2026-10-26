using AIServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace AIServices.Controllers
{
    public class vbEditor : Controller
    {
        private readonly IAIService _aiService;
        public vbEditor(IAIService aiService)
        {
            _aiService = aiService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Generate(string requirement)
        {
            string prompt = $@""" 
       Act as a Visual Basic expert. 
             correct the code or create a new one for the following requirement. 
             Requirement: {requirement} 
             Return only the Visual Basic code. 
             """;
            string vbCode = await _aiService.GetResponseAsync(prompt);
            ViewBag.VBCode = vbCode;
            return View("Index");
        }
    }
}
