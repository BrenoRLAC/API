using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace API.Utilities
{
    public static class AssistantHelpers
    {
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
