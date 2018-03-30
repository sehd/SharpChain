using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaSharp.SharpChain
{
    public class Chain<TContent,THash>
    {
        private IBlockStore<TContent, THash> store;
        private ICryptographyHelper<THash> cryptographyHelper;

        public Chain(IBlockStore<TContent, THash> store,ICryptographyHelper<THash> cryptographyHelper)
        {
            this.store = store;
            this.cryptographyHelper = cryptographyHelper;
        }

        public THash CreateBlock(TContent content)
        {
            var block = BlockFactory.Create(content, GetLatestHash(), cryptographyHelper);
            return block.Hash;
        }

        public bool AddBlock(TContent content, THash hash)
        {
            var previous = store.GetLast();
            if (previous == null)
            {
                AddGenesis(content);
                return true;
            }

            var block = BlockFactory.Create(content, previous.Hash, cryptographyHelper);
            if (cryptographyHelper.CompareHash(hash, block.Hash))
            {
                store.Insert(block);
                return true;
            }
            return false;
        }

        private THash GetLatestHash()
        {
            var latest = store.GetLast();
            if (latest == null)
                return cryptographyHelper.GetDefaultHash();
            return latest.Hash;
        }

        private void AddGenesis(TContent content)
        {
            var block = BlockFactory.Create(content, cryptographyHelper.GetDefaultHash(), cryptographyHelper);
            store.Insert(block);
        }
    }
}
