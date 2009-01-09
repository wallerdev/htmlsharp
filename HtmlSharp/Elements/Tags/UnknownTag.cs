using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSharp.Elements.Tags
{
    public class UnknownTag : Tag
    {
        public UnknownTag(string name)
            : this(name, new Element[0])
        {
        }

        public UnknownTag(string name, params Element[] children)
            : base(children)
        {
            TagName = name;
        }
    }
}
