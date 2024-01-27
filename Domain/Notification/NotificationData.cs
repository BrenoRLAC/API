using System;
using System.IO;
using Newtonsoft.Json;

namespace API.Domain.Notification
{
    public class NotificationData
    {
        [JsonProperty("messageId")] public int MessageId { get; set; }
        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("content")] public string Content { get; set; }                
        [JsonProperty("read")] public bool Read { get; set; }
    
    }
}