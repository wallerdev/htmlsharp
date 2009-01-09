using System;

namespace HtmlSharp.Elements.Tags
{
    public class Basefont : Tag
    {
        public string Color { get { return this["color"]; } }

        public string Face { get { return this["face"]; } }

        public string Id { get { return this["id"]; } }

        public string Size { get { return this["size"]; } }

        public Basefont()
            : this(new Element[0])
        {
        }

        public Basefont(params Element[] children)
            : base(children)
        {
            TagName = "basefont";
        }
    }
}