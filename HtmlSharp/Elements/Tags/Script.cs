using System;
using System.Collections.Generic;

namespace HtmlSharp.Elements.Tags
{
    public class Script : Tag, IAllowsNesting
    {
        public IEnumerable<Type> NestingBreakers { get { return new Type[0]; } }

        public string Charset { get { return this["charset"]; } }

        public string Defer { get { return this["defer"]; } }

        public string Language { get { return this["language"]; } }

        public string Src { get { return this["src"]; } }

        public string Type { get { return this["type"]; } }

        public Script()
            : this(new Element[0])
        {
        }

        public Script(params Element[] children)
            : base(children)
        {
            TagName = "script";
        }
    }
}