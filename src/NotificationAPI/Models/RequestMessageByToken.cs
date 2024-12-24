namespace NotificationAPI.Models
{
    public class RequestMessageByToken
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Token { get; set; }
        public string Priority { get; set; }
    }
}
