using System.Collections.Generic;
using Newtonsoft.Json;

namespace API.Domain.Notification
{
    public class NotificationResult
    {
        [JsonProperty("all")]
        public List<NotificationData> All { get; set; }

        [JsonProperty("unread")]
        public List<NotificationData> UnRead { get; set; }
    }
}