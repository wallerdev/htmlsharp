using System;

namespace HtmlSharp.Elements.Tags
{
    public class Head : Tag, IAllowsNesting
    {
        public Type[] NestingBreakers { get { return new Type[0]; } }

        public string Dir { get { return this["dir"]; } }

        public string Lang { get { return this["lang"]; } }

        public string Profile { get { return this["profile"]; } }

        public Head()
            : this(new Element[0])
        {
        }

        public Head(params Element[] children)
            : base(children)
        {
            TagName = "head";
        }
    }
}