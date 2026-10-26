using AIServices.Services;

namespace AIServices.Models
{
    public class SQLGeneratorController
    {
        private readonly IAIService _aiService;
        public SQLGeneratorController(IAIService aiService)
        {
            _aiService = aiService;
        }
    }
}
