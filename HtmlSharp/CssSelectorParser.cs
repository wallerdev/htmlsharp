using System;
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
        public override bool Equals(object obj)
        {
            return obj != null && GetType() == obj.GetType();
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode();
        }
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
        IEnumerable<CssSelectorFilter> filters;
        CssTypeSelector selector;

        public CssSimpleSelectorSequence(CssTypeSelector selector, IEnumerable<CssSelectorFilter> filters)
        {
            this.selector = selector;
            this.filters = filters;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                CssSimpleSelectorSequence t = (CssSimpleSelectorSequence)obj;
                return selector.Equals(t.selector) && filters.SequenceEqual(t.filters);
            }
        }

        public override int GetHashCode()
        {
            return selector.GetHashCode() ^ filters.Aggregate(0, (a, b) => a ^= b.GetHashCode());
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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                CssHashSelector t = (CssHashSelector)obj;
                return hash.Equals(t.hash);
            }
        }

        public override int GetHashCode()
        {
            return hash.GetHashCode();
        }
    }

    public class CssClassSelector : CssSelectorFilter
    {
        string klass;

        public CssClassSelector(string klass)
        {
            this.klass = klass;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                CssClassSelector t = (CssClassSelector)obj;
                return klass.Equals(t.klass);
            }
        }

        public override int GetHashCode()
        {
            return klass.GetHashCode();
        }
    }

    public class CssAttributeSelector : CssSelectorFilter
    {
        string type;

        public CssAttributeSelector(string type)
        {
            this.type = type;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                CssAttributeSelector t = (CssAttributeSelector)obj;
                return type.Equals(t.type);
            }
        }

        public override int GetHashCode()
        {
            return type.GetHashCode();
        }
    }

    public class CssPseudoRootSelector : CssSelectorFilter
    {
        public CssPseudoRootSelector()
        {

        }

        public override bool Equals(object obj)
        {
            return obj != null && GetType() == obj.GetType();
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode();
        }
    }

    public class CssPseudoFirstChildSelector : CssSelectorFilter
    {
        public override bool Equals(object obj)
        {
            return obj != null && GetType() == obj.GetType();
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode();
        }
    }

    public class CssPseudoLastChildSelector : CssSelectorFilter
    {
        public override bool Equals(object obj)
        {
            return obj != null && GetType() == obj.GetType();
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode();
        }
    }

    public class CssPseudoFirstOfTypeSelector : CssSelectorFilter
    {
        public override bool Equals(object obj)
        {
            return obj != null && GetType() == obj.GetType();
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode();
        }
    }

    public class CssPseudoLastOfTypeSelector : CssSelectorFilter
    {
        public override bool Equals(object obj)
        {
            return obj != null && GetType() == obj.GetType();
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode();
        }
    }

    public class CssPseudoOnlyChildSelector : CssSelectorFilter
    {
        public override bool Equals(object obj)
        {
            return obj != null && GetType() == obj.GetType();
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode();
        }
    }

    public class CssPseudoOnlyOfTypeSelector : CssSelectorFilter
    {
        public override bool Equals(object obj)
        {
            return obj != null && GetType() == obj.GetType();
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode();
        }
    }

    public class CssPseudoEmptySelector : CssSelectorFilter
    {
        public override bool Equals(object obj)
        {
            return obj != null && GetType() == obj.GetType();
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode();
        }
    }

    public class CssPseudoEnabledSelector : CssSelectorFilter
    {
        public override bool Equals(object obj)
        {
            return obj != null && GetType() == obj.GetType();
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode();
        }
    }

    public class CssPseudoDisabledSelector : CssSelectorFilter
    {
        public override bool Equals(object obj)
        {
            return obj != null && GetType() == obj.GetType();
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode();
        }
    }

    public class CssPseudoCheckedSelector : CssSelectorFilter
    {
        public override bool Equals(object obj)
        {
            return obj != null && GetType() == obj.GetType();
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode();
        }
    }

    public class CssNegationSelector : CssSelectorFilter
    {
        string negation;

        public CssNegationSelector(string negation)
        {
            this.negation = negation;
        }
    }

    public class CssAttributePrefixMatchSelector : CssAttributeSelector
    {
        string prefix;

        public CssAttributePrefixMatchSelector(string type, string prefix)
            : base(type)
        {
            this.prefix = prefix;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                CssAttributePrefixMatchSelector t = (CssAttributePrefixMatchSelector)obj;
                return prefix.Equals(t.prefix) && base.Equals(obj);
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ prefix.GetHashCode();
        }
    }

    public class CssAttributeSuffixMatchSelector : CssAttributeSelector
    {
        string suffix;

        public CssAttributeSuffixMatchSelector(string type, string suffix)
            : base(type)
        {
            this.suffix = suffix;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                CssAttributeSuffixMatchSelector t = (CssAttributeSuffixMatchSelector)obj;
                return suffix.Equals(t.suffix) && base.Equals(obj);
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ suffix.GetHashCode();
        }
    }

    public class CssAttributeSubstringMatchSelector : CssAttributeSelector
    {
        string substring;

        public CssAttributeSubstringMatchSelector(string type, string substring)
            : base(type)
        {
            this.substring = substring;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                CssAttributeSubstringMatchSelector t = (CssAttributeSubstringMatchSelector)obj;
                return substring.Equals(t.substring) && base.Equals(obj);
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ substring.GetHashCode();
        }
    }

    public class CssAttributeExactMatchSelector : CssAttributeSelector
    {
        string text;

        public CssAttributeExactMatchSelector(string type, string text)
            : base(type)
        {
            this.text = text;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                CssAttributeExactMatchSelector t = (CssAttributeExactMatchSelector)obj;
                return text.Equals(t.text) && base.Equals(obj);
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ text.GetHashCode();
        }
    }

    public class CssAttributeIncludesSelector : CssAttributeSelector
    {
        string includes;

        public CssAttributeIncludesSelector(string type, string includes)
            : base(type)
        {
            this.includes = includes;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                CssAttributeIncludesSelector t = (CssAttributeIncludesSelector)obj;
                return includes.Equals(t.includes) && base.Equals(obj);
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ includes.GetHashCode();
        }
    }

    public class CssAttributeDashMatchSelector : CssAttributeSelector
    {
        string dash;

        public CssAttributeDashMatchSelector(string type, string dash)
            : base(type)
        {
            this.dash = dash;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                CssAttributeDashMatchSelector t = (CssAttributeDashMatchSelector)obj;
                return dash.Equals(t.dash) && base.Equals(obj);
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ dash.GetHashCode();
        }
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
                return Name == t.Name && Namespace == t.Namespace;
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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                CssSelectorsGroup t = (CssSelectorsGroup)obj;
                return selectors.SequenceEqual(t.selectors);
            }
        }

        public override int GetHashCode()
        {
            return selectors.Aggregate(0, (a, b) => a ^= b.GetHashCode());
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
                if (new CssSelectorToken(CssSelectorTokenType.Comma, ",").Equals(CurrentToken))
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

            return new CssSelector(simpleSelectorSequences, combinators);
        }

        private CssCombinator ParseCombinator()
        {
            CssCombinator combinator = null;
            if (CurrentToken != null && CurrentToken.TokenType == CssSelectorTokenType.Plus)
            {
                combinator = new CssAdjacentSiblingCombinator();
                currentPosition++;
                SkipWhiteSpace();
            }
            else if (CurrentToken != null && CurrentToken.TokenType == CssSelectorTokenType.Greater)
            {
                combinator = new CssChildCombinator();
                currentPosition++;
                SkipWhiteSpace();
            }
            else if (CurrentToken != null && CurrentToken.TokenType == CssSelectorTokenType.Tilde)
            {
                combinator = new CssGeneralSiblingCombinator();
                currentPosition++;
                SkipWhiteSpace();
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
            if (CurrentToken != null)
            {
                while (true)
                {
                    if (CurrentToken == null)
                    {
                        break;
                    }
                    CssSelectorFilter filterSelector;
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
                typeSelector = new CssUniversalSelector();
            }
            return new CssSimpleSelectorSequence(typeSelector, filters);
        }

        private CssSelectorFilter ParseNegation()
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
        private CssSelectorFilter ParsePseudoSelector()
        {
            CssSelectorFilter selector = null;
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
                else if (CurrentToken.TokenType == CssSelectorTokenType.Ident)
                {
                    //class

                    if (CurrentToken.Text == "root")
                    {
                        selector = new CssPseudoRootSelector();
                    }
                    else if (CurrentToken.Text == "first-child")
                    {
                        selector = new CssPseudoFirstChildSelector();
                    }
                    else if (CurrentToken.Text == "last-child")
                    {
                        selector = new CssPseudoLastChildSelector();
                    }
                    else if (CurrentToken.Text == "first-of-type")
                    {
                        selector = new CssPseudoFirstOfTypeSelector();
                    }
                    else if (CurrentToken.Text == "last-of-type")
                    {
                        selector = new CssPseudoLastOfTypeSelector();
                    }
                    else if (CurrentToken.Text == "only-child")
                    {
                        selector = new CssPseudoOnlyChildSelector();
                    }
                    else if (CurrentToken.Text == "only-of-type")
                    {
                        selector = new CssPseudoOnlyOfTypeSelector();
                    }
                    else if (CurrentToken.Text == "empty")
                    {
                        selector = new CssPseudoEmptySelector();
                    }
                    else if (CurrentToken.Text == "enabled")
                    {
                        selector = new CssPseudoEnabledSelector();
                    }
                    else if (CurrentToken.Text == "disabled")
                    {
                        selector = new CssPseudoDisabledSelector();
                    }
                    else if (CurrentToken.Text == "checked")
                    {
                        selector = new CssPseudoCheckedSelector();
                    }
                    else
                    {
                        //parse error
                        throw new Exception("Unsupported pseudo selector");
                    }

                    currentPosition++;
                }
                else if (CurrentToken.TokenType == CssSelectorTokenType.Function)
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
                }
            }
            return selector;
        }

        private CssSelectorFilter ParseAttributeSelector()
        {
            CssSelectorFilter selector = null;

            if (CurrentToken.Text == "[")
            {
                currentPosition++;
                SkipWhiteSpace();
                CssSelectorNamespacePrefix ns = ParseCssNamespacePrefix();
                string attributeType = null;
                if (CurrentToken.TokenType == CssSelectorTokenType.Ident)
                {
                    attributeType = CurrentToken.Text;
                    currentPosition++;
                }
                else
                {
                    // parse error
                }
                SkipWhiteSpace();
                if (CurrentToken.TokenType == CssSelectorTokenType.PrefixMatch)
                {
                    currentPosition++;
                    SkipWhiteSpace();
                    if (CurrentToken.TokenType == CssSelectorTokenType.Ident)
                    {
                        selector = new CssAttributePrefixMatchSelector(attributeType, CurrentToken.Text);
                    }
                    else if (CurrentToken.TokenType == CssSelectorTokenType.String)
                    {
                        selector = new CssAttributePrefixMatchSelector(attributeType, CurrentToken.Text);
                    }
                    else
                    {
                        //parse error lol
                    }
                    currentPosition++;
                    SkipWhiteSpace();
                }
                else if (CurrentToken.TokenType == CssSelectorTokenType.SuffixMatch)
                {
                    currentPosition++;
                    SkipWhiteSpace();
                    if (CurrentToken.TokenType == CssSelectorTokenType.Ident)
                    {
                        selector = new CssAttributeSuffixMatchSelector(attributeType, CurrentToken.Text);
                    }
                    else if (CurrentToken.TokenType == CssSelectorTokenType.String)
                    {
                        selector = new CssAttributeSuffixMatchSelector(attributeType, CurrentToken.Text);
                    }
                    else
                    {
                        //parse error lol
                    }
                    currentPosition++;
                    SkipWhiteSpace();
                }
                else if (CurrentToken.TokenType == CssSelectorTokenType.SubstringMatch)
                {
                    currentPosition++;
                    SkipWhiteSpace();
                    if (CurrentToken.TokenType == CssSelectorTokenType.Ident)
                    {
                        selector = new CssAttributeSubstringMatchSelector(attributeType, CurrentToken.Text);
                    }
                    else if (CurrentToken.TokenType == CssSelectorTokenType.String)
                    {
                        selector = new CssAttributeSubstringMatchSelector(attributeType, CurrentToken.Text);
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
                    if (CurrentToken.TokenType == CssSelectorTokenType.Ident)
                    {
                        selector = new CssAttributeExactMatchSelector(attributeType, CurrentToken.Text);
                    }
                    else if (CurrentToken.TokenType == CssSelectorTokenType.String)
                    {
                        selector = new CssAttributeExactMatchSelector(attributeType, CurrentToken.Text);
                    }
                    else
                    {
                        //parse error lol
                    }
                    currentPosition++;
                    SkipWhiteSpace();
                }
                else if (CurrentToken.TokenType == CssSelectorTokenType.Includes)
                {
                    currentPosition++;
                    SkipWhiteSpace();
                    if (CurrentToken.TokenType == CssSelectorTokenType.Ident)
                    {
                        selector = new CssAttributeIncludesSelector(attributeType, CurrentToken.Text);
                    }
                    else if (CurrentToken.TokenType == CssSelectorTokenType.String)
                    {
                        selector = new CssAttributeIncludesSelector(attributeType, CurrentToken.Text);
                    }
                    else
                    {
                        //parse error lol
                    }
                    currentPosition++;
                    SkipWhiteSpace();
                }
                else if (CurrentToken.TokenType == CssSelectorTokenType.DashMatch)
                {
                    currentPosition++;
                    SkipWhiteSpace();
                    if (CurrentToken.TokenType == CssSelectorTokenType.Ident)
                    {
                        selector = new CssAttributeDashMatchSelector(attributeType, CurrentToken.Text);
                    }
                    else if (CurrentToken.TokenType == CssSelectorTokenType.String)
                    {
                        selector = new CssAttributeDashMatchSelector(attributeType, CurrentToken.Text);
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
                    selector = new CssAttributeSelector(attributeType);
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
            if (CurrentToken.TokenType == CssSelectorTokenType.WhiteSpace)
            {
                currentPosition++;
            }
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
                    currentPosition++;
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
                    currentPosition++;
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
