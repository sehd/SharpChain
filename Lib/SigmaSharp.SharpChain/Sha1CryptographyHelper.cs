using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SigmaSharp.SharpChain
{
    internal class Sha1CryptographyHelper : ICryptographyHelper<byte[]>
    {
        SHA1 hashFunction;

        public Sha1CryptographyHelper()
        {
            hashFunction = SHA1.Create();
        }

        public bool CompareHash(byte[] hash1, byte[] hash2)
        {
            if (hash1.Length != hash2.Length || hash1.Length != hashFunction.HashSize / 8)
                return false;

            else
            {
                var res = true;
                for (int i = 0; i < hash1.Length; i++)
                {
                    res &= hash1[i] == hash2[i];
                }
                return res;
            }
        }

        public byte[] GetDefaultHash()
        {
            return new byte[0];
        }

        public byte[] GetHash<TContent>(Block<TContent, byte[]> input)
        {
            var data = new List<byte>();
            var content = JsonConvert.SerializeObject(input.Content);
            data.AddRange(Encoding.UTF8.GetBytes(content));
            data.AddRange(input.PreviousHash);
            return hashFunction.ComputeHash(data.ToArray());
        }
    }
}
