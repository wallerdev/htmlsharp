using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSharp.Css
{
    public class IDFilter : SelectorFilter
    {
        string id;

        public IDFilter(string id)
        {
            this.id = id;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                IDFilter t = (IDFilter)obj;
                return id.Equals(t.id);
            }
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }
}
