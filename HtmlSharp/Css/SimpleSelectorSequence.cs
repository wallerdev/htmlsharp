using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSharp.Css
{
    public class SimpleSelectorSequence
    {
        IEnumerable<SelectorFilter> filters;
        TypeSelector selector;

        public SimpleSelectorSequence(TypeSelector selector, IEnumerable<SelectorFilter> filters)
        {
            this.selector = selector;
            this.filters = filters;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                SimpleSelectorSequence t = (SimpleSelectorSequence)obj;
                return selector.Equals(t.selector) && filters.SequenceEqual(t.filters);
            }
        }

        public override int GetHashCode()
        {
            return selector.GetHashCode() ^ filters.Aggregate(0, (a, b) => a ^= b.GetHashCode());
        }
    }
}
