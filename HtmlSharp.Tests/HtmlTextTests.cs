using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using HtmlSharp.Elements;

namespace HtmlSharp.Tests
{
    public class HtmlTextTests
    {
        [Test]
        public void TestHtmlDecodingValue()
        {
            var tag = new HtmlText();
            tag.Value = "10 &lt; 20";
            Assert.AreEqual("10 < 20", tag.Value);
            Assert.AreEqual("10 < 20", tag.ToString());
        }
    }
}
