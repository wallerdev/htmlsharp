using System;

namespace HtmlSharp.Elements.Tags
{
    public class Hr : Tag
    {
        public string Align { get { return this["align"]; } }

        public string Class { get { return this["class"]; } }

        public string Dir { get { return this["dir"]; } }

        public string Id { get { return this["id"]; } }

        public string Lang { get { return this["lang"]; } }

        public string Noshade { get { return this["noshade"]; } }

        public string Onclick { get { return this["onclick"]; } }

        public string Ondblclick { get { return this["ondblclick"]; } }

        public string Onkeydown { get { return this["onkeydown"]; } }

        public string Onkeypress { get { return this["onkeypress"]; } }

        public string Onkeyup { get { return this["onkeyup"]; } }

        public string Onmousedown { get { return this["onmousedown"]; } }

        public string Onmousemove { get { return this["onmousemove"]; } }

        public string Onmouseout { get { return this["onmouseout"]; } }

        public string Onmouseover { get { return this["onmouseover"]; } }

        public string Onmouseup { get { return this["onmouseup"]; } }

        public string Size { get { return this["size"]; } }

        public string Style { get { return this["style"]; } }

        public string Title { get { return this["title"]; } }

        public string Width { get { return this["width"]; } }

        public Hr()
            : this(new Element[0])
        {
        }

        public Hr(params Element[] children)
            : base(children)
        {
            TagName = "hr";
        }
    }
}