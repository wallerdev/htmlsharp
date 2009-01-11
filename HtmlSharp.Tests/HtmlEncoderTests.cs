using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlSharp.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class HtmlEncoderTests
    {
        [TestMethod]
        public void TestEntityWithSemicolon()
        {
            var encoder = new HtmlEncoder();
            Assert.AreEqual(">", encoder.Decode("&gt;"));
        }

        [TestMethod]
        public void TestEntityWithoutSemicolon()
        {
            var encoder = new HtmlEncoder();
            Assert.AreEqual("&gt", encoder.Decode("&gt"));
        }

        [TestMethod]
        public void TestInvalidEntity()
        {
            var encoder = new HtmlEncoder();
            Assert.AreEqual("&blah;", encoder.Decode("&blah;"));
        }
    }
}
