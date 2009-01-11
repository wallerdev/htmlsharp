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
    /// Summary description for TagTests
    /// </summary>
    [TestClass]
    public class TagTests
    {
        [TestMethod]
        public void TestTagEquivalence()
        {
            Assert.AreEqual(new Div(), new Div());
            Assert.AreNotEqual<Element>(new Div(), new Table());
            Assert.AreEqual(new Div(new TagAttribute("id", "abc")), new Div(new TagAttribute("id", "abc")));
            Assert.AreNotEqual(new Div(new TagAttribute("id", "abc")), new Div());
        }

        [TestMethod]
        public void TestTagToString()
        {
            Assert.AreEqual("<div></div>", new Div().ToString());
            Assert.AreEqual("<div>hello</div>", new Div(new HtmlText() { Value = "hello" }).ToString());
        }
    }
}
