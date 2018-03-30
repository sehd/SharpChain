using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaSharp.SharpChain
{
    public static class BlockFactory
    {
        public static Block<TContent, THash> Create<TContent, THash>(TContent content, THash previousHash, ICryptographyHelper<THash> cryptographyHelper)
        {
            var res = new Block<TContent, THash>()
            {
                Content = content,
                PreviousHash = previousHash
            };
            var hash = cryptographyHelper.GetHash(res);
            res.Hash = hash;
            return res;
        }
    }
}
