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

        [TestMethod]
        public void TestFunction()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.Function, "fcn("), tokenizer.Tokenize("fcn(").Single());
        }

        [TestMethod]
        public void TestNumber()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.Number, "123"), tokenizer.Tokenize("123").Single());
        }

        [TestMethod]
        public void TestHash()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.Hash, "#abc"), tokenizer.Tokenize("#abc").Single());
        }

        [TestMethod]
        public void TestPlus()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.Plus, "+"), tokenizer.Tokenize("+").Single());
        }

        [TestMethod]
        public void TestGreater()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.Greater, ">"), tokenizer.Tokenize(">").Single());
        }

        [TestMethod]
        public void TestComma()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.Comma, ","), tokenizer.Tokenize(",").Single());
        }

        [TestMethod]
        public void TestTilde()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.Tilde, "~"), tokenizer.Tokenize("~").Single());
        }

        [TestMethod]
        public void TestNot()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.Not, ":not("), tokenizer.Tokenize(":not(").Single());
        }

        [TestMethod]
        public void TestAtKeyword()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.AtKeyword, "@abc"), tokenizer.Tokenize("@abc").Single());
        }

        [TestMethod]
        public void TestInvalid()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.Invalid, "\"abc"), tokenizer.Tokenize("\"abc").Single());
        }

        [TestMethod]
        public void TestPercentage()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.Percentage, "100%"), tokenizer.Tokenize("100%").Single());
        }

        [TestMethod]
        public void TestDimension()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.Dimension, "50px"), tokenizer.Tokenize("50px").Single());
        }

        [TestMethod]
        public void TestCommentOpen()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.CommentOpen, "<!--"), tokenizer.Tokenize("<!--").Single());
        }

        [TestMethod]
        public void TestCommentClose()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.CommentClose, "-->"), tokenizer.Tokenize("-->").Single());
        }

        [TestMethod]
        public void TestUri()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.Uri, "url(\"http://www.google.com\")"), tokenizer.Tokenize("url(\"http://www.google.com\")").Single());
        }

        [TestMethod]
        public void TestUnicodeRange()
        {
            Assert.AreEqual(new CssSelectorToken(CssSelectorTokenType.UnicodeRange, "U+123"), tokenizer.Tokenize("U+123").Single());
        }

    }
}
