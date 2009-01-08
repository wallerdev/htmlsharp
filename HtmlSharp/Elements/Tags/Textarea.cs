using System;

namespace HtmlSharp.Elements.Tags
{
    class Textarea : Tag, IAllowsNesting
    {
        public Type[] NestingBreakers { get { return new Type[] {  }; } }

        public string Accesskey {  get { return this["accesskey"]; } }

        public string Class {  get { return this["class"]; } }

        public string Cols {  get { return this["cols"]; } }

        public string Dir {  get { return this["dir"]; } }

        public string Disabled {  get { return this["disabled"]; } }

        public string Id {  get { return this["id"]; } }

        public string Lang {  get { return this["lang"]; } }

        public string Name {  get { return this["name"]; } }

        public string Onblur {  get { return this["onblur"]; } }

        public string Onchange {  get { return this["onchange"]; } }

        public string Onclick {  get { return this["onclick"]; } }

        public string Ondblclick {  get { return this["ondblclick"]; } }

        public string Onfocus {  get { return this["onfocus"]; } }

        public string Onkeydown {  get { return this["onkeydown"]; } }

        public string Onkeypress {  get { return this["onkeypress"]; } }

        public string Onkeyup {  get { return this["onkeyup"]; } }

        public string Onmousedown {  get { return this["onmousedown"]; } }

        public string Onmousemove {  get { return this["onmousemove"]; } }

        public string Onmouseout {  get { return this["onmouseout"]; } }

        public string Onmouseover {  get { return this["onmouseover"]; } }

        public string Onmouseup {  get { return this["onmouseup"]; } }

        public string Onselect {  get { return this["onselect"]; } }

        public string Readonly {  get { return this["readonly"]; } }

        public string Rows {  get { return this["rows"]; } }

        public string Style {  get { return this["style"]; } }

        public string Tabindex {  get { return this["tabindex"]; } }

        public string Title {  get { return this["title"]; } }

        public Textarea()
        {
            
        }
    }
}