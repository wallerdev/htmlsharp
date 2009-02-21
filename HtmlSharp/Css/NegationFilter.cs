using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSharp.Css
{
    public class NegationFilter : SelectorFilter
    {
        string negation;

        public NegationFilter(string negation)
        {
            this.negation = negation;
        }
    }
}
