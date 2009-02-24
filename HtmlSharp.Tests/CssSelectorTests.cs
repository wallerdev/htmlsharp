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
            doc = parser.Parse(File.ReadAllText("../../../HtmlSharp.Tests/Simple.html"));
        }

        [TestMethod]
        public void TestTypeSelector()
        {
            var tag = doc.Find("p");
            Assert.AreEqual(new P(new HtmlText() { Value = "It will be used in tests." }), tag);
        }
    }
}
