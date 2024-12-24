using System.Collections.Generic;

namespace NotificationAPI.Models
{
    public class RequestMessageByTokens
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Priority { get; set; }
        public List<string> Tokens { get; set; }
    }
}
