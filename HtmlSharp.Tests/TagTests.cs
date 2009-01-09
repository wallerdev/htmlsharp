using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using HtmlSharp.Elements.Tags;
using HtmlSharp.Elements;

namespace HtmlSharp.Tests
{
    class TagTests
    {
        [Test]
        public void TestTagEquivalence()
        {
            Assert.AreEqual(new Div(), new Div());
            Assert.AreNotEqual<Element>(new Div(), new Table());
            Assert.AreEqual(new Div(new TagAttribute("id", "abc")), new Div(new TagAttribute("id", "abc")));
            Assert.AreNotEqual(new Div(new TagAttribute("id", "abc")), new Div());
        }

        [Test]
        public void TestTagToString()
        {
            Assert.AreEqual("<div></div>", new Div().ToString());
            Assert.AreEqual("<div>hello</div>", new Div(new HtmlText() { Value = "hello" }).ToString());
        }
    }
}
