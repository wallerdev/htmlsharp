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
        public void TestUniversalSelector()
        {
            CssSelectorParser parser = new CssSelectorParser();
            var selector = parser.Parse("*");
            Assert.AreEqual(
                new CssSelectorsGroup(
                    new[] {
                        new CssSelector(
                            new CssSimpleSelectorSequence(
                                new CssUniversalSelector(), 
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
        public void TestDescendentCombinator()
        {
            CssSelectorParser parser = new CssSelectorParser();
            var selector = parser.Parse("div a");
            Assert.AreEqual(
                new CssSelectorsGroup(
                    new[] {
                        new CssSelector(
                            new[] {
                                new CssSimpleSelectorSequence(
                                   new CssTypeSelector("div"), new CssSelectorFilter[0]),
                               new CssSimpleSelectorSequence(
                                   new CssTypeSelector("a"), new CssSelectorFilter[0])},
                            new[] {
                                new CssDescendantCombinator()
                            })}),
                selector);
        }

        [TestMethod]
        public void TestChildCombinator()
        {
            CssSelectorParser parser = new CssSelectorParser();
            var selector = parser.Parse("div > a");
            Assert.AreEqual(
                new CssSelectorsGroup(
                    new[] {
                        new CssSelector(
                            new[] {
                                new CssSimpleSelectorSequence(
                                   new CssTypeSelector("div"), new CssSelectorFilter[0]),
                               new CssSimpleSelectorSequence(
                                   new CssTypeSelector("a"), new CssSelectorFilter[0])},
                            new[] {
                                new CssChildCombinator()
                            })}),
                selector);
        }

        [TestMethod]
        public void TestAdjacentSiblingCombinator()
        {
            CssSelectorParser parser = new CssSelectorParser();
            var selector = parser.Parse("div + a");
            Assert.AreEqual(
                new CssSelectorsGroup(
                    new[] {
                        new CssSelector(
                            new[] {
                                new CssSimpleSelectorSequence(
                                   new CssTypeSelector("div"), new CssSelectorFilter[0]),
                               new CssSimpleSelectorSequence(
                                   new CssTypeSelector("a"), new CssSelectorFilter[0])},
                            new[] {
                                new CssAdjacentSiblingCombinator()
                            })}),
                selector);
        }

        [TestMethod]
        public void TestMultipleCombinators()
        {
            CssSelectorParser parser = new CssSelectorParser();
            var selector = parser.Parse("div > table tr > td");
            Assert.AreEqual(
                new CssSelectorsGroup(
                    new[] {
                        new CssSelector(
                            new[] {
                                new CssSimpleSelectorSequence(
                                   new CssTypeSelector("div"), new CssSelectorFilter[0]),
                               new CssSimpleSelectorSequence(
                                   new CssTypeSelector("table"), new CssSelectorFilter[0]),
                                new CssSimpleSelectorSequence(
                                   new CssTypeSelector("tr"), new CssSelectorFilter[0]),
                               new CssSimpleSelectorSequence(
                                   new CssTypeSelector("td"), new CssSelectorFilter[0])},
                            new CssCombinator[] {
                                new CssChildCombinator(),
                                new CssDescendantCombinator(),
                                new CssChildCombinator()
                            })}),
                selector);
        }

        [TestMethod]
        public void TestGeneralSiblingCombinator()
        {
            CssSelectorParser parser = new CssSelectorParser();
            var selector = parser.Parse("div ~ a");
            Assert.AreEqual(
                new CssSelectorsGroup(
                    new[] {
                        new CssSelector(
                            new[] {
                                new CssSimpleSelectorSequence(
                                   new CssTypeSelector("div"), new CssSelectorFilter[0]),
                               new CssSimpleSelectorSequence(
                                   new CssTypeSelector("a"), new CssSelectorFilter[0])},
                            new[] {
                                new CssGeneralSiblingCombinator()
                            })}),
                selector);
        }

        [TestMethod]
        public void TestClassFilter()
        {
            CssSelectorParser parser = new CssSelectorParser();
            var selector = parser.Parse(".class");
            Assert.AreEqual(
                new CssSelectorsGroup(
                    new[] {
                        new CssSelector(
                            new CssSimpleSelectorSequence(
                                new CssUniversalSelector(),
                                new[] {
                                    new CssClassSelector(".class")}))}), 
                selector);
        }

        [TestMethod]
        public void TestIdFilter()
        {
            CssSelectorParser parser = new CssSelectorParser();
            var selector = parser.Parse("#id");
            Assert.AreEqual(
                new CssSelectorsGroup(
                    new[] {
                        new CssSelector(
                            new CssSimpleSelectorSequence(
                                new CssUniversalSelector(),
                                new[] {
                                    new CssHashSelector("#id")}))}),
                selector);
        }

        [TestMethod]
        public void TestAttributePresentFilter()
        {
            CssSelectorParser parser = new CssSelectorParser();
            var selector = parser.Parse("[href]");
            Assert.AreEqual(
                new CssSelectorsGroup(
                    new[] {
                        new CssSelector(
                            new CssSimpleSelectorSequence(
                                new CssUniversalSelector(),
                                new[] {
                                    new CssAttributeSelector("href")}))}),
                selector);
        }

        [TestMethod]
        public void TestAttributePrefixFilter()
        {
            CssSelectorParser parser = new CssSelectorParser();
            var selector = parser.Parse("[href^=http]");
            Assert.AreEqual(
                new CssSelectorsGroup(
                    new[] {
                        new CssSelector(
                            new CssSimpleSelectorSequence(
                                new CssUniversalSelector(),
                                new[] {
                                    new CssAttributePrefixMatchSelector("href", "http")}))}),
                selector);
        }

        [TestMethod]
        public void TestAttributeSuffixFilter()
        {
            CssSelectorParser parser = new CssSelectorParser();
            var selector = parser.Parse("[href$=html]");
            Assert.AreEqual(
                new CssSelectorsGroup(
                    new[] {
                        new CssSelector(
                            new CssSimpleSelectorSequence(
                                new CssUniversalSelector(),
                                new[] {
                                    new CssAttributeSuffixMatchSelector("href", "html")}))}),
                selector);
        }

        [TestMethod]
        public void TestAttributeSubstringMatcFilter()
        {
            CssSelectorParser parser = new CssSelectorParser();
            var selector = parser.Parse("[href*=www]");
            Assert.AreEqual(
                new CssSelectorsGroup(
                    new[] {
                        new CssSelector(
                            new CssSimpleSelectorSequence(
                                new CssUniversalSelector(),
                                new[] {
                                    new CssAttributeSubstringMatchSelector("href", "www")}))}),
                selector);
        }

        [TestMethod]
        public void TestAttributeExactMatcFilter()
        {
            CssSelectorParser parser = new CssSelectorParser();
            var selector = parser.Parse("[name=username]");
            Assert.AreEqual(
                new CssSelectorsGroup(
                    new[] {
                        new CssSelector(
                            new CssSimpleSelectorSequence(
                                new CssUniversalSelector(),
                                new[] {
                                    new CssAttributeExactMatchSelector("name", "username")}))}),
                selector);
        }

        [TestMethod]
        public void TestAttributeIncludesSelector()
        {
            CssSelectorParser parser = new CssSelectorParser();
            var selector = parser.Parse("[rel~=copyright]");
            Assert.AreEqual(
                new CssSelectorsGroup(
                    new[] {
                        new CssSelector(
                            new CssSimpleSelectorSequence(
                                new CssUniversalSelector(),
                                new[] {
                                    new CssAttributeIncludesSelector("rel", "copyright")}))}),
                selector);
        }

        [TestMethod]
        public void TestAttributeDashSelector()
        {
            CssSelectorParser parser = new CssSelectorParser();
            var selector = parser.Parse("[hreflang|=en]");
            Assert.AreEqual(
                new CssSelectorsGroup(
                    new[] {
                        new CssSelector(
                            new CssSimpleSelectorSequence(
                                new CssUniversalSelector(),
                                new[] {
                                    new CssAttributeDashMatchSelector("hreflang", "en")}))}),
                selector);
        }

        [TestMethod]
        public void TestMultipleFilters()
        {
            CssSelectorParser parser = new CssSelectorParser();
            var selector = parser.Parse(".class.selected");
            Assert.AreEqual(
                new CssSelectorsGroup(
                    new[] {
                        new CssSelector(
                            new CssSimpleSelectorSequence(
                                new CssUniversalSelector(),
                                new[] {
                                    new CssClassSelector(".class"),
                                    new CssClassSelector(".selected")}))}),
                selector);
        }

        [TestMethod]
        public void TestPseudoRootSelector()
        {
            CssSelectorParser parser = new CssSelectorParser();
            var selector = parser.Parse(":root");
            Assert.AreEqual(
                new CssSelectorsGroup(
                    new[] {
                        new CssSelector(
                            new CssSimpleSelectorSequence(
                                new CssUniversalSelector(),
                                new[] {
                                    new CssPseudoRootSelector()}))}),
                selector);
        }
        [TestMethod]
        public void TestCssPseudoFirstChildSelector()
        {
            CssSelectorParser parser = new CssSelectorParser();
            var selector = parser.Parse(":first-child");
            Assert.AreEqual(
                new CssSelectorsGroup(
                    new[] {
                        new CssSelector(
                            new CssSimpleSelectorSequence(
                                new CssUniversalSelector(),
                                new[] {
                                    new CssPseudoFirstChildSelector()}))}),
                selector);
        }

        [TestMethod]
        public void TestCssPseudoLastChildSelector()
        {
            CssSelectorParser parser = new CssSelectorParser();
            var selector = parser.Parse(":last-child");
            Assert.AreEqual(
                new CssSelectorsGroup(
                    new[] {
                        new CssSelector(
                            new CssSimpleSelectorSequence(
                                new CssUniversalSelector(),
                                new[] {
                                    new CssPseudoLastChildSelector()}))}),
                selector);
        }

        [TestMethod]
        public void TestCssPseudoFirstOfTypeSelector()
        {
            CssSelectorParser parser = new CssSelectorParser();
            var selector = parser.Parse(":first-of-type");
            Assert.AreEqual(
                new CssSelectorsGroup(
                    new[] {
                        new CssSelector(
                            new CssSimpleSelectorSequence(
                                new CssUniversalSelector(),
                                new[] {
                                    new CssPseudoFirstOfTypeSelector()}))}),
                selector);
        }

        [TestMethod]
        public void TestCssPseudoLastOfTypeSelector()
        {
            CssSelectorParser parser = new CssSelectorParser();
            var selector = parser.Parse(":last-of-type");
            Assert.AreEqual(
                new CssSelectorsGroup(
                    new[] {
                        new CssSelector(
                            new CssSimpleSelectorSequence(
                                new CssUniversalSelector(),
                                new[] {
                                    new CssPseudoLastOfTypeSelector()}))}),
                selector);
        }

        [TestMethod]
        public void TestCssPseudoOnlyChildSelector()
        {
            CssSelectorParser parser = new CssSelectorParser();
            var selector = parser.Parse(":only-child");
            Assert.AreEqual(
                new CssSelectorsGroup(
                    new[] {
                        new CssSelector(
                            new CssSimpleSelectorSequence(
                                new CssUniversalSelector(),
                                new[] {
                                    new CssPseudoOnlyChildSelector()}))}),
                selector);
        }

        [TestMethod]
        public void TestCssPseudoOnlyOfTypeSelector()
        {
            CssSelectorParser parser = new CssSelectorParser();
            var selector = parser.Parse(":only-of-type");
            Assert.AreEqual(
                new CssSelectorsGroup(
                    new[] {
                        new CssSelector(
                            new CssSimpleSelectorSequence(
                                new CssUniversalSelector(),
                                new[] {
                                    new CssPseudoOnlyOfTypeSelector()}))}),
                selector);
        }

        [TestMethod]
        public void TestCssPseudoEmptySelector()
        {
            CssSelectorParser parser = new CssSelectorParser();
            var selector = parser.Parse(":empty");
            Assert.AreEqual(
                new CssSelectorsGroup(
                    new[] {
                        new CssSelector(
                            new CssSimpleSelectorSequence(
                                new CssUniversalSelector(),
                                new[] {
                                    new CssPseudoEmptySelector()}))}),
                selector);
        }

        [TestMethod]
        public void TestCssPseudoEnabledSelector()
        {
            CssSelectorParser parser = new CssSelectorParser();
            var selector = parser.Parse(":enabled");
            Assert.AreEqual(
                new CssSelectorsGroup(
                    new[] {
                        new CssSelector(
                            new CssSimpleSelectorSequence(
                                new CssUniversalSelector(),
                                new[] {
                                    new CssPseudoEnabledSelector()}))}),
                selector);
        }

        [TestMethod]
        public void TestCssPseudoDisabledSelector()
        {
            CssSelectorParser parser = new CssSelectorParser();
            var selector = parser.Parse(":disabled");
            Assert.AreEqual(
                new CssSelectorsGroup(
                    new[] {
                        new CssSelector(
                            new CssSimpleSelectorSequence(
                                new CssUniversalSelector(),
                                new[] {
                                    new CssPseudoDisabledSelector()}))}),
                selector);
        }

        [TestMethod]
        public void TestCssPseudoCheckedSelector()
        {
            CssSelectorParser parser = new CssSelectorParser();
            var selector = parser.Parse(":checked");
            Assert.AreEqual(
                new CssSelectorsGroup(
                    new[] {
                        new CssSelector(
                            new CssSimpleSelectorSequence(
                                new CssUniversalSelector(),
                                new[] {
                                    new CssPseudoCheckedSelector()}))}),
                selector);
        }

    }
}
