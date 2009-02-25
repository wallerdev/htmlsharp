using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using HtmlSharp.Elements;
using HtmlSharp.Css;

namespace HtmlSharp
{
    public class Document
    {
        public string Html { get; private set; }
        public Tag Root { get; private set; }

        public IEnumerable<Tag> GetTags()
        {
            Element currentTag = Root.Children.ElementAtOrDefault(0);
            while (currentTag != null)
            {
                Tag current = currentTag as Tag;
                if (current != null)
                {
                    yield return current;
                }
                currentTag = currentTag.Next;
            }
        }

        public Document(string html, Tag root)
        {
            Html = html;
            Root = root;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (Element element in Root.Children)
            {
                builder.Append(element.ToString());
            }
            return builder.ToString();
        }

        public IEnumerable<Tag> FindAll()
        {
            foreach (Tag t in Root.FindAll())
            {
                yield return t;
            }
        }

        public Tag Find(string selector)
        {
            SelectorParser parser = new SelectorParser();
            var selectorGroup = parser.Parse(selector);
            return selectorGroup.Apply(GetTags()).FirstOrDefault();
        }
    }
}
