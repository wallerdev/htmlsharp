using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSharp.Css
{
    public class AttributeSuffixFilter : AttributeFilter
    {
        string suffix;

        public AttributeSuffixFilter(string type, string suffix)
            : base(type)
        {
            this.suffix = suffix;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                AttributeSuffixFilter t = (AttributeSuffixFilter)obj;
                return suffix.Equals(t.suffix) && base.Equals(obj);
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ suffix.GetHashCode();
        }
    }
}
