using System;

namespace HtmlSharp.Elements.Tags
{
    class Basefont : Tag
    {
        public string Color {  get { return this["color"]; } }

        public string Face {  get { return this["face"]; } }

        public string Id {  get { return this["id"]; } }

        public string Size {  get { return this["size"]; } }

        public Basefont()
        {
            
        }
    }
}