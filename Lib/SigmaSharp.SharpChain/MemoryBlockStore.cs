using System;
using System.Collections.Generic;
using System.Linq;

namespace SigmaSharp.SharpChain
{
    internal class MemoryBlockStore<TContent, THash> : IBlockStore<TContent, THash>
    {
        List<Block<TContent, THash>> store;

        public MemoryBlockStore()
        {
            store = new List<Block<TContent, THash>>();
        }

        public IEnumerable<Block<TContent, THash>> GetAll()
        {
            return store.ToArray();
        }

        public Block<TContent, THash> GetLast()
        {
            return store.LastOrDefault();
        }

        public void Insert(Block<TContent, THash> block)
        {
            store.Add(block);
        }
    }
}
