using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlSharp.Tests
{
    /// <summary>
    /// Summary description for TagAttributeTests
    /// </summary>
    [TestClass]
    public class TagAttributeTests
    {
        [TestMethod]
        public void TestEquivalence()
        {
            var attr = new TagAttribute("align", "left");
            var attr2 = new TagAttribute("align", "left");
            Assert.AreEqual(attr, attr2);
        }

        [TestMethod]
        public void TestToString()
        {
            var attr = new TagAttribute("align", "left");
            Assert.AreEqual("align=\"left\"", attr.ToString());
        }

        [TestMethod]
        public void TestHtmlDecodingValue()
        {
            var attr = new TagAttribute("value", "3 &lt; 5");
            Assert.AreEqual("3 < 5", attr.Value);
        }
    }
}
