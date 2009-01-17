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
        public void TestSingleTypeSelector()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.TypeSelector, "div"), tokenizer.Tokenize("div").Single());
        }

        [TestMethod]
        public void TestSingleUniversalSelector()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.UniversalSelector, "*"), tokenizer.Tokenize("*").Single());
        }

        [TestMethod]
        public void TestAttributeStart()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.AttributeStart, "["), tokenizer.Tokenize("[a]").First());
        }

        [TestMethod]
        public void TestAttributeEnd()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.AttributeEnd, "]"), tokenizer.Tokenize("[a]").Last());
        }

        [TestMethod]
        public void TestSkipInitialSpace()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.TypeSelector, "div"), tokenizer.Tokenize("    \t  div").Single());
        }

        [TestMethod]
        public void TestAttributePrefixMatch()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.AttributePrefixMatch, "^="), tokenizer.Tokenize("[name^=div]").ElementAt(2));
        }

        [TestMethod]
        public void TestAttributSuffixMatch()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.AttributeSuffixMatch, "$="), tokenizer.Tokenize("[name$=div]").ElementAt(2));
        }

        [TestMethod]
        public void TestAttributeSubstringMatch()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.AttributeSubstringMatch, "*="), tokenizer.Tokenize("[name*=div]").ElementAt(2));
        }

        [TestMethod]
        public void TestAttributeMatch()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.AttributeMatch, "="), tokenizer.Tokenize("[name=div]").ElementAt(2));
        }

        [TestMethod]
        public void TestAttributeIncludesMatch()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.AttributeIncludesMatch, "~="), tokenizer.Tokenize("[name~=div]").ElementAt(2));
        }

        [TestMethod]
        public void TestAttributeDashMatch()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.AttributeDashMatch, "|="), tokenizer.Tokenize("[name|=div]").ElementAt(2));
        }

        [TestMethod]
        public void TestAttributeValue()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.AttributeValue, "div"), tokenizer.Tokenize("[name=div]").ElementAt(3));
        }

        [TestMethod]
        public void TestSingleQuotedAttributeValue()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.AttributeValue, "div"), tokenizer.Tokenize("[name='div']").ElementAt(3));
        }

        [TestMethod]
        public void TestDoubleQuotedAttributeValue()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.AttributeValue, "div"), tokenizer.Tokenize("[name=\"div\"]").ElementAt(3));
        }

        [TestMethod]
        public void TestEscapedQuotedAttributeValue()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.AttributeValue, "di\"v"), tokenizer.Tokenize("[name=\"di\\\"v\"]").ElementAt(3));
        }
    }
}
