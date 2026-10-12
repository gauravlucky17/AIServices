namespace AIServices.Models
{
    public class ResponseModel
    {
        public List<Choice> choices { get; set; }
    }
    public class Choice
    {
        public ResponseMessage message { get; set; }
    }
    public class ResponseMessage
    {
        public string content { get; set; }
    }
}
