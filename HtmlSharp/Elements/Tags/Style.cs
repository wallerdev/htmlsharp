using System;

namespace HtmlSharp.Elements.Tags
{
    class Style : Tag, IAllowsNesting
    {
        public Type[] NestingBreakers { get { return new Type[] {  }; } }

        public string Dir {  get { return this["dir"]; } }

        public string Lang {  get { return this["lang"]; } }

        public string Media {  get { return this["media"]; } }

        public string Title {  get { return this["title"]; } }

        public string Type {  get { return this["type"]; } }

        public Style()
        {
            
        }
    }
}