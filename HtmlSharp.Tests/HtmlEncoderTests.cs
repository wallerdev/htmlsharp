using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;

namespace HtmlSharp.Tests
{
    public class HtmlEncoderTests
    {
        [Test]
        public void TestEntityWithSemicolon()
        {
            var encoder = new HtmlEncoder();
            Assert.AreEqual(">", encoder.Decode("&gt;"));
        }

        [Test]
        public void TestEntityWithoutSemicolon()
        {
            var encoder = new HtmlEncoder();
            Assert.AreEqual("&gt", encoder.Decode("&gt"));
        }

        [Test]
        public void TestInvalidEntity()
        {
            var encoder = new HtmlEncoder();
            Assert.AreEqual("&blah;", encoder.Decode("&blah;"));
        }
    }
}
