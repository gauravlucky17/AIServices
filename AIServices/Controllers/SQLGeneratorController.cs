using AIServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace AIServices.Controllers
{
    public class SQLGeneratorController : Controller
    {
        private readonly IAIService _aiService;
        public SQLGeneratorController(IAIService aiService)
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
       Act as a SQL Server expert. 
             Generate a SQL Server query for the following requirement. 
             Requirement: {requirement} 
             Return only the SQL query. 
             """;
            string sqlQuery = await _aiService.GetResponseAsync(prompt);
            ViewBag.SQLQuery = sqlQuery;
            return View("Index");
        }
    }
}
