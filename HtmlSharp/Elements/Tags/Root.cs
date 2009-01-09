using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSharp.Elements.Tags
{
    public class Root : Tag, IAllowsNesting
    {
        public Type[] NestingBreakers { get { return new Type[0]; } }

        public Root()
            : this(new Element[0])
        {
        }

        public Root(params Element[] children)
            : base(children)
        {
            Hidden = true;
            TagName = "[document]";
        }
    }
}
