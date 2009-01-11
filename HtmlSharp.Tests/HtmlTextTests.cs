using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HtmlSharp.Elements;

namespace HtmlSharp.Tests
{
    /// <summary>
    /// Summary description for HtmlTextTests
    /// </summary>
    [TestClass]
    public class HtmlTextTests
    {
        [TestMethod]
        public void TestHtmlDecodingValue()
        {
            var tag = new HtmlText();
            tag.Value = "10 &lt; 20";
            Assert.AreEqual("10 < 20", tag.Value);
            Assert.AreEqual("10 < 20", tag.ToString());
        }
    }
}
