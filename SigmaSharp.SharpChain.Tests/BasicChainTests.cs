using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace SigmaSharp.SharpChain.Tests
{
    [TestClass]
    public class BasicChainTests
    {
        [TestMethod]
        public void BasinSingleChainAddAndRestore()
        {
            var chain = ChainFactory.Create<string>();
            var sampleBlock = new List<string>()
            {
                "B1","B2","B3","B4","B5","B6","B7","B8","B9","B10","B11"
            };
            foreach (var item in sampleBlock)
            {
                chain.AddBlock(item, chain.CreateBlock(item));
            }
            var res = chain.ViewChain().Select((obj, i) => new { content = obj, index = i });
            foreach (var item in res)
            {
                Assert.AreEqual(item.content, sampleBlock[item.index]);
            }
        }
    }
}
