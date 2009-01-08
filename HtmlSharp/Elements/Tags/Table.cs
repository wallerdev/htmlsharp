using System;

namespace HtmlSharp.Elements.Tags
{
    class Table : Tag, IAllowsNesting
    {
        public Type[] NestingBreakers { get { return new Type[] {  }; } }

        public string Align {  get { return this["align"]; } }

        public string Bgcolor {  get { return this["bgcolor"]; } }

        public string Border {  get { return this["border"]; } }

        public string Cellpadding {  get { return this["cellpadding"]; } }

        public string Cellspacing {  get { return this["cellspacing"]; } }

        public string Class {  get { return this["class"]; } }

        public string Dir {  get { return this["dir"]; } }

        public string Frame {  get { return this["frame"]; } }

        public string Id {  get { return this["id"]; } }

        public string Lang {  get { return this["lang"]; } }

        public string Onclick {  get { return this["onclick"]; } }

        public string Ondblclick {  get { return this["ondblclick"]; } }

        public string Onkeydown {  get { return this["onkeydown"]; } }

        public string Onkeypress {  get { return this["onkeypress"]; } }

        public string Onkeyup {  get { return this["onkeyup"]; } }

        public string Onmousedown {  get { return this["onmousedown"]; } }

        public string Onmousemove {  get { return this["onmousemove"]; } }

        public string Onmouseout {  get { return this["onmouseout"]; } }

        public string Onmouseover {  get { return this["onmouseover"]; } }

        public string Onmouseup {  get { return this["onmouseup"]; } }

        public string Rules {  get { return this["rules"]; } }

        public string Style {  get { return this["style"]; } }

        public string Summary {  get { return this["summary"]; } }

        public string Title {  get { return this["title"]; } }

        public string Width {  get { return this["width"]; } }

        public Table()
        {
            ResetsNesting = true;
            
        }
    }
}