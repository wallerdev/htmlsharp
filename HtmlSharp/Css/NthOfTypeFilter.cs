using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSharp.Css
{
    public class NthOfTypeFilter : SelectorFilter
    {
        Expression expression;

        public NthOfTypeFilter(Expression expression)
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
                NthOfTypeFilter t = (NthOfTypeFilter)obj;
                return expression.Equals(t.expression);
            }
        }

        public override int GetHashCode()
        {
            return expression.GetHashCode();
        }
    }
}
