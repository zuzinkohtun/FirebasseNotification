namespace NotificationAPI.Models
{
    public class RequestMessage
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Topic { get; set; }
        public string Priority { get; set; }
    }
}
