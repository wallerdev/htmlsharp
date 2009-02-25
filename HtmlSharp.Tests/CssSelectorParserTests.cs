using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HtmlSharp.Elements.Tags;
using HtmlSharp.Css;

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
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse("a");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new TypeSelector("a"), 
                                new IFilter[0]) )}), 
                selector);
        }

        [TestMethod]
        public void TestUniversalSelector()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse("*");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(), 
                                new IFilter[0]) )}),
                selector);
        }

        [TestMethod]
        public void TestMultipleSelectors()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse("a,div");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new TypeSelector("a"), new IFilter[0])),
                        new Selector(
                            new SimpleSelectorSequence(
                                new TypeSelector("div"), new IFilter[0]))}), 
                selector);
        }

        [TestMethod]
        public void TestDescendentCombinator()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse("div a");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new[] {
                                new SimpleSelectorSequence(
                                   new TypeSelector("div"), new IFilter[0]),
                               new SimpleSelectorSequence(
                                   new TypeSelector("a"), new IFilter[0])},
                            new[] {
                                new DescendantCombinator()
                            })}),
                selector);
        }

        [TestMethod]
        public void TestChildCombinator()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse("div > a");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new[] {
                                new SimpleSelectorSequence(
                                   new TypeSelector("div"), new IFilter[0]),
                               new SimpleSelectorSequence(
                                   new TypeSelector("a"), new IFilter[0])},
                            new[] {
                                new ChildCombinator()
                            })}),
                selector);
        }

        [TestMethod]
        public void TestAdjacentSiblingCombinator()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse("div + a");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new[] {
                                new SimpleSelectorSequence(
                                   new TypeSelector("div"), new IFilter[0]),
                               new SimpleSelectorSequence(
                                   new TypeSelector("a"), new IFilter[0])},
                            new[] {
                                new AdjacentSiblingCombinator()
                            })}),
                selector);
        }

        [TestMethod]
        public void TestMultipleCombinators()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse("div > table tr > td");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new[] {
                                new SimpleSelectorSequence(
                                   new TypeSelector("div"), new IFilter[0]),
                               new SimpleSelectorSequence(
                                   new TypeSelector("table"), new IFilter[0]),
                                new SimpleSelectorSequence(
                                   new TypeSelector("tr"), new IFilter[0]),
                               new SimpleSelectorSequence(
                                   new TypeSelector("td"), new IFilter[0])},
                            new Combinator[] {
                                new ChildCombinator(),
                                new DescendantCombinator(),
                                new ChildCombinator()
                            })}),
                selector);
        }

        [TestMethod]
        public void TestGeneralSiblingCombinator()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse("div ~ a");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new[] {
                                new SimpleSelectorSequence(
                                   new TypeSelector("div"), new IFilter[0]),
                               new SimpleSelectorSequence(
                                   new TypeSelector("a"), new IFilter[0])},
                            new[] {
                                new GeneralSiblingCombinator()
                            })}),
                selector);
        }

        [TestMethod]
        public void TestClassFilter()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse(".class");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new ClassFilter("class")}))}), 
                selector);
        }

        [TestMethod]
        public void TestIdFilter()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse("#id");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new IDFilter("id")}))}),
                selector);
        }

        [TestMethod]
        public void TestAttributePresentFilter()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse("[href]");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new AttributeFilter("href")}))}),
                selector);
        }

        [TestMethod]
        public void TestAttributePrefixFilter()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse("[href^=http]");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new AttributePrefixFilter("href", "http")}))}),
                selector);
        }

        [TestMethod]
        public void TestAttributeSuffixFilter()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse("[href$=html]");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new AttributeSuffixFilter("href", "html")}))}),
                selector);
        }

        [TestMethod]
        public void TestAttributeSubstringMatcFilter()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse("[href*=www]");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new AttributeSubstringFilter("href", "www")}))}),
                selector);
        }

        [TestMethod]
        public void TestAttributeExactMatcFilter()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse("[name=username]");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new AttributeExactFilter("name", "username")}))}),
                selector);
        }

        [TestMethod]
        public void TestAttributeIncludesSelector()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse("[rel~=copyright]");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new AttributeIncludesFilter("rel", "copyright")}))}),
                selector);
        }

        [TestMethod]
        public void TestAttributeDashSelector()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse("[hreflang|=en]");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new AttributeDashFilter("hreflang", "en")}))}),
                selector);
        }

        [TestMethod]
        public void TestMultipleFilters()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse(".class.selected");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new ClassFilter("class"),
                                    new ClassFilter("selected")}))}),
                selector);
        }

        [TestMethod]
        public void TestPseudoRootSelector()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse(":root");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new RootFilter()}))}),
                selector);
        }
        [TestMethod]
        public void TestCssPseudoFirstChildSelector()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse(":first-child");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new FirstChildFilter()}))}),
                selector);
        }

        [TestMethod]
        public void TestCssPseudoLastChildSelector()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse(":last-child");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new LastChildFilter()}))}),
                selector);
        }

        [TestMethod]
        public void TestCssPseudoFirstOfTypeSelector()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse(":first-of-type");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new FirstOfTypeFilter()}))}),
                selector);
        }

        [TestMethod]
        public void TestCssPseudoLastOfTypeSelector()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse(":last-of-type");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new LastOfTypeFilter()}))}),
                selector);
        }

        [TestMethod]
        public void TestCssPseudoOnlyChildSelector()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse(":only-child");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new OnlyChildFilter()}))}),
                selector);
        }

        [TestMethod]
        public void TestCssPseudoOnlyOfTypeSelector()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse(":only-of-type");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new OnlyOfTypeFilter()}))}),
                selector);
        }

        [TestMethod]
        public void TestCssPseudoEmptySelector()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse(":empty");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new EmptyFilter()}))}),
                selector);
        }

        [TestMethod]
        public void TestCssPseudoEnabledSelector()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse(":enabled");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new EnabledFilter()}))}),
                selector);
        }

        [TestMethod]
        public void TestCssPseudoDisabledSelector()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse(":disabled");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new DisabledFilter()}))}),
                selector);
        }

        [TestMethod]
        public void TestCssPseudoCheckedSelector()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse(":checked");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new CheckedFilter()}))}),
                selector);
        }

        [TestMethod]
        public void TestCssNthChildFilter()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse(":nth-child(n)");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new NthChildFilter(
                                        new NumericExpression(1, 0))}))}),
                selector);
        }

        [TestMethod]
        public void TestCssNthLastChildFilter()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse(":nth-last-child(n)");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new NthLastChildFilter(
                                        new NumericExpression(1, 0))}))}),
                selector);
        }

        [TestMethod]
        public void TestCssNthOfTypeFilter()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse(":nth-of-type(n)");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new NthOfTypeFilter(
                                        new NumericExpression(1, 0))}))}),
                selector);
        }

        [TestMethod]
        public void TestCssNthLastOfTypeFilter()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse(":nth-last-of-type(n)");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new NthLastOfTypeFilter(
                                        new NumericExpression(1, 0))}))}),
                selector);
        }

        [TestMethod]
        public void TestCssLangFilter()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse(":lang(en)");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new LangFilter("en")}))}),
                selector);
        }

        [TestMethod]
        public void TestCssExpressionN()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse(":nth-child(n)");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new NthChildFilter(
                                        new NumericExpression(1, 0))}))}),
                selector);
        }

        [TestMethod]
        public void TestCssExpressionNumber()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse(":nth-child(2)");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new NthChildFilter(
                                        new NumericExpression(0, 2))}))}),
                selector);
        }

        [TestMethod]
        public void TestCssExpressionNumericN()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse(":nth-child(2n)");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new NthChildFilter(
                                        new NumericExpression(2, 0))}))}),
                selector);
        }

        [TestMethod]
        public void TestCssExpressionNumericNPlusNumeric()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse(":nth-child(2n+1)");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new NthChildFilter(
                                        new NumericExpression(2, 1))}))}),
                selector);
        }

        [TestMethod]
        public void TestCssExpressionNumericNMinusNumeric()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse(":nth-child(2n-1)");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new NthChildFilter(
                                        new NumericExpression(2, -1))}))}),
                selector);
        }

        [TestMethod]
        public void TestCssExpressionNegativeNumericNMinusNumeric()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse(":nth-child(-2n-1)");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new NthChildFilter(
                                        new NumericExpression(-2, -1))}))}),
                selector);
        }

        [TestMethod]
        public void TestCssExpressionNegativeNumericN()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse(":nth-child(-2n)");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new NthChildFilter(
                                        new NumericExpression(-2, 0))}))}),
                selector);
        }

       [TestMethod]
        public void TestCssExpressionNegativeN()
        {
            SelectorParser parser = new SelectorParser();
            var selector = parser.Parse(":nth-child(-n)");
            Assert.AreEqual(
                new SelectorsGroup(
                    new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new NthChildFilter(
                                        new NumericExpression(-1, 0))}))}),
                selector);
        }

       [TestMethod]
       public void TestCssNegationUniversalFilter()
       {
           SelectorParser parser = new SelectorParser();
           var selector = parser.Parse(":not(*)");
           Assert.AreEqual(
               new SelectorsGroup(
                   new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new NegationTypeFilter(
                                        new UniversalSelector())}))}),
               selector);
       }

       [TestMethod]
       public void TestCssNegationTypeFilter()
       {
           SelectorParser parser = new SelectorParser();
           var selector = parser.Parse(":not(p)");
           Assert.AreEqual(
               new SelectorsGroup(
                   new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new NegationTypeFilter(
                                        new TypeSelector("p"))}))}),
               selector);
       }

       [TestMethod]
       public void TestCssNegationAttributeFilter()
       {
           SelectorParser parser = new SelectorParser();
           var selector = parser.Parse(":not([align=right])");
           Assert.AreEqual(
               new SelectorsGroup(
                   new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new NegationFilter(
                                        new AttributeExactFilter("align", "right"))}))}),
               selector);
       }

       [TestMethod]
       public void TestCssNegationIDFilter()
       {
           SelectorParser parser = new SelectorParser();
           var selector = parser.Parse(":not(#id)");
           Assert.AreEqual(
               new SelectorsGroup(
                   new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new NegationFilter(
                                        new IDFilter("id"))}))}),
               selector);
       }

       [TestMethod]
       public void TestCssNegationClassFilter()
       {
           SelectorParser parser = new SelectorParser();
           var selector = parser.Parse(":not(.class)");
           Assert.AreEqual(
               new SelectorsGroup(
                   new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new NegationFilter(
                                        new ClassFilter("class"))}))}),
               selector);
       }

       [TestMethod]
       public void TestCssNegationPseudoFilter()
       {
           SelectorParser parser = new SelectorParser();
           var selector = parser.Parse(":not(:nth-child(2))");
           Assert.AreEqual(
               new SelectorsGroup(
                   new[] {
                        new Selector(
                            new SimpleSelectorSequence(
                                new UniversalSelector(),
                                new[] {
                                    new NegationFilter(
                                        new NthChildFilter(
                                            new NumericExpression(0, 2)))}))}),
               selector);
       }
    }
}
