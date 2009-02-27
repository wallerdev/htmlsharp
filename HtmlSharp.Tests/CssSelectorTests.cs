using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using HtmlSharp.Elements.Tags;
using HtmlSharp.Elements;

namespace HtmlSharp.Tests
{
    /// <summary>
    /// Summary description for CssSelectorTests
    /// </summary>
    [TestClass]
    public class CssSelectorTests
    {
        Document doc;
        public CssSelectorTests()
        {
            HtmlParser parser = new HtmlParser();
            doc = parser.Parse(File.ReadAllText("Simple.html"));
        }

        [TestMethod]
        public void TestTypeSelector()
        {
            var tag = doc.Find("p");
            Assert.AreEqual(new P(new HtmlText() { Value = "It will be used in tests." }), tag);
        }

        [TestMethod]
        public void TestFindFail()
        {
            var tag = doc.Find("form");
            Assert.IsNull(tag);
        }

        [TestMethod]
        public void TestDescendantCombinator()
        {
            var tag = doc.Find("html p");
            Assert.AreEqual(new P(new HtmlText() { Value = "It will be used in tests." }), tag);
        }

        [TestMethod]
        public void TestChildCombinator()
        {
            var tag = doc.Find("html > p");
            Assert.IsNull(tag);
            tag = doc.Find("body > p");
            Assert.AreEqual(new P(new HtmlText() { Value = "It will be used in tests." }), tag);
        }

        [TestMethod]
        public void TestSiblingCombinator()
        {
            var tag = doc.Find("body + h1");
            Assert.IsNull(tag);
            tag = doc.Find("h1 + p");
            Assert.AreEqual(new P(new HtmlText() { Value = "It will be used in tests." }), tag);
        }

        [TestMethod]
        public void TestGeneralSiblingCombinator()
        {
            var tag = doc.Find("body ~ h2");
            Assert.IsNull(tag);
            tag = doc.Find("h1 ~ h2");
            Assert.AreEqual(new H2(new HtmlText() { Value = "Really, it's unfortunate." }), tag);
        }

        [TestMethod]
        public void TestIDFilter()
        {
            var tag = doc.Find("#info");
            Assert.AreEqual(new P(new[] { new TagAttribute("id", "info") },
                new HtmlText() { Value = "It probably will not be used anywhere else." }), tag);
        }

        [TestMethod]
        public void TestClassFilter()
        {
            var tag = doc.Find(".more");
            Assert.AreEqual(new P(new[] { new TagAttribute("class", "more") },
                new HtmlText() { Value = "Nothing to really talk about." }), tag);
        }

        [TestMethod]
        public void TestUniversalSelector()
        {
            HtmlParser parser = new HtmlParser();
            var html = parser.Parse("<p></p>");
            Assert.AreEqual(new P(), html.Find("*"));
        }

        [TestMethod]
        public void TestAttributeExistsSelector()
        {
            var tag = doc.Find("[id]");
            Assert.AreEqual(new P(new[] { new TagAttribute("id", "info") },
                new HtmlText() { Value = "It probably will not be used anywhere else." }), tag);
        }

        [TestMethod]
        public void TestAttributeEqualsSelector()
        {
            var tag = doc.Find("[id=fake]");
            Assert.IsNull(tag);
            tag = doc.Find("[id=info]");
            Assert.AreEqual(new P(new[] { new TagAttribute("id", "info") },
                new HtmlText() { Value = "It probably will not be used anywhere else." }), tag);
        }

        [TestMethod]
        public void TestAttributeDashFilter()
        {
            var tag = doc.Find("[hreflang|=fake]");
            Assert.IsNull(tag);
            tag = doc.Find("[hreflang|=en]");
            Assert.AreEqual(new Link(new[] { new TagAttribute("rel", "copyright copyleft"),
                new TagAttribute("hreflang", "en-us")}), tag);
        }

        [TestMethod]
        public void TestAttributeIncludesFilter()
        {
            var tag = doc.Find("[rel~=fake]");
            Assert.IsNull(tag);
            tag = doc.Find("[rel~=copyleft]");
            Assert.AreEqual(new Link(new[] { new TagAttribute("rel", "copyright copyleft"),
                new TagAttribute("hreflang", "en-us")}), tag);
        }

        [TestMethod]
        public void TestAttributePrefixFilter()
        {
            var tag = doc.Find("[id^=fake]");
            Assert.IsNull(tag);
            tag = doc.Find("[id^=in]");
            Assert.AreEqual(new P(new[] { new TagAttribute("id", "info") },
                new HtmlText() { Value = "It probably will not be used anywhere else." }), tag);
        }

        [TestMethod]
        public void TestAttributeSuffixSelector()
        {
            var tag = doc.Find("[id$=fake]");
            Assert.IsNull(tag);
            tag = doc.Find("[id$=fo]");
            Assert.AreEqual(new P(new[] { new TagAttribute("id", "info") },
                new HtmlText() { Value = "It probably will not be used anywhere else." }), tag);
        }

        [TestMethod]
        public void TestAttributeSubstringSelector()
        {
            var tag = doc.Find("[id*=fake]");
            Assert.IsNull(tag);
            tag = doc.Find("[id*=nf]");
            Assert.AreEqual(new P(new[] { new TagAttribute("id", "info") },
                new HtmlText() { Value = "It probably will not be used anywhere else." }), tag);
        }

        [TestMethod]
        public void TestRootFilter()
        {
            HtmlParser parser = new HtmlParser();
            var html = parser.Parse("<p></p>");
            Assert.AreEqual(new P(), html.Find(":root"));
        }

        [TestMethod]
        public void TestNthChildFilter()
        {
            var tag = doc.Find("body:nth-child(3)");
            Assert.AreEqual(new P(new[] { new TagAttribute("id", "info") },
                new HtmlText() { Value = "It probably will not be used anywhere else." }), tag);
        }

        [TestMethod]
        public void TestNthLastChildFilter()
        {
            var tag = doc.Find("head:nth-last-child(1)");
            Assert.AreEqual(new Link(new[] { new TagAttribute("rel", "copyright copyleft"),
                new TagAttribute("hreflang", "en-us")}), tag);
        }

        [TestMethod]
        public void TestNthOfTypeFilter()
        {
            var tag = doc.Find("p:nth-of-type(2)");
            Assert.AreEqual(new P(new[] { new TagAttribute("id", "info") },
                new HtmlText() { Value = "It probably will not be used anywhere else." }), tag);
        }

        [TestMethod]
        public void TestNthLastOfTypeFilter()
        {
            var tag = doc.Find("p:nth-last-of-type(1)");
            Assert.AreEqual(new P(new[] { new TagAttribute("class", "more") },
                new HtmlText() { Value = "Nothing to really talk about." }), tag);
        }
    }
}
