using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using HtmlSharp.Elements;
using HtmlSharp.Extensions;
using System.Collections.Specialized;

namespace HtmlSharp.Css
{
    public class SelectorParser
    {
        SelectorTokenizer tokenizer = new SelectorTokenizer();
        List<SelectorToken> tokens = new List<SelectorToken>();
        int currentPosition = 0;

        SelectorToken CurrentToken { get { return tokens.ElementAtOrDefault(currentPosition); } }

        public SelectorsGroup Parse(string selector)
        {
            tokens = tokenizer.Tokenize(selector).ToList();

            List<Selector> selectors = new List<Selector>();
            while (currentPosition < tokens.Count)
            {
                selectors.Add(ParseSelector());
                if (new SelectorToken(SelectorTokenType.Comma, ",").Equals(CurrentToken))
                {
                    currentPosition++;
                }
                else if (CurrentToken != null)
                {
                    //parse error
                }
            }

            return new SelectorsGroup(selectors);
        }

        private Selector ParseSelector()
        {
            List<SimpleSelectorSequence> simpleSelectorSequences = new List<SimpleSelectorSequence>();
            List<Combinator> combinators = new List<Combinator>();

            Combinator combinator = null;
            do
            {
                simpleSelectorSequences.Add(ParseSimpleSelectorSequence());

                combinator = ParseCombinator();
                if (combinator != null)
                {
                    combinators.Add(combinator);
                }
            }
            while (combinator != null);

            return new Selector(simpleSelectorSequences, combinators);
        }

        private Combinator ParseCombinator()
        {
            Combinator combinator = null;
            if (CurrentToken != null && CurrentToken.TokenType == SelectorTokenType.Plus)
            {
                combinator = new AdjacentSiblingCombinator();
                currentPosition++;
                SkipWhiteSpace();
            }
            else if (CurrentToken != null && CurrentToken.TokenType == SelectorTokenType.Greater)
            {
                combinator = new ChildCombinator();
                currentPosition++;
                SkipWhiteSpace();
            }
            else if (CurrentToken != null && CurrentToken.TokenType == SelectorTokenType.Tilde)
            {
                combinator = new GeneralSiblingCombinator();
                currentPosition++;
                SkipWhiteSpace();
            }
            else if (CurrentToken != null && CurrentToken.TokenType == SelectorTokenType.WhiteSpace)
            {
                combinator = new DescendantCombinator();
                currentPosition++;
            }

            return combinator;
        }

        private SimpleSelectorSequence ParseSimpleSelectorSequence()
        {
            if (CurrentToken == null)
            {
                //parse error
            }

            TypeSelector typeSelector = ParseTypeSelector() ?? ParseUniversalSelector();

            List<SelectorFilter> filters = new List<SelectorFilter>();
            if (CurrentToken != null)
            {
                while (true)
                {
                    if (CurrentToken == null)
                    {
                        break;
                    }
                    SelectorFilter filterSelector;
                    filterSelector = ParseHashSelector();
                    if (filterSelector != null)
                    {
                        filters.Add(filterSelector);
                        continue;
                    }
                    filterSelector = ParseClassSelector();
                    if (filterSelector != null)
                    {
                        filters.Add(filterSelector);
                        continue;
                    }
                    filterSelector = ParseAttributeSelector();
                    if (filterSelector != null)
                    {
                        filters.Add(filterSelector);
                        continue;
                    }
                    filterSelector = ParsePseudoSelector();
                    if (filterSelector != null)
                    {
                        filters.Add(filterSelector);
                        continue;
                    }
                    filterSelector = ParseNegation();
                    if (filterSelector != null)
                    {
                        filters.Add(filterSelector);
                        continue;
                    }
                    if (filterSelector == null)
                    {
                        break;
                    }
                }
            }
            if (typeSelector == null)
            {
                //there better be a hash, class, attrib, pseudo, or negation!
                if (filters.Count == 0)
                {
                    //parse error
                }
                typeSelector = new UniversalSelector();
            }
            return new SimpleSelectorSequence(typeSelector, filters);
        }

        private SelectorFilter ParseNegation()
        {
            return null;
            throw new NotImplementedException();
        }


        /* '::' starts a pseudo-element, ':' a pseudo-class */
        /* Exceptions: :first-line, :first-letter, :before and :after. */
        /* Note that pseudo-elements are restricted to one per selector and */
        /* occur only in the last simple_selector_sequence. */
        /*This :: notation is introduced by the current document in order to establish a 
         * discrimination between pseudo-classes and pseudo-elements. For compatibility with 
         * existing style sheets, user agents must also accept the previous one-colon notation 
         * for pseudo-elements introduced in CSS levels 1 and 2 (namely, :first-line, 
         * :first-letter, :before and :after). This compatibility is not allowed for the new 
         * pseudo-elements introduced in CSS level 3.
         */
        private SelectorFilter ParsePseudoSelector()
        {
            SelectorFilter selector = null;
            if (CurrentToken.Text == ":")
            {
                currentPosition++;
                if (CurrentToken == null)
                {
                    //parse error
                }
                if (CurrentToken.Text == ":")
                {
                    //element
                    throw new NotImplementedException(":: things arent implemented");
                }
                else if (CurrentToken.TokenType == SelectorTokenType.Ident)
                {
                    //class

                    if (CurrentToken.Text == "root")
                    {
                        selector = new RootFilter();
                    }
                    else if (CurrentToken.Text == "first-child")
                    {
                        selector = new FirstChildFilter();
                    }
                    else if (CurrentToken.Text == "last-child")
                    {
                        selector = new LastChildFilter();
                    }
                    else if (CurrentToken.Text == "first-of-type")
                    {
                        selector = new FirstOfTypeFilter();
                    }
                    else if (CurrentToken.Text == "last-of-type")
                    {
                        selector = new LastOfTypeFilter();
                    }
                    else if (CurrentToken.Text == "only-child")
                    {
                        selector = new OnlyChildFilter();
                    }
                    else if (CurrentToken.Text == "only-of-type")
                    {
                        selector = new OnlyOfTypeFilter();
                    }
                    else if (CurrentToken.Text == "empty")
                    {
                        selector = new EmptyFilter();
                    }
                    else if (CurrentToken.Text == "enabled")
                    {
                        selector = new EnabledFilter();
                    }
                    else if (CurrentToken.Text == "disabled")
                    {
                        selector = new DisabledFilter();
                    }
                    else if (CurrentToken.Text == "checked")
                    {
                        selector = new CheckedFilter();
                    }
                    else
                    {
                        //parse error
                        throw new Exception("Unsupported pseudo selector");
                    }

                    currentPosition++;
                }
                else if (CurrentToken.TokenType == SelectorTokenType.Function)
                {
                    if (CurrentToken.Text == "nth-child(")
                    {
                        currentPosition++;
                        if (CurrentToken == null)
                        {
                            //parse error
                        }
                        SkipWhiteSpace();
                        if (CurrentToken == null)
                        {
                            //parse error
                        }
                        Expression expression = ParseExpression();
                        if (expression == null)
                        {
                            //parse error
                        }
                        selector = new NthChildFilter(expression);
                    }
                    else if (CurrentToken.Text == "nth-last-child(")
                    {

                        currentPosition++;
                        if (CurrentToken == null)
                        {
                            //parse error
                        }
                        SkipWhiteSpace();
                        if (CurrentToken == null)
                        {
                            //parse error
                        }
                        Expression expression = ParseExpression();
                        if (expression == null)
                        {
                            //parse error
                        }
                        selector = new NthLastChildFilter(expression);
                    }
                    else if (CurrentToken.Text == "nth-of-type(")
                    {

                        currentPosition++;
                        if (CurrentToken == null)
                        {
                            //parse error
                        }
                        SkipWhiteSpace();
                        if (CurrentToken == null)
                        {
                            //parse error
                        }
                        Expression expression = ParseExpression();
                        if (expression == null)
                        {
                            //parse error
                        }
                        selector = new NthOfTypeFilter(expression);
                    }
                    else if (CurrentToken.Text == "nth-last-of-type(")
                    {

                        currentPosition++;
                        if (CurrentToken == null)
                        {
                            //parse error
                        }
                        SkipWhiteSpace();
                        if (CurrentToken == null)
                        {
                            //parse error
                        }
                        Expression expression = ParseExpression();
                        if (expression == null)
                        {
                            //parse error
                        }
                        selector = new NthLastOfTypeFilter(expression);
                    }
                    else if (CurrentToken.Text == "lang(")
                    {
                        currentPosition++;
                        if (CurrentToken == null)
                        {
                            //parse error
                        }
                        SkipWhiteSpace();
                        if (CurrentToken == null)
                        {
                            //parse error
                        }
                        if (CurrentToken.TokenType == SelectorTokenType.Ident)
                        {
                            selector = new LangFilter(CurrentToken.Text);
                        }
                        else if (CurrentToken.TokenType == SelectorTokenType.Text)
                        {
                            selector = new LangFilter(CurrentToken.Text.Substring(1, CurrentToken.Text.Length - 2));
                        }

                    }
                    else
                    {
                        //invalid function name
                    }
                    currentPosition++;
                    if (CurrentToken.Text != ")")
                    {
                        //parse error
                    }
                    else
                    {
                        currentPosition++;
                    }
                }

            }
            return selector;
        }

        /* In CSS3, the expressions are identifiers, strings, */
        /* or of the form "an+b" */
        private Expression ParseExpression()
        {
            bool negative = false;
            Expression expression = null;
            if (CurrentToken.Text == "-")
            {
                negative = true;
                currentPosition++;
            }
            if (CurrentToken.TokenType == SelectorTokenType.Dimension || CurrentToken.Text == "n" || CurrentToken.Text == "-n")
            {
                string dimension = CurrentToken.Text;
                int dim;
                string negativeSecond = null;

                Match m = Regex.Match(CurrentToken.Text, @"(\d+n)-(\d+)");
                if (m.Success)
                {
                    dimension = m.Groups[1].Value;
                    negativeSecond = m.Groups[2].Value;
                }

                if (int.TryParse(dimension.Substring(0, dimension.Length - 1), out dim) || CurrentToken.Text == "n" || CurrentToken.Text == "-n")
                {
                    if (CurrentToken.Text == "n")
                    {
                        dim = 1;
                    }
                    else if (CurrentToken.Text == "-n")
                    {
                        dim = -1;
                    }
                    dim = negative ? dim * -1 : dim;
                    currentPosition++;
                    if (CurrentToken.TokenType == SelectorTokenType.Plus)
                    {
                        currentPosition++;
                        if (CurrentToken.TokenType == SelectorTokenType.Number)
                        {
                            //xn+b
                            int num;
                            if (int.TryParse(CurrentToken.Text, out num))
                            {
                                expression = new NumericExpression(dim, num);
                            }
                            else
                            {
                                //parse error
                            }
                        }
                    }
                    else if (negativeSecond != null)
                    {
                        //xn-b
                        int num;
                        if (int.TryParse(negativeSecond, out num))
                        {
                            expression = new NumericExpression(dim, -num);
                            currentPosition--;
                        }
                        else
                        {
                            //parse error
                        }
                    }
                    else if (CurrentToken.Text == ")")
                    {
                        expression = new NumericExpression(dim, 0);
                        currentPosition--;
                    }
                }
                else
                {
                    //parse error
                }
            }
            else if (CurrentToken.TokenType == SelectorTokenType.Number)
            {
                //b
                int num;
                if (int.TryParse(CurrentToken.Text, out num))
                {
                    expression = new NumericExpression(0, num * (negative ? -1 : 1));
                }
                else
                {
                    //parse error
                }
            }
            else if (CurrentToken.TokenType == SelectorTokenType.Ident)
            {
                //odd or even
                if (CurrentToken.Text == "odd")
                {
                    expression = new OddExpression();
                }
                else if (CurrentToken.Text == "even")
                {
                    expression = new OddExpression();
                }
                else
                {
                    //parse error
                }
            }
            else if (CurrentToken.TokenType == SelectorTokenType.String)
            {
                string withoutQuotes = CurrentToken.Text.Substring(1, CurrentToken.Text.Length - 2);
                if (CurrentToken.Text == "odd")
                {
                    expression = new OddExpression();
                }
                else if (CurrentToken.Text == "even")
                {
                    expression = new OddExpression();
                }
                else
                {
                    //parse error
                }
            }

            return expression;
        }

        private SelectorFilter ParseAttributeSelector()
        {
            SelectorFilter selector = null;

            if (CurrentToken.Text == "[")
            {
                currentPosition++;
                SkipWhiteSpace();
                SelectorNamespacePrefix ns = ParseCssNamespacePrefix();
                string attributeType = null;
                if (CurrentToken.TokenType == SelectorTokenType.Ident)
                {
                    attributeType = CurrentToken.Text;
                    currentPosition++;
                }
                else
                {
                    // parse error
                }
                SkipWhiteSpace();
                if (CurrentToken.TokenType == SelectorTokenType.PrefixMatch)
                {
                    currentPosition++;
                    SkipWhiteSpace();
                    if (CurrentToken.TokenType == SelectorTokenType.Ident)
                    {
                        selector = new AttributePrefixFilter(attributeType, CurrentToken.Text);
                    }
                    else if (CurrentToken.TokenType == SelectorTokenType.String)
                    {
                        selector = new AttributePrefixFilter(attributeType, CurrentToken.Text);
                    }
                    else
                    {
                        //parse error lol
                    }
                    currentPosition++;
                    SkipWhiteSpace();
                }
                else if (CurrentToken.TokenType == SelectorTokenType.SuffixMatch)
                {
                    currentPosition++;
                    SkipWhiteSpace();
                    if (CurrentToken.TokenType == SelectorTokenType.Ident)
                    {
                        selector = new AttributeSuffixFilter(attributeType, CurrentToken.Text);
                    }
                    else if (CurrentToken.TokenType == SelectorTokenType.String)
                    {
                        selector = new AttributeSuffixFilter(attributeType, CurrentToken.Text);
                    }
                    else
                    {
                        //parse error lol
                    }
                    currentPosition++;
                    SkipWhiteSpace();
                }
                else if (CurrentToken.TokenType == SelectorTokenType.SubstringMatch)
                {
                    currentPosition++;
                    SkipWhiteSpace();
                    if (CurrentToken.TokenType == SelectorTokenType.Ident)
                    {
                        selector = new AttributeSubstringFilter(attributeType, CurrentToken.Text);
                    }
                    else if (CurrentToken.TokenType == SelectorTokenType.String)
                    {
                        selector = new AttributeSubstringFilter(attributeType, CurrentToken.Text);
                    }
                    else
                    {
                        //parse error lol
                    }
                    currentPosition++;
                    SkipWhiteSpace();
                }
                else if (CurrentToken.Text == "=")
                {
                    currentPosition++;
                    SkipWhiteSpace();
                    if (CurrentToken.TokenType == SelectorTokenType.Ident)
                    {
                        selector = new AttributeExactFilter(attributeType, CurrentToken.Text);
                    }
                    else if (CurrentToken.TokenType == SelectorTokenType.String)
                    {
                        selector = new AttributeExactFilter(attributeType, CurrentToken.Text);
                    }
                    else
                    {
                        //parse error lol
                    }
                    currentPosition++;
                    SkipWhiteSpace();
                }
                else if (CurrentToken.TokenType == SelectorTokenType.Includes)
                {
                    currentPosition++;
                    SkipWhiteSpace();
                    if (CurrentToken.TokenType == SelectorTokenType.Ident)
                    {
                        selector = new AttributeIncludesFilter(attributeType, CurrentToken.Text);
                    }
                    else if (CurrentToken.TokenType == SelectorTokenType.String)
                    {
                        selector = new AttributeIncludesFilter(attributeType, CurrentToken.Text);
                    }
                    else
                    {
                        //parse error lol
                    }
                    currentPosition++;
                    SkipWhiteSpace();
                }
                else if (CurrentToken.TokenType == SelectorTokenType.DashMatch)
                {
                    currentPosition++;
                    SkipWhiteSpace();
                    if (CurrentToken.TokenType == SelectorTokenType.Ident)
                    {
                        selector = new AttributeDashFilter(attributeType, CurrentToken.Text);
                    }
                    else if (CurrentToken.TokenType == SelectorTokenType.String)
                    {
                        selector = new AttributeDashFilter(attributeType, CurrentToken.Text);
                    }
                    else
                    {
                        //parse error lol
                    }
                    currentPosition++;
                    SkipWhiteSpace();
                }
                else if (CurrentToken.Text != "]")
                {
                    //parse error lolz
                }

                if (CurrentToken.Text == "]" && selector == null)
                {
                    selector = new AttributeFilter(attributeType);
                    currentPosition++;
                }
                else if (CurrentToken.Text == "]" && selector != null)
                {
                    currentPosition++;
                }
                else
                {
                    //parse error lolz
                }
            }

            return selector;
        }

        private void SkipWhiteSpace()
        {
            if (CurrentToken.TokenType == SelectorTokenType.WhiteSpace)
            {
                currentPosition++;
            }
        }

        private SelectorFilter ParseClassSelector()
        {
            SelectorFilter selector = null;
            if (CurrentToken.Text == ".")
            {
                currentPosition++;
                if (CurrentToken == null)
                {
                    //parse error
                }
                if (CurrentToken.TokenType == SelectorTokenType.Ident)
                {
                    selector = new ClassFilter("." + CurrentToken.Text);
                }

                currentPosition++;
            }
            return selector;
        }

        private SelectorFilter ParseHashSelector()
        {
            SelectorFilter selector = null;
            if (CurrentToken.TokenType == SelectorTokenType.Hash)
            {
                selector = new IDFilter(CurrentToken.Text);
                currentPosition++;
            }
            return selector;
        }

        private TypeSelector ParseUniversalSelector()
        {
            TypeSelector selector = null;

            int tempPos = currentPosition;
            SelectorNamespacePrefix prefix = ParseCssNamespacePrefix();

            if (CurrentToken != null)
            {
                if (CurrentToken.Text == "*")
                {
                    selector = new UniversalSelector(prefix);
                    currentPosition++;
                }
            }

            return selector;
        }

        private TypeSelector ParseTypeSelector()
        {
            TypeSelector selector = null;

            int tempPos = currentPosition;
            SelectorNamespacePrefix prefix = ParseCssNamespacePrefix();

            if (CurrentToken != null)
            {
                if (CurrentToken.TokenType == SelectorTokenType.Ident)
                {
                    selector = new TypeSelector(CurrentToken.Text, prefix);
                    currentPosition++;
                }
            }

            return selector;
        }

        private SelectorNamespacePrefix ParseCssNamespacePrefix()
        {
            SelectorNamespacePrefix prefix = null;

            if (CurrentToken != null)
            {
                if (CurrentToken.TokenType == SelectorTokenType.Ident)
                {
                    string ident = CurrentToken.Text;
                    currentPosition++;
                    if (CurrentToken == null)
                    {
                        currentPosition--;
                    }
                    else if (CurrentToken.Text == "|")
                    {
                        prefix = new SelectorNamespacePrefix(ident);
                        currentPosition++;
                    }
                    else
                    {
                        currentPosition--;
                    }
                }
                else if (CurrentToken.Text == "*")
                {
                    currentPosition++;
                    if (CurrentToken == null)
                    {
                        currentPosition--;
                    }
                    else if (CurrentToken.Text == "|")
                    {
                        prefix = new SelectorNamespacePrefix("*");
                        currentPosition++;
                    }
                    else
                    {
                        currentPosition--;
                    }
                }
                else if (CurrentToken.Text == "|")
                {
                    //TODO: is this supposed to mean universal selector?
                    prefix = new SelectorNamespacePrefix("");
                    currentPosition++;
                }
            }
            return prefix;
        }
    }


}
