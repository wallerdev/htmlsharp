using System;
using System.Collections.Generic;

namespace HtmlSharp.Elements.Tags
{
    public class Title : Tag, IAllowsNesting
    {
        public IEnumerable<Type> NestingBreakers { get { return new Type[0]; } }

        public string Dir { get { return this["dir"]; } }

        public string Lang { get { return this["lang"]; } }

        public Title()
            : this(new Element[0])
        {
        }

        public Title(params Element[] children)
            : base(children)
        {
            TagName = "title";
        }
    }
}