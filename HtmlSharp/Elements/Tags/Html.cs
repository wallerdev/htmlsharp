using System;

namespace HtmlSharp.Elements.Tags
{
    class Html : Tag, IAllowsNesting
    {
        public Type[] NestingBreakers { get { return new Type[] {  }; } }

        public string Dir {  get { return this["dir"]; } }

        public string Lang {  get { return this["lang"]; } }

        public string Version {  get { return this["version"]; } }

        public Html()
        {
            
        }
    }
}