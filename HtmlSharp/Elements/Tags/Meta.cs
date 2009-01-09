using System;

namespace HtmlSharp.Elements.Tags
{
    public class Meta : Tag
    {
        public string Content { get { return this["content"]; } }

        public string Dir { get { return this["dir"]; } }

        public string Httpequiv { get { return this["http-equiv"]; } }

        public string Lang { get { return this["lang"]; } }

        public string Name { get { return this["name"]; } }

        public string Scheme { get { return this["scheme"]; } }

        public Meta()
            : this(new Element[0])
        {
        }

        public Meta(params Element[] children)
            : base(children)
        {
            TagName = "meta";
        }
    }
}