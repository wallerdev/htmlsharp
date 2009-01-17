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
        public string Name { get; private set; }

        public CssTypeSelector(string name)
        {
            this.Name = name;
        }

        public CssTypeSelector(Tag tag)
        {
            Name = tag.TagName;
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

    public enum CssSelectorTokenType
    {
        Invalid,
        TypeSelector,
        UniversalSelector,
        AttributeStart,
        AttributeEnd,
        AttributeName,
        AttributeMatch,
        AttributePrefixMatch,
        AttributeSuffixMatch,
        AttributeSubstringMatch,
        AttributeIncludesMatch,
        AttributeDashMatch,
        AttributeValue,
        WhiteSpace
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
        int currentPosition = 0;
        string input;
        List<CssSelectorToken> tokens = new List<CssSelectorToken>();

        CssSelectorTokenType LastTokenType
        {
            get
            {
                if (tokens.Count > 0)
                {
                    return tokens[tokens.Count - 1].TokenType;
                }
                return CssSelectorTokenType.Invalid;
            }
        }

        public CssSelectorTokenizer()
        {
        }

        public IEnumerable<CssSelectorToken> Tokenize(string input)
        {
            this.input = input;

            SkipWhiteSpace();
            while (currentPosition < input.Length)
            {
                if (char.IsLetter(input, currentPosition))
                {
                    yield return new CssSelectorToken(CssSelectorTokenType.TypeSelector, ConsumeTypeToken());
                }
                else if (input[currentPosition] == '*')
                {
                    yield return new CssSelectorToken(CssSelectorTokenType.UniversalSelector, ConsumeChar());
                }
                else if (input[currentPosition] == '[')
                {
                    yield return new CssSelectorToken(CssSelectorTokenType.AttributeStart, ConsumeChar());
                    SkipWhiteSpace();

                    yield return new CssSelectorToken(CssSelectorTokenType.AttributeName, ConsumeAttributeName());
                    SkipWhiteSpace();

                    if (input[currentPosition] == ']')
                    {
                        yield return new CssSelectorToken(CssSelectorTokenType.AttributeEnd, ConsumeChar());
                    }
                    else
                    {
                        if (input.Substring(currentPosition, 2) == "^=")
                        {
                            yield return new CssSelectorToken(CssSelectorTokenType.AttributePrefixMatch, ConsumePrefixMatch());
                        }
                        else if (input.Substring(currentPosition, 2) == "$=")
                        {
                            yield return new CssSelectorToken(CssSelectorTokenType.AttributeSuffixMatch, ConsumeSuffixMatch());
                        }
                        else if (input.Substring(currentPosition, 2) == "*=")
                        {
                            yield return new CssSelectorToken(CssSelectorTokenType.AttributeSubstringMatch, ConsumeSuffixMatch());
                        }
                        else if (input[currentPosition] == '=')
                        {
                            yield return new CssSelectorToken(CssSelectorTokenType.AttributeMatch, ConsumeChar());
                        }
                        else if (input.Substring(currentPosition, 2) == "~=")
                        {
                            yield return new CssSelectorToken(CssSelectorTokenType.AttributeIncludesMatch, ConsumeIncludesMatch());
                        }
                        else if (input.Substring(currentPosition, 2) == "|=")
                        {
                            yield return new CssSelectorToken(CssSelectorTokenType.AttributeDashMatch, ConsumeDashMatch());
                        }
                        else
                        {
                            UnrecognizedToken();
                        }
                        SkipWhiteSpace();

                        if (input[currentPosition] == '"' || input[currentPosition] == '\'')
                        {
                            yield return new CssSelectorToken(CssSelectorTokenType.AttributeValue, ConsumeAttributeStringValue());
                        }
                        else if (char.IsLetter(input, currentPosition))
                        {
                            yield return new CssSelectorToken(CssSelectorTokenType.AttributeValue, ConsumeAttributeValue());
                        }
                        else
                        {
                            UnrecognizedToken();
                        }

                        if (input[currentPosition] == ']')
                        {
                            yield return new CssSelectorToken(CssSelectorTokenType.AttributeEnd, ConsumeChar());
                        }
                        else
                        {
                            UnrecognizedToken();
                        }
                    }
                }
                else
                {
                    UnrecognizedToken();
                }
                if (currentPosition < input.Length && char.IsWhiteSpace(input, currentPosition))
                {
                    yield return new CssSelectorToken(CssSelectorTokenType.WhiteSpace, ConsumeWhiteSpace());
                }
            }
        }

        private string ConsumeWhiteSpace()
        {
            return ConsumeWhile(char.IsWhiteSpace);
        }

        private string ConsumeAttributeStringValue()
        {
            return ConsumeString();
        }

        private string ConsumeString()
        {
            char end = ConsumeChar()[0];
            StringBuilder token = new StringBuilder();
            while (currentPosition < input.Length && (input[currentPosition] != end || input[currentPosition - 1] == '\\'))
            {
                if (input[currentPosition - 1] == '\\' && input[currentPosition] == end)
                {
                    //remove backslash
                    token.Length -= 1;
                }
                token.Append(input[currentPosition]);
                currentPosition++;
            }
            currentPosition++;
            return token.ToString();
        }

        private string ConsumeAttributeValue()
        {
            return ConsumeIdent();
        }

        private string ConsumeDashMatch()
        {
            currentPosition += 2;
            return input.Substring(currentPosition - 2, 2);
        }

        private string ConsumeIncludesMatch()
        {
            currentPosition += 2;
            return input.Substring(currentPosition - 2, 2);
        }

        private string ConsumeSuffixMatch()
        {
            currentPosition += 2;
            return input.Substring(currentPosition - 2, 2);
        }

        private void UnrecognizedToken()
        {
            throw new FormatException(
                string.Format("Unrecognised token '{0}' at position {1}",
                    input[currentPosition], currentPosition + 1));
        }

        private string ConsumePrefixMatch()
        {
            currentPosition += 2;
            return input.Substring(currentPosition - 2, 2);
        }

        private string ConsumeIdent()
        {
            return ConsumeWhile(char.IsLetterOrDigit);
        }

        private string ConsumeAttributeName()
        {
            return ConsumeIdent();
        }

        private string ConsumeChar()
        {
            currentPosition++;
            return input[currentPosition - 1].ToString();
        }

        private string ConsumeTypeToken()
        {
            return ConsumeIdent();
        }

        private string ConsumeWhile(Predicate<char> condition)
        {
            StringBuilder token = new StringBuilder();
            while (currentPosition < input.Length && condition(input[currentPosition]))
            {
                token.Append(input[currentPosition]);
                currentPosition++;
            }
            return token.ToString();
        }

        private CssSelectorToken ConsumeChar(CssSelectorToken value)
        {
            currentPosition++;
            return value;
        }

        private void SkipWhiteSpace()
        {
            ConsumeWhile(char.IsWhiteSpace);
        }
    }

    public class CssSelectorParser
    {
        public CssSelector Parse(string selector)
        {
            List<CssSimpleSelector> simpleSelectors = new List<CssSimpleSelector>();
            List<CssCombinator> combinators = new List<CssCombinator>();

            selector = RemoveUncessaryWhiteSpace(selector).ToLowerInvariant();

            //int i = 0;
            //while (i < selector.Length)
            //{
            //    if (selector[i] == '*')
            //    {
            //        simpleSelectors.Add(new CssUniversalSelector());
            //    }
            //    else if (char.IsLetter(selector, i))
            //    {
            //        string tag = TakeWhile(i, selector, IsTagCharacter);
            //        simpleSelectors.Add(new CssTypeSelector(tag));
            //        i += tag.Length;
            //    }
            //    else
            //    {
            //        i++;
            //    }
            //}

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
