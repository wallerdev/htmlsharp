using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSharp.Css
{
    public class ClassFilter : SelectorFilter
    {
        string klass;

        public ClassFilter(string klass)
        {
            this.klass = klass;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                ClassFilter t = (ClassFilter)obj;
                return klass.Equals(t.klass);
            }
        }

        public override int GetHashCode()
        {
            return klass.GetHashCode();
        }
    }
}
