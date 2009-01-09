using System;
using System.Collections.Generic;

namespace HtmlSharp.Elements.Tags
{
    public class Html : Tag, IAllowsNesting
    {
        public IEnumerable<Type> NestingBreakers { get { return new Type[0]; } }

        public string Dir { get { return this["dir"]; } }

        public string Lang { get { return this["lang"]; } }

        public string Version { get { return this["version"]; } }

        public Html()
            : this(new Element[0])
        {
        }

        public Html(params Element[] children)
            : base(children)
        {
            TagName = "html";
        }
    }
}