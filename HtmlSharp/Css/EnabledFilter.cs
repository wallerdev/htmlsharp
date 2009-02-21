using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSharp.Css
{
    public class EnabledFilter : SelectorFilter
    {
        public override bool Equals(object obj)
        {
            return obj != null && GetType() == obj.GetType();
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode();
        }
    }
}
