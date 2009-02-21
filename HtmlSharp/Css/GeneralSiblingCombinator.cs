using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSharp.Css
{
    public class GeneralSiblingCombinator : Combinator
    {
        public override string ToString()
        {
            return "~";
        }
    }
}
