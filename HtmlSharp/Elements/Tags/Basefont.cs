using System;

namespace HtmlSharp.Elements.Tags
{
    public class BaseFont : Tag
    {
        public string Color { get { return this["color"]; } }

        public string Face { get { return this["face"]; } }

        public string Id { get { return this["id"]; } }

        public string Size { get { return this["size"]; } }

        public BaseFont()
            : this(new Element[0])
        {
        }

        public BaseFont(params Element[] children)
            : base(children)
        {
            TagName = "basefont";
        }
    }
}