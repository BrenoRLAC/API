using System.Security.Cryptography;
using System.Text;

namespace API.Constants
{
    public class Constant
    {
        public static string GetKey()
        {
            string key = "73ctu$wN7wcAzZ$@$a%@K^h26TMpNyoPym6oyM5m7d4%QHW7ZzeQV2CiNUnCtbJ!LY4cGdqg^Zfadx^4AHGfwa9aN^WptHmMBdvVgZfRxS4ABn6stsVRBx6QXzBLj8t&\"";

            byte[] originalBytes = Encoding.UTF8.GetBytes(key);

            Array.Resize(ref originalBytes, 32);

            string resizedKey = Encoding.UTF8.GetString(originalBytes);

            return resizedKey;
        }

        public static string TunnelKey = new string("73ctu$wN7wcAzZ$@$a%@K^h26TMpNyoPym6oyM5m7d4%QHW7ZzeQV2CiNUnCtbJ!LY4cGdqg^Zfadx^4AHGfwa9aN^WptHmMBdvVgZfRxS4ABn6stsVRBx6QXzBLj8t&".Reverse().ToArray());

    }
}

