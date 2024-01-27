using API.Constants;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;




namespace API.Utilities
{
    public static class AssistantHelpers
    {
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static string EncryptInt(this int value)
        {
            return value.ToString().Encrypt();
        }

        private static AesCryptoServiceProvider _crytoProvider;
        private static AesCryptoServiceProvider CryptoProvider
        {
            get
            {
                if (_crytoProvider != null) return _crytoProvider;

                var md5HashProvider = new MD5CryptoServiceProvider();
                var keyHash = md5HashProvider.ComputeHash(Encoding.UTF8.GetBytes(Constant.TunnelKey));

                _crytoProvider = new AesCryptoServiceProvider();
                _crytoProvider.Key = keyHash;
                _crytoProvider.Mode = CipherMode.ECB;
                _crytoProvider.Padding = PaddingMode.PKCS7;

                return _crytoProvider;
            }
        }

        public static string Encrypt(this string value)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            byte[] inArray = CryptoProvider.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length);
            return Convert.ToBase64String(inArray);
        }

        private static ICryptoTransform _decryptor;
        private static ICryptoTransform Decryptor => _decryptor ?? (_decryptor = CryptoProvider.CreateDecryptor());
        public static string Decrypt(this string value)
        {
            byte[] bytes = Convert.FromBase64String(value);
            byte[] inArray = Decryptor.TransformFinalBlock(bytes, 0, bytes.Length);

            return Encoding.UTF8.GetString(inArray);
        }

        public static int DecryptInt(this string value)
        {
            return Convert.ToInt32(value.Decrypt());
        }

        public static string PassDecrypt(this string value)
        {
            byte[] bytes = Convert.FromBase64String(value);

            byte[] key = Encoding.UTF8.GetBytes(Constant.GetKey());
          
            string decryptedBytes = DecryptStringFromBytesAes(bytes, key);

            return decryptedBytes;
        }

        static byte[] EncryptStringToBytesAes(string plainText, byte[] Key)
        {
            byte[] encrypted;
            byte[] IV;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;

                aesAlg.GenerateIV();
                IV = aesAlg.IV;

                aesAlg.Mode = CipherMode.ECB;                

                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            var combinedIvCt = IV.Concat(encrypted).ToArray();

            return combinedIvCt;

        }

        static string DecryptStringFromBytesAes(byte[] cipherTextCombined, byte[] Key)
        {
            string plaintext = null;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;

                byte[] IV = new byte[aesAlg.BlockSize / 8];
                byte[] cipherText = new byte[cipherTextCombined.Length - IV.Length];

                Array.Copy(cipherTextCombined, IV, IV.Length);
                Array.Copy(cipherTextCombined, IV.Length, cipherText, 0, cipherText.Length);

               
                aesAlg.IV = IV;

                aesAlg.Mode = CipherMode.ECB;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }
       
    }
}
