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
        CssSelectorTokenizer tokenizer;

        [TestInitialize]
        public void Setup()
        {
            tokenizer = new CssSelectorTokenizer();
        }

        [TestMethod]
        public void TestIdent()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.Ident, "div"), tokenizer.Tokenize("div").Single());
        }

        [TestMethod]
        public void TestText()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.Text, "*"), tokenizer.Tokenize("*").Single());
        }

        [TestMethod]
        public void TestWhiteSpace()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.WhiteSpace, "    \t  "), tokenizer.Tokenize("    \t  ").Single());
        }

        [TestMethod]
        public void TestPrefixMatch()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.PrefixMatch, "^="), tokenizer.Tokenize("^=").Single());
        }

        [TestMethod]
        public void TestSuffixMatch()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.SuffixMatch, "$="), tokenizer.Tokenize("$=").Single());
        }

        [TestMethod]
        public void TestSubstringMatch()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.SubstringMatch, "*="), tokenizer.Tokenize("*=").Single());
        }

        [TestMethod]
        public void TestIncludes()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.Includes, "~="), tokenizer.Tokenize("~=").Single());
        }

        [TestMethod]
        public void TestDashMatch()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.DashMatch, "|="), tokenizer.Tokenize("|=").Single());
        }

        [TestMethod]
        public void TestSingleQuoteString()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.String, "'div'"), tokenizer.Tokenize("'div'").Single());
        }

        [TestMethod]
        public void TestDoubleQuoteString()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.String, "\"div\""), tokenizer.Tokenize("\"div\"").Single());
        }
    }
}
