using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HtmlSharp.Css;

namespace HtmlSharp.Tests
{
    /// <summary>
    /// Summary description for CssSelectorTokenizerTests
    /// </summary>
    [TestClass]
    public class CssSelectorTokenizerTests
    {
        SelectorTokenizer tokenizer;

        [TestInitialize]
        public void Setup()
        {
            tokenizer = new SelectorTokenizer();
        }

        [TestMethod]
        public void TestIdent()
        {
            Assert.AreEqual(new SelectorToken(SelectorTokenType.Ident, "div"), tokenizer.Tokenize("div").Single());
        }

        [TestMethod]
        public void TestText()
        {
            Assert.AreEqual(new SelectorToken(SelectorTokenType.Text, "*"), tokenizer.Tokenize("*").Single());
        }

        [TestMethod]
        public void TestWhiteSpace()
        {
            Assert.AreEqual(new SelectorToken(SelectorTokenType.WhiteSpace, "    \t  "), tokenizer.Tokenize("    \t  ").Single());
        }

        [TestMethod]
        public void TestPrefixMatch()
        {
            Assert.AreEqual(new SelectorToken(SelectorTokenType.PrefixMatch, "^="), tokenizer.Tokenize("^=").Single());
        }

        [TestMethod]
        public void TestSuffixMatch()
        {
            Assert.AreEqual(new SelectorToken(SelectorTokenType.SuffixMatch, "$="), tokenizer.Tokenize("$=").Single());
        }

        [TestMethod]
        public void TestSubstringMatch()
        {
            Assert.AreEqual(new SelectorToken(SelectorTokenType.SubstringMatch, "*="), tokenizer.Tokenize("*=").Single());
        }

        [TestMethod]
        public void TestIncludes()
        {
            Assert.AreEqual(new SelectorToken(SelectorTokenType.Includes, "~="), tokenizer.Tokenize("~=").Single());
        }

        [TestMethod]
        public void TestDashMatch()
        {
            Assert.AreEqual(new SelectorToken(SelectorTokenType.DashMatch, "|="), tokenizer.Tokenize("|=").Single());
        }

        [TestMethod]
        public void TestSingleQuoteString()
        {
            Assert.AreEqual(new SelectorToken(SelectorTokenType.String, "'div'"), tokenizer.Tokenize("'div'").Single());
        }

        [TestMethod]
        public void TestDoubleQuoteString()
        {
            Assert.AreEqual(new SelectorToken(SelectorTokenType.String, "\"div\""), tokenizer.Tokenize("\"div\"").Single());
        }

        [TestMethod]
        public void TestFunction()
        {
            Assert.AreEqual(new SelectorToken(SelectorTokenType.Function, "fcn("), tokenizer.Tokenize("fcn(").Single());
        }

        [TestMethod]
        public void TestNumber()
        {
            Assert.AreEqual(new SelectorToken(SelectorTokenType.Number, "123"), tokenizer.Tokenize("123").Single());
        }

        [TestMethod]
        public void TestHash()
        {
            Assert.AreEqual(new SelectorToken(SelectorTokenType.Hash, "#abc"), tokenizer.Tokenize("#abc").Single());
        }

        [TestMethod]
        public void TestPlus()
        {
            Assert.AreEqual(new SelectorToken(SelectorTokenType.Plus, "+"), tokenizer.Tokenize("+").Single());
        }

        [TestMethod]
        public void TestGreater()
        {
            Assert.AreEqual(new SelectorToken(SelectorTokenType.Greater, ">"), tokenizer.Tokenize(">").Single());
        }

        [TestMethod]
        public void TestComma()
        {
            Assert.AreEqual(new SelectorToken(SelectorTokenType.Comma, ","), tokenizer.Tokenize(",").Single());
        }

        [TestMethod]
        public void TestTilde()
        {
            Assert.AreEqual(new SelectorToken(SelectorTokenType.Tilde, "~"), tokenizer.Tokenize("~").Single());
        }

        [TestMethod]
        public void TestNot()
        {
            Assert.AreEqual(new SelectorToken(SelectorTokenType.Not, ":not("), tokenizer.Tokenize(":not(").Single());
        }

        [TestMethod]
        public void TestAtKeyword()
        {
            Assert.AreEqual(new SelectorToken(SelectorTokenType.AtKeyword, "@abc"), tokenizer.Tokenize("@abc").Single());
        }

        [TestMethod]
        public void TestInvalid()
        {
            Assert.AreEqual(new SelectorToken(SelectorTokenType.Invalid, "\"abc"), tokenizer.Tokenize("\"abc").Single());
        }

        [TestMethod]
        public void TestPercentage()
        {
            Assert.AreEqual(new SelectorToken(SelectorTokenType.Percentage, "100%"), tokenizer.Tokenize("100%").Single());
        }

        [TestMethod]
        public void TestDimension()
        {
            Assert.AreEqual(new SelectorToken(SelectorTokenType.Dimension, "50px"), tokenizer.Tokenize("50px").Single());
        }

        [TestMethod]
        public void TestCommentOpen()
        {
            Assert.AreEqual(new SelectorToken(SelectorTokenType.CommentOpen, "<!--"), tokenizer.Tokenize("<!--").Single());
        }

        [TestMethod]
        public void TestCommentClose()
        {
            Assert.AreEqual(new SelectorToken(SelectorTokenType.CommentClose, "-->"), tokenizer.Tokenize("-->").Single());
        }

        [TestMethod]
        public void TestUri()
        {
            Assert.AreEqual(new SelectorToken(SelectorTokenType.Uri, "url(\"http://www.google.com\")"), tokenizer.Tokenize("url(\"http://www.google.com\")").Single());
        }

        [TestMethod]
        public void TestUnicodeRange()
        {
            Assert.AreEqual(new SelectorToken(SelectorTokenType.UnicodeRange, "U+123"), tokenizer.Tokenize("U+123").Single());
        }

    }
}
