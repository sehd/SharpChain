using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaSharp.SharpChain
{
    public static class ChainFactory
    {
        /// <summary>
        /// Create a blockchain with custom store and hash function
        /// </summary>
        /// <typeparam name="TContent">Type of the content in blockchain</typeparam>
        /// <typeparam name="THash">Type of the hash used in cryptography helper</typeparam>
        /// <param name="store">IBlockStore instance to store the records</param>
        /// <param name="cryptographyHelper">ICryptographyHelper used to compute hash</param>
        /// <returns>Instance of a chain containing operations for the blockchain</returns>
        public static Chain<TContent, THash> Create<TContent, THash>(IBlockStore<TContent, THash> store, ICryptographyHelper<THash> cryptographyHelper)
        {
            return new Chain<TContent, THash>(store, cryptographyHelper);
        }

        /// <summary>
        /// Create a blockchain with custom store using SHA1 hash function
        /// </summary>
        /// <typeparam name="TContent">Type of the content in blockchain</typeparam>
        /// <param name="store">IBlockStore instance to store the records</param>
        /// <returns>Instance of a chain containing operations for the blockchain</returns>
        public static Chain<TContent, byte[]> Create<TContent>(IBlockStore<TContent, byte[]> store)
        {
            return Create(store, new Sha1CryptographyHelper());
        }

        /// <summary>
        /// Create a blockchain with custom hash function storing blocks in memory
        /// </summary>
        /// <typeparam name="TContent">Type of the content in blockchain</typeparam>
        /// <typeparam name="THash">Type of the hash used in cryptography helper</typeparam>
        /// <param name="cryptographyHelper">ICryptographyHelper used to compute hash</param>
        /// <returns>Instance of a chain containing operations for the blockchain</returns>
        public static Chain<TContent, THash> Create<TContent, THash>(ICryptographyHelper<THash> cryptographyHelper)
        {
            return Create(new MemoryBlockStore<TContent, THash>(), cryptographyHelper);
        }

        /// <summary>
        /// Create a blockchain with SHA1 hash function and stores blocks in memory
        /// </summary>
        /// <typeparam name="TContent">Type of the content in blockchain</typeparam>
        /// <returns>Instance of a chain containing operations for the blockchain</returns>
        public static Chain<TContent, byte[]> Create<TContent>()
        {
            return Create(new MemoryBlockStore<TContent, byte[]>(), new Sha1CryptographyHelper());
        }
    }
}
