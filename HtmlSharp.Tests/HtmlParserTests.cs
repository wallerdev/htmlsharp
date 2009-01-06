using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlSharp;
using MbUnit.Framework;
using HtmlSharp.Elements;

namespace HtmlSharp.Tests
{
    public class HtmlParserTests
    {
        HtmlParser parser;

        [SetUp]
        public void SetUp()
        {
            parser = new HtmlParser();
        }

        [Test]
        public void TestSingleEmptyTag()
        {
            var page = parser.Parse("<tag>");
            Assert.IsInstanceOfType(typeof(Tag), page.Root.Children[0]);
        }

        [Test]
        public void TestSingleTagWithoutContentAndWithClosingTag()
        {
            var page = parser.Parse("<tag></tag>");
            Assert.IsInstanceOfType(typeof(Tag), page.Root.Children[0]);
        }

        [Test]
        public void TestSingleTagWithoutContentAndWithSelfClosingTag()
        {
            var page = parser.Parse("<tag/>");
            Assert.IsInstanceOfType(typeof(Tag), page.Root.Children[0]);
        }

        [Test]
        public void TestSingleTagWithContent()
        {
            var page = parser.Parse("<tag>content");
            Assert.IsInstanceOfType(typeof(Tag), page.Root.Children[0]);
            Assert.IsInstanceOfType(typeof(Text), page.Root.Children[0].Children[0]);
        }

        [Test]
        public void TestSingleTagWithContentAndWithClosingTag()
        {
            var page = parser.Parse("<tag>content</tag>");
            Assert.IsInstanceOfType(typeof(Tag), page.Root.Children[0]);
            Assert.IsInstanceOfType(typeof(Text), page.Root.Children[0].Children[0]);
        }

        [Test]
        public void TestRepeatedTagsWithoutContent()
        {
            var page = parser.Parse("<tag><tag>");
            Assert.IsInstanceOfType(typeof(Tag), page.Root.Children[0]);
            Assert.IsInstanceOfType(typeof(Tag), page.Root.Children[1]);
        }

        [Test]
        public void TestRepeatedTagsWithoutContentAndWithClosingTag()
        {
            var page = parser.Parse("<tag></tag><tag></tag>");
            Assert.IsInstanceOfType(typeof(Tag), page.Root.Children[0]);
            Assert.IsInstanceOfType(typeof(Tag), page.Root.Children[1]);
        }

        [Test]
        public void TestRepeatedTagsWithContent()
        {
            var page = parser.Parse("<tag>content<tag>content");
            Assert.IsInstanceOfType(typeof(Tag), page.Root.Children[0]);
            Assert.IsInstanceOfType(typeof(Text), page.Root.Children[0].Children[0]);
            Assert.IsInstanceOfType(typeof(Tag), page.Root.Children[1]);
            Assert.IsInstanceOfType(typeof(Text), page.Root.Children[1].Children[0]);
        }

        [Test]
        public void TestRepeatedTagsWithContentAndWithClosingTags()
        {
            var page = parser.Parse("<tag>content</tag><tag>content</tag>");
            Assert.IsInstanceOfType(typeof(Tag), page.Root.Children[0]);
            Assert.IsInstanceOfType(typeof(Text), page.Root.Children[0].Children[0]);
            Assert.IsInstanceOfType(typeof(Tag), page.Root.Children[1]);
            Assert.IsInstanceOfType(typeof(Text), page.Root.Children[1].Children[0]);
        }

        [Test]
        public void TestNestedTags()
        {
            var page = parser.Parse("<tag><innertag></tag>");
            Assert.IsInstanceOfType(typeof(Tag), page.Root.Children[0]);
            Assert.IsInstanceOfType(typeof(Tag), page.Root.Children[0].Children[0]);
        }

        [Test]
        public void TestNestedTagsWithContent()
        {
            var page = parser.Parse("<tag><innertag>content</tag>");
            Assert.IsInstanceOfType(typeof(Tag), page.Root.Children[0]);
            Assert.IsInstanceOfType(typeof(Tag), page.Root.Children[0].Children[0]);
            Assert.IsInstanceOfType(typeof(Text), page.Root.Children[0].Children[0].Children[0]);
        }

        [Test]
        public void TestDuplicateRepeatedTags()
        {
            var page = parser.Parse("<tag><tag></tag>");
            Assert.IsInstanceOfType(typeof(Tag), page.Root.Children[0]);
            Assert.IsInstanceOfType(typeof(Tag), page.Root.Children[1]);
        }

        [Test]
        public void TestUnecessaryClosingTags()
        {
            var page = parser.Parse("<tag></tag></tag></innertag>");
            Assert.AreEqual(1, page.Root.Children.Count);
            Assert.IsInstanceOfType(typeof(Tag), page.Root.Children[0]);
        }

        //Nestable Tags
        
        [Test]
        public void TestDuplicateNestedNestableTags()
        {
            var page = parser.Parse("<div><div></div>");
            Assert.IsInstanceOfType(typeof(Tag), page.Root.Children[0]);
            Assert.IsInstanceOfType(typeof(Tag), page.Root.Children[0].Children[0]);
        }
    }
}
