using Newtonsoft.Json;

namespace API.Domain.Notification
{
    public class NotificationRequest
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("contentMessage")]
        public string ContentMessage { get; set; }                    
        public List<(string, string)> ReturnUsers { get; set; }
    }
}