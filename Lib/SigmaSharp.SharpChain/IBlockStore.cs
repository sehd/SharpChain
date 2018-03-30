using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaSharp.SharpChain
{
    public interface IBlockStore<TContent, THash>
    {
        /// <summary>
        /// Stores a block
        /// </summary>
        /// <param name="block">The block to store</param>
        void Insert(Block<TContent, THash> block);

        /// <summary>
        /// Gets latest inserted block in the store
        /// </summary>
        /// <returns>The latest inserted block</returns>
        Block<TContent, THash> GetLast();

        /// <summary>
        /// Get all stored blocks
        /// </summary>
        /// <returns>An enumerable containing all stored blocks</returns>
        IEnumerable<Block<TContent, THash>> GetAll();
    }
}
