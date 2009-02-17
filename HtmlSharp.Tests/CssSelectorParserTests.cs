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
        public void TestTypeSelector()
        {
            CssSelectorParser parser = new CssSelectorParser();
            var selector = parser.Parse("a");
            Assert.AreEqual(
                new CssSelectorsGroup(
                    new[] {
                        new CssSelector(
                            new CssSimpleSelectorSequence(
                                new CssTypeSelector("a"), 
                                new CssSelectorFilter[0]) )}), 
                selector);
        }

        [TestMethod]
        public void TestMultipleSelectors()
        {
            CssSelectorParser parser = new CssSelectorParser();
            var selector = parser.Parse("a,div");
            Assert.AreEqual(
                new CssSelectorsGroup(
                    new[] {
                        new CssSelector(
                            new CssSimpleSelectorSequence(
                                new CssTypeSelector("a"), new CssSelectorFilter[0])),
                        new CssSelector(
                            new CssSimpleSelectorSequence(
                                new CssTypeSelector("div"), new CssSelectorFilter[0]))}), 
                selector);
        }

        [TestMethod]
        public void TestClassFilter()
        {
            CssSelectorParser parser = new CssSelectorParser();
            var selector = parser.Parse("a.class");
            Assert.AreEqual(
                new CssSelectorsGroup(
                    new[] {
                        new CssSelector(
                            new CssSimpleSelectorSequence(
                                new CssTypeSelector("a"),
                                new CssSelectorFilter[] {
                                    new CssClassSelector(".class")}))}), 
                selector);
        }

        [TestMethod]
        public void TestIdFilter()
        {
            CssSelectorParser parser = new CssSelectorParser();
            var selector = parser.Parse("a#id");
            Assert.AreEqual(
                new CssSelectorsGroup(
                    new[] {
                        new CssSelector(
                            new CssSimpleSelectorSequence(
                                new CssTypeSelector("a"),
                                new CssSelectorFilter[] {
                                    new CssHashSelector("#id")}))}),
                selector);
        }

        [TestMethod]
        public void TestMultipleFilters()
        {
            CssSelectorParser parser = new CssSelectorParser();
            var selector = parser.Parse("a.class.selected");
            Assert.AreEqual(
                new CssSelectorsGroup(
                    new[] {
                        new CssSelector(
                            new CssSimpleSelectorSequence(
                                new CssTypeSelector("a"),
                                new CssSelectorFilter[] {
                                    new CssClassSelector(".class"),
                                    new CssClassSelector(".selected")}))}),
                selector);
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
