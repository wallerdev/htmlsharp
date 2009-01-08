using System;

namespace HtmlSharp.Elements.Tags
{
    class Body : Tag, IAllowsNesting
    {
        public Type[] NestingBreakers { get { return new Type[] {  }; } }

        public string Alink {  get { return this["alink"]; } }

        public string Background {  get { return this["background"]; } }

        public string Bgcolor {  get { return this["bgcolor"]; } }

        public string Class {  get { return this["class"]; } }

        public string Dir {  get { return this["dir"]; } }

        public string Id {  get { return this["id"]; } }

        public string Lang {  get { return this["lang"]; } }

        public string Link {  get { return this["link"]; } }

        public string Onclick {  get { return this["onclick"]; } }

        public string Ondblclick {  get { return this["ondblclick"]; } }

        public string Onkeydown {  get { return this["onkeydown"]; } }

        public string Onkeypress {  get { return this["onkeypress"]; } }

        public string Onkeyup {  get { return this["onkeyup"]; } }

        public string Onload {  get { return this["onload"]; } }

        public string Onmousedown {  get { return this["onmousedown"]; } }

        public string Onmousemove {  get { return this["onmousemove"]; } }

        public string Onmouseout {  get { return this["onmouseout"]; } }

        public string Onmouseover {  get { return this["onmouseover"]; } }

        public string Onmouseup {  get { return this["onmouseup"]; } }

        public string Onunload {  get { return this["onunload"]; } }

        public string Style {  get { return this["style"]; } }

        public string Text {  get { return this["text"]; } }

        public string Title {  get { return this["title"]; } }

        public string Vlink {  get { return this["vlink"]; } }

        public Body()
        {
            
        }
    }
}