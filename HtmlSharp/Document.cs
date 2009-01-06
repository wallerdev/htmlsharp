using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace HtmlSharp
{
    public class Document
    {
        public string Html { get; private set; }
        public Element Root { get; private set; }

        public Document(string html, Element root)
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
    }
}
