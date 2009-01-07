using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSharp.Elements
{
    class UnknownTag : Tag
    {
        public UnknownTag(string name)
        {
            Name = name;
        }
    }
}
