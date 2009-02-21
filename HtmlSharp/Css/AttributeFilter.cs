using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSharp.Css
{
    public class AttributeFilter : SelectorFilter
    {
        string type;

        public AttributeFilter(string type)
        {
            this.type = type;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                AttributeFilter t = (AttributeFilter)obj;
                return type.Equals(t.type);
            }
        }

        public override int GetHashCode()
        {
            return type.GetHashCode();
        }
    }
}
