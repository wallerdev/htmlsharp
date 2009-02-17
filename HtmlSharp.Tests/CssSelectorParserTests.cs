using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HtmlSharp.Elements.Tags;

namespace HtmlSharp.Tests
{
    /// <summary>
    /// Summary description for CssSelectorParserTests
    /// </summary>
    [TestClass]
    public class CssSelectorParserTests
    {
        [TestMethod]
        public void TestUniversalSelector()
        {
            CssSelectorParser parser = new CssSelectorParser();
            var selector = parser.Parse("a");
            Assert.AreEqual(new CssSelectorsGroup(new[] { new CssSelector(new CssSimpleSelectorSequence(new CssUniversalSelector(), new CssSelectorFilter[0]) )}), selector);
        }

        //[TestMethod]
        //public void TestWhiteSpace()
        //{
        //    CssSelectorParser parser = new CssSelectorParser();
        //    var selectors = new[] { parser.Parse("a>b"), parser.Parse("a> b"), parser.Parse("a >b"), parser.Parse("a > b") };
        //    foreach (var selector in selectors)
        //    {
        //        Assert.AreEqual(
        //            new CssSelector(
        //                new[]
        //                {
        //                    new CssTypeSelector(
        //                        new P()), 
        //                    new CssTypeSelector(
        //                        new A())
        //                },
        //                new[]
        //                {
        //                    new CssChildCombinator()
        //                }), selector);
        //    }
        //}
    }
}
