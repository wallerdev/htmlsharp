using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSharp.Css
{
    public class NthLastOfTypeFilter : SelectorFilter
    {
        Expression expression;

        public NthLastOfTypeFilter(Expression expression)
        {
            this.expression = expression;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                NthLastOfTypeFilter t = (NthLastOfTypeFilter)obj;
                return expression.Equals(t.expression);
            }
        }

        public override int GetHashCode()
        {
            return expression.GetHashCode();
        }
    }
}
