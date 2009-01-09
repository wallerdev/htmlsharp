using System;

namespace HtmlSharp.Elements.Tags
{
    public class Font : Tag, IAllowsNesting
    {
        public Type[] NestingBreakers { get { return new Type[0]; } }

        public string Class { get { return this["class"]; } }

        public string Color { get { return this["color"]; } }

        public string Dir { get { return this["dir"]; } }

        public string Face { get { return this["face"]; } }

        public string Id { get { return this["id"]; } }

        public string Lang { get { return this["lang"]; } }

        public string Size { get { return this["size"]; } }

        public string Style { get { return this["style"]; } }

        public string Title { get { return this["title"]; } }

        public Font()
            : this(new Element[0])
        {
        }

        public Font(params Element[] children)
            : base(children)
        {
            TagName = "font";
        }
    }
}