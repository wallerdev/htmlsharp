using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HtmlSharp
{
    public class CssSelector
    {
        List<CssSimpleSelector> selectors = new List<CssSimpleSelector>();
        List<CssCombinator> combinators = new List<CssCombinator>();

        public void Add(CssSimpleSelector selector)
        {
            selectors.Add(selector);
        }

        public void Add(CssCombinator combinator)
        {
            combinators.Add(combinator);
        }

        public override string ToString()
        {
            StringBuilder selectorBuilder = new StringBuilder();
            for (int i = 0; i < selectors.Count; i++)
            {
                selectorBuilder.Append(selectors[i]);
                if (combinators.Count > i)
                {
                    selectorBuilder.Append(combinators[i]);
                }
            }

            return selectorBuilder.ToString();
        }
    }

    public class CssCombinator
    {

    }

    public class CssSimpleSelector
    {

    }

    public class CssUniversalSelector : CssSimpleSelector
    {
        public override string ToString()
        {
            return "*";
        }
    }

    public class CssSelectorParser
    {
        public CssSelector Parse(string selector)
        {
            CssSelector result = new CssSelector();

            selector = RemoveUncessaryWhiteSpace(selector);

            CssSimpleSelector currentSelector = null;

            int i = 0;
            while (i < selector.Length)
            {
                if (selector[i] == '*')
                {
                    currentSelector = new CssUniversalSelector();
                }
                i++;
            }

            result.Add(currentSelector);

            return result;
        }

        string RemoveUncessaryWhiteSpace(string selector)
        {
            selector = Regex.Replace(selector, "[ \t\n\r\f]+", " ");
            //selector = Regex.Replace(selector, " ?[

            return selector;
        }
    }
}
