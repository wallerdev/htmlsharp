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
            foreach (var tag in GetAllChildTags(Root))
            {
                yield return tag;
            }
        }

        IEnumerable<Tag> GetAllChildTags(Element tag)
        {
            foreach (var child in tag.Children)
            {
                if (child is Tag)
                {
                    yield return child as Tag;
                }
                GetAllChildTags(child);
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
            foreach (Element ele in Root.Children)
            {
                builder.Append(ele.ToString());
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
            selectorGroup.Apply(GetTags());

            return null;
        }
    }
}
