using System;

namespace HtmlSharp.Elements.Tags
{
    public class Applet : Tag, IAllowsNesting
    {
        public Type[] NestingBreakers { get { return new Type[0]; } }

        public string Align { get { return this["align"]; } }

        public string Alt { get { return this["alt"]; } }

        public string Archive { get { return this["archive"]; } }

        public string Class { get { return this["class"]; } }

        public string Code { get { return this["code"]; } }

        public string Codebase { get { return this["codebase"]; } }

        public string Dir { get { return this["dir"]; } }

        public string Height { get { return this["height"]; } }

        public string Hspace { get { return this["hspace"]; } }

        public string Id { get { return this["id"]; } }

        public string Lang { get { return this["lang"]; } }

        public string Name { get { return this["name"]; } }

        public string Object { get { return this["object"]; } }

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

        public string Style { get { return this["style"]; } }

        public string Title { get { return this["title"]; } }

        public string Vspace { get { return this["vspace"]; } }

        public string Width { get { return this["width"]; } }

        public Applet()
            : this(new Element[0])
        {
        }

        public Applet(params Element[] children)
            : base(children)
        {
            TagName = "applet";
        }
    }
}