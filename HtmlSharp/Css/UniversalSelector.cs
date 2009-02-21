using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSharp.Css
{
    public class UniversalSelector : TypeSelector
    {
        public UniversalSelector()
            : base("*")
        {

        }

        public UniversalSelector(SelectorNamespacePrefix prefix)
            : base("*", prefix)
        {

        }

        public override string ToString()
        {
            return "*";
        }
    }
}
