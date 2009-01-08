using System;

namespace HtmlSharp.Elements.Tags
{
    class Title : Tag, IAllowsNesting
    {
        public Type[] NestingBreakers { get { return new Type[] {  }; } }

        public string Dir {  get { return this["dir"]; } }

        public string Lang {  get { return this["lang"]; } }

        public Title()
        {
            
        }
    }
}