namespace AIServices.Models
{
    public class Message
    {
        public string role { get; set; }
        public string content { get; set; }
    }
    public class RequestModel
    {
        public string model { get; set; }
        public List<Message> messages { get; set; }
    }
}
