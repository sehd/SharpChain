using System.Collections.Generic;
using System.Linq;

namespace SigmaSharp.SharpChain
{
    public class Chain<TContent, THash>
    {
        private IBlockStore<TContent, THash> store;
        private ICryptographyHelper<THash> cryptographyHelper;

        /// <summary>
        /// Creates an instance of Chain. Don't use directly use <code>ChainFactory.Create()</code> instead.
        /// </summary>
        /// <param name="store">An instance of IBlockStore to store blocks</param>
        /// <param name="cryptographyHelper">An instance of ICryptographyHelper to compute hash</param>
        public Chain(IBlockStore<TContent, THash> store, ICryptographyHelper<THash> cryptographyHelper)
        {
            this.store = store;
            this.cryptographyHelper = cryptographyHelper;
        }

        /// <summary>
        /// Creates a block with the content and previous hash and returns its hash. This method will not add the block to the blockchain.
        /// </summary>
        /// <param name="content">The content of the block</param>
        /// <returns>The hash of the new block used when adding the block to the blockchain</returns>
        public THash CreateBlock(TContent content)
        {
            //TODO: Content should not be sufficient for getting the hash. You must be able to call this
            //      function only if you are the creator of the block. Maybe blocks should have creators,
            //      or maybe this function would require a password or something
            var block = BlockFactory.Create(content, GetLatestHash(), cryptographyHelper);
            return block.Hash;
        }

        /// <summary>
        /// Adds a new block to the blockchain. 
        /// If the block and it's hash matches other blocks already in the chain
        /// returns true otherwise returns false indicating that the block may be corrupted and can't be added
        /// </summary>
        /// <param name="content">The content of the new block</param>
        /// <param name="hash">The hash of the new block Indicating that the block is sound</param>
        /// <returns>True if the block is OK and added to chain or False if it's not</returns>
        public bool AddBlock(TContent content, THash hash)
        {
            var previous = store.GetLast();
            if (previous == null)
            {
                AddGenesis(content);
                return true;
            }

            //TODO This shouldn't get the previous hash but rather compute the hash all the way down to
            //     genesis block. Or at least this should go a few (customizable number of) blocks down.
            var block = BlockFactory.Create(content, previous.Hash, cryptographyHelper);
            if (cryptographyHelper.CompareHash(hash, block.Hash))
            {
                store.Insert(block);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get what is already in the chain
        /// </summary>
        /// <returns>A list of contents in the blockchain</returns>
        public IEnumerable<TContent> ViewChain()
        {
            return store.GetAll().Select(obj => obj.Content);
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
