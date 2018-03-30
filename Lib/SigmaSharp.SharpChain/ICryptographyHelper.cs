using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaSharp.SharpChain
{
    public interface ICryptographyHelper<T>
    {
        /// <summary>
        /// Get the hash of a block including its content and previous hash but, excluding current hash
        /// </summary>
        /// <typeparam name="TContent">The block content type</typeparam>
        /// <param name="input">The block to get hash of</param>
        /// <returns></returns>
        T GetHash<TContent>(Block<TContent, T> input);

        /// <summary>
        /// Compare two hash of this type
        /// </summary>
        /// <param name="hash1">First hash</param>
        /// <param name="hash2">Second hash</param>
        /// <returns></returns>
        bool CompareHash(T hash1, T hash2);

        /// <summary>
        /// Get an empty hash used only in genesis block
        /// </summary>
        /// <returns>An empty hash</returns>
        T GetDefaultHash();
    }
}
