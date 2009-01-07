using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;

namespace HtmlSharp.Tests
{
    public class TagAttributeTests
    {
        [Test]
        public void TestEquivalence()
        {
            var attr = new TagAttribute("align", "left");
            var attr2 = new TagAttribute("align", "left");
            Assert.AreEqual(attr, attr2);
        }

        [Test]
        public void TestToString()
        {
            var attr = new TagAttribute("align", "left");
            Assert.AreEqual("align=\"left\"", attr.ToString());
        }

        [Test]
        public void TestHtmlDecodingValue()
        {
            var attr = new TagAttribute("value", "3 &lt; 5");
            Assert.AreEqual("3 < 5", attr.Value);
        }
    }
}
