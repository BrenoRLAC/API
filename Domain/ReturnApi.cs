using Newtonsoft.Json;

namespace API.Domain
{
    public class ReturnApi<T>
    {
        public ReturnApi()
        {
        }

        public ReturnApi(int status, T data)
        {
            StatusCode = status;
            Data = data;
        }

        public ReturnApi(int status, string message)
        {
            StatusCode = status;
            Message = message;
        }


        [JsonProperty("success")]
        public bool Success => StatusCode is >= 200 and <= 299;

        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("code")] public string Code { get; set; }

        [JsonProperty("message")] public string Message { get; set; }

        [JsonProperty("data")] public T Data { get; set; }

    }
}
