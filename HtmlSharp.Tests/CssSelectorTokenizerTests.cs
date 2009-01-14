using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlSharp.Tests
{
    /// <summary>
    /// Summary description for CssSelectorTokenizerTests
    /// </summary>
    [TestClass]
    public class CssSelectorTokenizerTests
    {
        [TestMethod]
        public void TestSingleTypeSelector()
        {
            CssSelectorTokenizer tokenizer = new CssSelectorTokenizer();
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.TypeSelector, "div"), tokenizer.Tokenize("div").Single());
        }
    }
}
