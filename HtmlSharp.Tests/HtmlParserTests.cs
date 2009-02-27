using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HtmlSharp.Elements.Tags;
using HtmlSharp.Elements;

namespace HtmlSharp.Tests
{
    /// <summary>
    /// Summary description for HtmlParserTests
    /// </summary>
    [TestClass]
    public class HtmlParserTests
    {
        HtmlParser parser;

        [TestInitialize]
        public void SetUp()
        {
            parser = new HtmlParser();
        }

        [TestMethod]
        public void TestSingleEmptyTag()
        {
            parser = new HtmlParser();
            var page = parser.Parse("<tag>");
            Assert.AreEqual(new Root(new UnknownTag("tag")), page.Root);
        }

        [TestMethod]
        public void TestSingleTagWithoutContentAndWithClosingTag()
        {
            var page = parser.Parse("<tag></tag>");
            Assert.AreEqual(new Root(new UnknownTag("tag")), page.Root);
        }

        [TestMethod]
        public void TestSingleTagWithoutContentAndWithSelfClosingTag()
        {
            var page = parser.Parse("<tag/>");
            Assert.AreEqual(new Root(new UnknownTag("tag")), page.Root);
        }

        [TestMethod]
        public void TestSingleTagWithContent()
        {
            var page = parser.Parse("<tag>content");
            Assert.AreEqual(new Root(new UnknownTag("tag", new HtmlText() { Value = "content" })), page.Root);
        }

        [TestMethod]
        public void TestSingleTagWithContentAndWithClosingTag()
        {
            var page = parser.Parse("<tag>content</tag>");
            Assert.AreEqual(new Root(new UnknownTag("tag", new HtmlText() { Value = "content" })), page.Root);
        }

        [TestMethod]
        public void TestRepeatedTagsWithoutContent()
        {
            var page = parser.Parse("<tag><tag>");
            Assert.AreEqual(new Root(new UnknownTag("tag"), new UnknownTag("tag")), page.Root);
        }

        [TestMethod]
        public void TestRepeatedTagsWithoutContentAndWithClosingTag()
        {
            var page = parser.Parse("<tag></tag><tag></tag>");
            Assert.AreEqual(new Root(new UnknownTag("tag"), new UnknownTag("tag")), page.Root);
        }

        [TestMethod]
        public void TestRepeatedTagsWithContent()
        {
            var page = parser.Parse("<p>content<p>content");
            Assert.AreEqual(
                new Root(
                    new P(new HtmlText() { Value = "content" }),
                    new P(new HtmlText() { Value = "content" })), page.Root);
        }

        [TestMethod]
        public void TestRepeatedTagsWithContentAndWithClosingTags()
        {
            var page = parser.Parse("<tag>content</tag><tag>content</tag>");
            Assert.AreEqual(
                new Root(
                    new UnknownTag("tag", new HtmlText() { Value = "content" }),
                    new UnknownTag("tag", new HtmlText() { Value = "content" })), page.Root);
        }

        [TestMethod]
        public void TestNestedTags()
        {
            var page = parser.Parse("<tag><innertag></tag>");
            Assert.AreEqual(new Root(new UnknownTag("tag", new UnknownTag("innertag"))), page.Root);
        }

        [TestMethod]
        public void TestNestedTagsWithContent()
        {
            var page = parser.Parse("<tag><innertag>content</tag>");
            Assert.AreEqual(
                new Root(
                    new UnknownTag("tag",
                        new UnknownTag("innertag",
                            new HtmlText() { Value = "content" }))), page.Root);
        }

        [TestMethod]
        public void TestDuplicateRepeatedTags()
        {
            var page = parser.Parse("<tag><tag></tag>");
            Assert.AreEqual(new Root(new UnknownTag("tag"), new UnknownTag("tag")), page.Root);
        }

        [TestMethod]
        public void TestUnecessaryClosingTags()
        {
            var page = parser.Parse("<tag></tag></tag></innertag>");
            Assert.AreEqual(new Root(new UnknownTag("tag")), page.Root);
        }

        [TestMethod]
        public void TestDoubleQuotedAttributes()
        {
            var page = parser.Parse("<div id=\"value\">");
            Assert.AreEqual(new Root(new Div(new TagAttribute("id", "value"))), page.Root);
        }

        [TestMethod]
        public void TestSingleQuotedAttributes()
        {
            var page = parser.Parse("<div id='value'>");
            Assert.AreEqual(new Root(new Div(new TagAttribute("id", "value"))), page.Root);
        }

        [TestMethod]
        public void TestUnquotedAttributes()
        {
            var page = parser.Parse("<div id=value>");
            Assert.AreEqual(new Root(new Div(new TagAttribute("id", "value"))), page.Root);
        }

        [TestMethod]
        public void TestDuplicateNestedNestableTags()
        {
            var page = parser.Parse("<div><div></div>");
            Assert.AreEqual(new Root(new Div(new Div())), page.Root);
        }

        [TestMethod]
        public void TestParentIsSet()
        {
            var page = parser.Parse("<div><p></div>");
            Assert.AreEqual(page.Root, page.Root.Children.ElementAt(0).Parent);
            Assert.AreEqual(page.Root.Children.ElementAt(0),
                page.Root.Children.ElementAt(0).Children.ElementAt(0).Parent);
        }

        [TestMethod]
        public void TestNextSiblingIsSet()
        {
            var page = parser.Parse("<p>text</p></p><p>text</p>");
            Assert.AreEqual(page.Root.Children.ElementAt(1),
                page.Root.Children.ElementAt(0).NextSibling);
        }

        [TestMethod]
        public void TestPreviousSiblingIsSet()
        {
            var page = parser.Parse("<p>text</p></p><p>text</p>");
            Assert.AreEqual(page.Root.Children.ElementAt(0),
                page.Root.Children.ElementAt(1).PreviousSibling);
        }

        [TestMethod]
        public void TestSelfClosingTags()
        {
            var page = parser.Parse("<img><p>");
            Assert.AreEqual(new Root(new Img(), new P()), page.Root);
        }
    }
}
