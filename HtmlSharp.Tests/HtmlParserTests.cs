﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlSharp;
using MbUnit.Framework;
using HtmlSharp.Elements;
using HtmlSharp.Elements.Tags;

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
            parser = new HtmlParser();
            var page = parser.Parse("<tag>");
            Assert.AreEqual(new Root(new UnknownTag("tag")), page.Root);
        }

        [Test]
        public void TestSingleTagWithoutContentAndWithClosingTag()
        {
            var page = parser.Parse("<tag></tag>");
            Assert.AreEqual(new Root(new UnknownTag("tag")), page.Root);
        }

        [Test]
        public void TestSingleTagWithoutContentAndWithSelfClosingTag()
        {
            var page = parser.Parse("<tag/>");
            Assert.AreEqual(new Root(new UnknownTag("tag")), page.Root);
        }

        [Test]
        public void TestSingleTagWithContent()
        {
            var page = parser.Parse("<tag>content");
            Assert.AreEqual(new Root(new UnknownTag("tag", new HtmlText() { Value = "content" })), page.Root);
        }

        [Test]
        public void TestSingleTagWithContentAndWithClosingTag()
        {
            var page = parser.Parse("<tag>content</tag>");
            Assert.AreEqual(new Root(new UnknownTag("tag", new HtmlText() { Value = "content" })), page.Root);
        }

        [Test]
        public void TestRepeatedTagsWithoutContent()
        {
            var page = parser.Parse("<tag><tag>");
            Assert.AreEqual(new Root(new UnknownTag("tag"), new UnknownTag("tag")), page.Root);
        }

        [Test]
        public void TestRepeatedTagsWithoutContentAndWithClosingTag()
        {
            var page = parser.Parse("<tag></tag><tag></tag>");
            Assert.AreEqual(new Root(new UnknownTag("tag"), new UnknownTag("tag")), page.Root);
        }

        [Test]
        public void TestRepeatedTagsWithContent()
        {
            var page = parser.Parse("<tag>content<tag>content");
            Assert.AreEqual(
                new Root(
                    new UnknownTag("tag", new HtmlText() { Value = "content" }),
                    new UnknownTag("tag", new HtmlText() { Value = "content" })), page.Root);
        }

        [Test]
        public void TestRepeatedTagsWithContentAndWithClosingTags()
        {
            var page = parser.Parse("<tag>content</tag><tag>content</tag>");
            Assert.AreEqual(
                new Root(
                    new UnknownTag("tag", new HtmlText() { Value = "content" }),
                    new UnknownTag("tag", new HtmlText() { Value = "content" })), page.Root);
        }

        [Test]
        public void TestNestedTags()
        {
            var page = parser.Parse("<tag><innertag></tag>");
            Assert.AreEqual(new Root(new UnknownTag("tag", new UnknownTag("innertag"))), page.Root);
        }

        [Test]
        public void TestNestedTagsWithContent()
        {
            var page = parser.Parse("<tag><innertag>content</tag>");
            Assert.AreEqual(
                new Root(
                    new UnknownTag("tag",
                        new UnknownTag("innertag",
                            new HtmlText() { Value = "content" }))), page.Root);
        }

        [Test]
        public void TestDuplicateRepeatedTags()
        {
            var page = parser.Parse("<tag><tag></tag>");
            Assert.AreEqual(new Root(new UnknownTag("tag"), new UnknownTag("tag")), page.Root);
        }

        [Test]
        public void TestUnecessaryClosingTags()
        {
            var page = parser.Parse("<tag></tag></tag></innertag>");
            Assert.AreEqual(new Root(new UnknownTag("tag")), page.Root);
        }

        [Test]
        public void TestQuotedAttributes()
        {
            var page = parser.Parse("<div id=\"value\">");
            Assert.AreEqual(new Root(new Div(new TagAttribute("id", "value"))), page.Root);
        }

        //Nestable Tags
        
        [Test]
        public void TestDuplicateNestedNestableTags()
        {
            parser = new HtmlParser();
            var page = parser.Parse("<div><div></div>");
            Assert.AreEqual(new Root(new Div(new Div())), page.Root);
        }
    }
}
