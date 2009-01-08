using System;

namespace HtmlSharp.Elements.Tags
{
    class Frame : Tag
    {
        public string Class {  get { return this["class"]; } }

        public string Frameborder {  get { return this["frameborder"]; } }

        public string Id {  get { return this["id"]; } }

        public string Longdesc {  get { return this["longdesc"]; } }

        public string Marginheight {  get { return this["marginheight"]; } }

        public string Marginwidth {  get { return this["marginwidth"]; } }

        public string Name {  get { return this["name"]; } }

        public string Noresize {  get { return this["noresize"]; } }

        public string Scrolling {  get { return this["scrolling"]; } }

        public string Src {  get { return this["src"]; } }

        public string Style {  get { return this["style"]; } }

        public string Title {  get { return this["title"]; } }

        public Frame()
        {
            
        }
    }
}