using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using HtmlSharp.Elements;

namespace HtmlSharp
{
    public class CssSelector
    {
        List<CssSimpleSelector> selectors = new List<CssSimpleSelector>();
        List<CssCombinator> combinators = new List<CssCombinator>();

        public CssSelector(CssSimpleSelector selector)
        {
            selectors.Add(selector);
        }

        public CssSelector(IEnumerable<CssSimpleSelector> selectors, IEnumerable<CssCombinator> combinators)
        {
            this.selectors.AddRange(selectors);
            this.combinators.AddRange(combinators);
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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                CssSelector t = (CssSelector)obj;
                return combinators.SequenceEqual(t.combinators) && selectors.SequenceEqual(t.selectors);
            }
        }

        public override int GetHashCode()
        {
            return combinators.Aggregate(0, (a, b) => a ^= b.GetHashCode()) & selectors.Aggregate(0, (a, b) => a ^= b.GetHashCode());
        }
    }

    public class CssCombinator
    {

    }

    public class CssChildCombinator : CssCombinator
    {
        public override string ToString()
        {
            return ">";
        }
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

        public override bool Equals(object obj)
        {
            return obj.GetType() == GetType();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class CssTypeSelector : CssSimpleSelector
    {
        string name;

        public CssTypeSelector(string name)
        {
            this.name = name;
        }

        public CssTypeSelector(Tag tag)
        {
            name = tag.TagName;
        }
    }

    public class CssSelectorParser
    {
        public CssSelector Parse(string selector)
        {
            List<CssSimpleSelector> simpleSelectors = new List<CssSimpleSelector>();
            List<CssCombinator> combinators = new List<CssCombinator>();

            selector = RemoveUncessaryWhiteSpace(selector).ToLowerInvariant();

            int i = 0;
            while (i < selector.Length)
            {
                if (selector[i] == '*')
                {
                    simpleSelectors.Add(new CssUniversalSelector());
                }
                else if (char.IsLetter(selector, i))
                {
                    //reaad the tag name and update the position
                    string tag = TakeWhile(i, selector, IsTagCharacter);
                    //currentSelector = new CssTypeSelector(
                }
                i++;
            }

            return new CssSelector(simpleSelectors, combinators.ToArray());
        }

        static bool IsTagCharacter(char c)
        {
            return char.IsLetterOrDigit(c) || c == '_' || c == '-';
        }

        string TakeWhile(int index, string selector, Func<char, bool> method)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char c in selector)
            {
                if (method(c))
                {
                    builder.Append(c);
                }
            }

            return builder.ToString();
        }

        string RemoveUncessaryWhiteSpace(string selector)
        {
            selector = Regex.Replace(selector, "[ \t\n\r\f]+", " ");
            //selector = Regex.Replace(selector, " ?[

            return selector;
        }
    }
}
