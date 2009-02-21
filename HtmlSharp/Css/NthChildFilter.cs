using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSharp.Css
{
    public class NthChildFilter : SelectorFilter
    {
        Expression expression;

        public NthChildFilter(Expression expression)
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
                NthChildFilter t = (NthChildFilter)obj;
                return expression.Equals(t.expression);
            }
        }

        public override int GetHashCode()
        {
            return expression.GetHashCode();
        }
    }
}
