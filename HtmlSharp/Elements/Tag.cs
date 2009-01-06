using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSharp.Elements
{
    public class Tag : Element
    {
        public bool Hidden { get; set; }
        public List<KeyValuePair<string, string>> Attributes { get; protected set; }

        public Tag(string name)
        {
            Children = new List<Element>();
            Name = name;
            Attributes = new List<KeyValuePair<string, string>>();
        }

        public Tag(string name, List<KeyValuePair<string, string>> attributes, Element parent, Element previous)
            : this(name)
        {
            Attributes = attributes;
            Parent = parent;
            Previous = previous;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            string atts = string.Join(" ",
                Attributes.Select(a => string.Format("{0}=\"{1}\"", a.Key, a.Value))
                          .ToArray());

            if (atts.Length > 0)
            {
                builder.AppendFormat("<{0} {1}>", Name, atts);
            }
            else
            {
                builder.AppendFormat("<{0}>", Name);
            }
            foreach (Element ele in Children)
            {
                builder.Append(ele.ToString());
            }
            builder.AppendFormat("</{0}>", Name);
            return builder.ToString();
        }

    }
}
