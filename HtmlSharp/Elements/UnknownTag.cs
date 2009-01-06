using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSharp.Elements
{
    class UnknownTag : Tag
    {
        string name;

        public override string Name
        {
            get { return name; }
        }
        
        public UnknownTag(string name)
        {
            this.name = name;
        }
    }
}
