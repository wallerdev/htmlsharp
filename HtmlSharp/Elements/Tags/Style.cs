using System;
using System.Collections.Generic;

namespace HtmlSharp.Elements.Tags
{
    public class Style : Tag, IAllowsNesting
    {
        public IEnumerable<Type> NestingBreakers { get { return new Type[0]; } }

        public string Dir { get { return this["dir"]; } }

        public string Lang { get { return this["lang"]; } }

        public string Media { get { return this["media"]; } }

        public string Title { get { return this["title"]; } }

        public string Type { get { return this["type"]; } }

        public Style()
            : this(new Element[0])
        {
        }

        public Style(params Element[] children)
            : base(children)
        {
            TagName = "style";
        }
    }
}