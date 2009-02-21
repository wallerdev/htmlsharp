using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSharp.Css
{
    public class ChildCombinator : Combinator
    {
        public override string ToString()
        {
            return ">";
        }
    }
}
