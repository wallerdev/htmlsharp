using System;

namespace HtmlSharp.Elements.Tags
{
    class Br : Tag
    {
        public string Class {  get { return this["class"]; } }

        public string Clear {  get { return this["clear"]; } }

        public string Id {  get { return this["id"]; } }

        public string Style {  get { return this["style"]; } }

        public string Title {  get { return this["title"]; } }

        public Br()
        {
            
        }
    }
}