﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using HtmlSharp.Elements;
using HtmlSharp.Extensions;
using System.Collections.Specialized;

namespace HtmlSharp
{
    public class CssSelector
    {
        List<CssSimpleSelectorSequence> selectors = new List<CssSimpleSelectorSequence>();
        List<CssCombinator> combinators = new List<CssCombinator>();

        public CssSelector(CssSimpleSelectorSequence selector)
        {
            selectors.Add(selector);
        }

        public CssSelector(IEnumerable<CssSimpleSelectorSequence> selectors, IEnumerable<CssCombinator> combinators)
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

    public class CssDescendantCombinator : CssCombinator
    {
        public override string ToString()
        {
            return " ";
        }
    }

    public class CssAdjacentSiblingCombinator : CssCombinator
    {
        public override string ToString()
        {
            return "+";
        }
    }

    public class CssGeneralSiblingCombinator : CssCombinator
    {
        public override string ToString()
        {
            return "~";
        }
    }

    public class CssSimpleSelectorSequence
    {
        IEnumerable<CssSimpleSelector> selectors;

        public CssSimpleSelectorSequence(IEnumerable<CssSimpleSelector> selectors)
        {
            this.selectors = selectors;
        }
    }

    public class CssSimpleSelector
    {

    }

    public class CssSelectorFilter
    {

    }

    public class CssHashSelector : CssSelectorFilter
    {
        string hash;

        public CssHashSelector(string hash)
        {
            this.hash = hash;
        }
    }

    public class CssClassSelector : CssSelectorFilter
    {
        string klass;

        public CssClassSelector(string klass)
        {
            this.klass = klass;
        }
    }

    public class CssAttributeSelector : CssSelectorFilter
    {

    }

    public class CssPseudoSelector : CssSelectorFilter
    {

    }

    public class CssNegationSelector : CssSelectorFilter
    {

    }

    public class CssTypeSelector
    {
        public string Name { get; private set; }
        public CssSelectorNamespacePrefix Namespace { get; private set; }

        public CssTypeSelector(string name)
        {
            this.Name = name;
        }

        public CssTypeSelector(string name, CssSelectorNamespacePrefix prefix)
            : this(name)
        {
            this.Namespace = prefix;
        }



        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                CssTypeSelector t = (CssTypeSelector)obj;
                return Name == t.Name;
            }
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }

    public class CssUniversalSelector : CssTypeSelector
    {
        public CssUniversalSelector()
            : base("*")
        {

        }

        public CssUniversalSelector(CssSelectorNamespacePrefix prefix)
            : base("*", prefix)
        {

        }

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

    public enum CssSelectorTokenType
    {
        WhiteSpace,
        Includes,
        DashMatch,
        PrefixMatch,
        SuffixMatch,
        SubstringMatch,
        Ident,
        String,
        Function,
        Number,
        Hash,
        Plus,
        Greater,
        Comma,
        Tilde,
        Not,
        AtKeyword,
        Invalid,
        Percentage,
        Dimension,
        CommentOpen,
        CommentClose,
        Uri,
        UnicodeRange,
        Text
    }

    public class CssSelectorToken
    {
        public CssSelectorTokenType TokenType { get; set; }
        public string Text { get; set; }

        public CssSelectorToken(CssSelectorTokenType type, string text)
        {
            this.TokenType = type;
            this.Text = text;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                CssSelectorToken t = (CssSelectorToken)obj;
                return Text == t.Text && object.Equals(TokenType, t.TokenType);
            }
        }

        public override int GetHashCode()
        {
            return Text.GetHashCode() ^ TokenType.GetHashCode();
        }
    }

    public class CssSelectorTokenizer
    {
        static readonly Regex unicode = new Regex("\\\\[0-9a-f]{1,6}(\r\n|[ \n\r\t\f])?");
        static readonly Regex escape = new Regex(string.Format("({0})|\\\\[^\n\r\f0-9a-f]", unicode));
        static readonly Regex nonascii = new Regex("[^\x0-\x7F]");
        static readonly Regex nmchar = new Regex(string.Format("[_a-z0-9-]|({0})|({1})", nonascii, escape));
        static readonly Regex nmstart = new Regex(string.Format("[_a-z]|({0})|({1})", nonascii, escape));
        static readonly Regex ident = new Regex(string.Format("[-]?({0})({1})*", nmstart, nmchar));
        static readonly Regex name = new Regex(string.Format("({0})+", nmchar));
        static readonly Regex num = new Regex(@"[0-9]+|[0-9]*\.[0-9]+");
        static readonly Regex nl = new Regex("\n|\r\n|\r|\f");
        static readonly Regex str1 = new Regex(string.Format("\"([^\n\r\f\\\"]|\\\\({0})|({1})|({2}))*\"", nl, nonascii, escape));
        static readonly Regex str2 = new Regex(string.Format("'([^\n\r\f\\']|\\\\({0})|({1})|({2}))*'", nl, nonascii, escape));
        static readonly Regex str = new Regex(string.Format("({0})|({1})", str1, str2));
        static readonly Regex invalid1 = new Regex(string.Format("\"([^\n\r\f\\\"]|\\\\({0})|({1})|({2}))*", nl, nonascii, escape));
        static readonly Regex invalid2 = new Regex(string.Format("'([^\n\r\f\\']|\\\\({0})|({1})|({2}))*", nl, nonascii, escape));
        static readonly Regex invalid = new Regex(string.Format("({0})|({1})", invalid1, invalid2));
        static readonly Regex w = new Regex("[ \t\r\n\f]*");

        //(\, *, +, ?, |, {, [, (,), ^, $,., #, and white space)
        static readonly Regex s = new Regex("[ \t\r\n\f]+");
        static readonly Regex includes = new Regex("~=");

        static readonly Regex dashmatch = new Regex(@"\|=");
        static readonly Regex prefixmatch = new Regex(@"\^=");
        static readonly Regex suffixMatch = new Regex(@"\$=");
        static readonly Regex substringMatch = new Regex(@"\*=");

        static readonly Regex function = new Regex(string.Format(@"({0})\(", ident));
        static readonly Regex hash = new Regex(string.Format("#({0})", name));
        static readonly Regex plus = new Regex(string.Format(@"({0})\+", w));
        static readonly Regex greater = new Regex(string.Format(@"({0})>", w));
        static readonly Regex comma = new Regex(string.Format(@"({0}),", w));
        static readonly Regex tilde = new Regex(string.Format(@"({0})~", w));
        static readonly Regex not = new Regex(@":not\(");
        static readonly Regex atKeyword = new Regex(string.Format("@({0})", ident));
        static readonly Regex percentage = new Regex(string.Format("({0})%", num));
        static readonly Regex dimension = new Regex(string.Format("({0})({1})", num, ident));
        static readonly Regex cdo = new Regex("<!--");
        static readonly Regex cdc = new Regex("-->");

        static readonly Regex uri = new Regex(string.Format(@"url\(({0})({1})({0})\)", w, str));
        static readonly Regex uri2 = new Regex(string.Format(@"url\(({0})([!#$%&*-~]|({1})|({2}))*({0})\)", w, nonascii, escape));
        static readonly Regex unicodeRange = new Regex(@"U\+[0-9a-f?]{1,6}(-[0-9a-f]{1,6})?");
        static readonly Regex comment = new Regex(@"/\*[^*]*\*+([^/*][^*]*\*+)*/");

        static Dictionary<Regex, CssSelectorTokenType> tokenMap = new Dictionary<Regex, CssSelectorTokenType>()
        {
            {s, CssSelectorTokenType.WhiteSpace},
            {includes, CssSelectorTokenType.Includes},
            {dashmatch, CssSelectorTokenType.DashMatch},
            {prefixmatch, CssSelectorTokenType.PrefixMatch},
            {suffixMatch, CssSelectorTokenType.SuffixMatch},
            {substringMatch,  CssSelectorTokenType.SubstringMatch},
            {ident, CssSelectorTokenType.Ident},
            {str, CssSelectorTokenType.String},
            {function, CssSelectorTokenType.Function},
            {num, CssSelectorTokenType.Number},
            {hash, CssSelectorTokenType.Hash},
            {plus, CssSelectorTokenType.Plus},
            {greater, CssSelectorTokenType.Greater},
            {comma, CssSelectorTokenType.Comma},
            {tilde, CssSelectorTokenType.Tilde},
            {not, CssSelectorTokenType.Not},
            {atKeyword, CssSelectorTokenType.AtKeyword},
            {invalid, CssSelectorTokenType.Invalid},
            {percentage, CssSelectorTokenType.Percentage},
            {dimension, CssSelectorTokenType.Dimension},
            {cdo, CssSelectorTokenType.CommentOpen},
            {cdc, CssSelectorTokenType.CommentClose},
            {uri, CssSelectorTokenType.Uri},
            {uri2, CssSelectorTokenType.Uri},
            {unicodeRange, CssSelectorTokenType.UnicodeRange}
        };

        static List<Regex> tokenMatchers = new List<Regex>()
        {
            unicodeRange, uri, uri2, cdc, cdo, dimension, percentage, atKeyword, not, includes, tilde, comma, greater,
            plus, hash, num, function, str, ident, substringMatch, suffixMatch, prefixmatch, dashmatch, s, invalid
        };

        public IEnumerable<CssSelectorToken> Tokenize(string input)
        {
            int currentPosition = 0;
            while (currentPosition < input.Length)
            {
                var token = tokenMatchers.FirstOrDefault(t => t.MatchAtIndex(input, currentPosition).Success);
                if (token != null)
                {
                    Match m = token.MatchAtIndex(input, currentPosition);
                    yield return new CssSelectorToken(tokenMap[token], m.Value);
                    currentPosition += m.Length;
                    continue;
                }
                else
                {
                    yield return new CssSelectorToken(CssSelectorTokenType.Text, input[currentPosition].ToString());
                    currentPosition++;
                }
            }
        }
    }

    public class CssSelectorsGroup
    {
        IEnumerable<CssSelector> selectors;

        public CssSelectorsGroup(IEnumerable<CssSelector> selectors)
        {
            this.selectors = selectors;
        }
    }

    public class CssSelectorParser
    {
        CssSelectorTokenizer tokenizer = new CssSelectorTokenizer();
        List<CssSelectorToken> tokens = new List<CssSelectorToken>();
        int currentPosition = 0;

        CssSelectorToken CurrentToken { get { return tokens.ElementAtOrDefault(currentPosition); } }

        public CssSelectorsGroup Parse(string selector)
        {
            tokens = tokenizer.Tokenize(selector).ToList();

            List<CssSelector> selectors = new List<CssSelector>();
            while (currentPosition < tokens.Count)
            {
                selectors.Add(ParseSelector());
                if (new CssSelectorToken(CssSelectorTokenType.Text, ",").Equals(CurrentToken))
                {
                    currentPosition++;
                }
                else if (CurrentToken != null)
                {
                    //parse error
                }
            }

            return new CssSelectorsGroup(selectors);
        }

        private CssSelector ParseSelector()
        {
            List<CssSimpleSelectorSequence> simpleSelectorSequences = new List<CssSimpleSelectorSequence>();
            List<CssCombinator> combinators = new List<CssCombinator>();

            CssCombinator combinator = null;
            while (combinator == null)
            {
                simpleSelectorSequences.Add(ParseSimpleSelectorSequence());

                combinator = ParseCombinator();
                if (combinator != null)
                {
                    combinators.Add(combinator);
                }
            }

            return new CssSelector(simpleSelectorSequences, combinators);
        }

        private CssCombinator ParseCombinator()
        {
            CssCombinator combinator = null;
            if (CurrentToken != null && CurrentToken.TokenType == CssSelectorTokenType.Plus)
            {
                combinator = new CssAdjacentSiblingCombinator();
                currentPosition++;
                if (CurrentToken != null && CurrentToken.TokenType == CssSelectorTokenType.WhiteSpace)
                {
                    currentPosition++;
                }
            }
            else if (CurrentToken != null && CurrentToken.TokenType == CssSelectorTokenType.Greater)
            {
                combinator = new CssChildCombinator();
                currentPosition++;
                if (CurrentToken != null && CurrentToken.TokenType == CssSelectorTokenType.WhiteSpace)
                {
                    currentPosition++;
                }
            }
            else if (CurrentToken != null && CurrentToken.TokenType == CssSelectorTokenType.Tilde)
            {
                combinator = new CssGeneralSiblingCombinator();
                currentPosition++;
                if (CurrentToken != null && CurrentToken.TokenType == CssSelectorTokenType.WhiteSpace)
                {
                    currentPosition++;
                }
            }
            else if (CurrentToken != null && CurrentToken.TokenType == CssSelectorTokenType.WhiteSpace)
            {
                combinator = new CssDescendantCombinator();
                currentPosition++;
            }

            return combinator;
        }

        private CssSimpleSelectorSequence ParseSimpleSelectorSequence()
        {
            if (CurrentToken == null)
            {
                //parse error
            }

            CssTypeSelector typeSelector = ParseTypeSelector() ?? ParseUniversalSelector();

            List<CssSelectorFilter> filters = new List<CssSelectorFilter>();

            while (true)
            {
                CssSelectorFilter filterSelector;
                filterSelector = ParseHashSelector();
                if (filterSelector != null)
                {
                    filters.Add(filterSelector);
                }
                filterSelector = ParseClassSelector();
                if (filterSelector != null)
                {
                    filters.Add(filterSelector);
                }
                filterSelector = ParseAttributeSelector();
                if (filterSelector != null)
                {
                    filters.Add(filterSelector);
                }
            }
            if (typeSelector == null)
            {
                //there better be a hash, class, attrib, pseudo, or negation!

            }
            

            throw new NotImplementedException();
        }

        private CssSelectorFilter ParseAttributeSelector()
        {
            throw new NotImplementedException();
        }

        private CssSelectorFilter ParseClassSelector()
        {
            CssSelectorFilter selector = null;
            if (CurrentToken.Text == ".")
            {
                currentPosition++;
                if (CurrentToken == null)
                {
                    //parse error
                }
                if (CurrentToken.TokenType == CssSelectorTokenType.Ident)
                {
                    selector = new CssClassSelector("." + CurrentToken.Text);
                }
                
                currentPosition++;
            }
            return selector;
        }

        private CssSelectorFilter ParseHashSelector()
        {
            CssSelectorFilter selector = null;
            if (CurrentToken.TokenType == CssSelectorTokenType.Hash)
            {
                selector = new CssHashSelector(CurrentToken.Text);
                currentPosition++;
            }
            return selector;
        }

        private CssTypeSelector ParseUniversalSelector()
        {
            CssTypeSelector selector = null;

            int tempPos = currentPosition;
            CssSelectorNamespacePrefix prefix = ParseCssNamespacePrefix();

            if (CurrentToken != null)
            {
                if (CurrentToken.Text == "*")
                {
                    selector = new CssUniversalSelector(prefix);
                }
            }

            return selector;
        }

        private CssTypeSelector ParseTypeSelector()
        {
            CssTypeSelector selector = null;

            int tempPos = currentPosition;
            CssSelectorNamespacePrefix prefix = ParseCssNamespacePrefix();

            if (CurrentToken != null)
            {
                if (CurrentToken.TokenType == CssSelectorTokenType.Ident)
                {
                    selector = new CssTypeSelector(CurrentToken.Text, prefix);
                }
            }

            return selector;
        }

        private CssSelectorNamespacePrefix ParseCssNamespacePrefix()
        {
            CssSelectorNamespacePrefix prefix = null;

            if (CurrentToken != null)
            {
                if (CurrentToken.TokenType == CssSelectorTokenType.Ident)
                {
                    string ident = CurrentToken.Text;
                    currentPosition++;
                    if (CurrentToken == null)
                    {
                        currentPosition--;
                    }
                    else if (CurrentToken.Text == "|")
                    {
                        prefix = new CssSelectorNamespacePrefix(ident);
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
                        prefix = new CssSelectorNamespacePrefix("*");
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
                    prefix = new CssSelectorNamespacePrefix("");
                    currentPosition++;
                }
            }
            return prefix;
        }
    }

    public class CssSelectorNamespacePrefix
    {
        public string Namespace { get; private set; }
        public CssSelectorNamespacePrefix(string ns)
        {
            this.Namespace = ns;
        }
    }
}