using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlSharp.Elements;

namespace HtmlSharp.Css
{
    public class GeneralSiblingCombinator : Combinator
    {
        public override string ToString()
        {
            return "~";
        }

        public override IEnumerable<Tag> Apply(IEnumerable<Tag> tags)
        {
            throw new NotImplementedException();
        }
    }
}
