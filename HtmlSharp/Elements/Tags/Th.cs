using System;

namespace HtmlSharp.Elements.Tags
{
    class Th : Tag, IAllowsNesting
    {
        public Type[] NestingBreakers { get { return new Type[] { typeof(Tr) }; } }

        public string Abbr {  get { return this["abbr"]; } }

        public string Align {  get { return this["align"]; } }

        public string Axis {  get { return this["axis"]; } }

        public string Bgcolor {  get { return this["bgcolor"]; } }

        public string Char {  get { return this["char"]; } }

        public string Charoff {  get { return this["charoff"]; } }

        public string Class {  get { return this["class"]; } }

        public string Colspan {  get { return this["colspan"]; } }

        public string Dir {  get { return this["dir"]; } }

        public string Headers {  get { return this["headers"]; } }

        public string Height {  get { return this["height"]; } }

        public string Id {  get { return this["id"]; } }

        public string Lang {  get { return this["lang"]; } }

        public string Nowrap {  get { return this["nowrap"]; } }

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

        public string Rowspan {  get { return this["rowspan"]; } }

        public string Scope {  get { return this["scope"]; } }

        public string Style {  get { return this["style"]; } }

        public string Title {  get { return this["title"]; } }

        public string Valign {  get { return this["valign"]; } }

        public string Width {  get { return this["width"]; } }

        public Th()
        {
            ResetsNesting = true;
            
        }
    }
}