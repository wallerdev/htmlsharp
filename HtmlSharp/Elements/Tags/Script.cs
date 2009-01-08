using System;

namespace HtmlSharp.Elements.Tags
{
    class Script : Tag, IAllowsNesting
    {
        public Type[] NestingBreakers { get { return new Type[] {  }; } }

        public string Charset {  get { return this["charset"]; } }

        public string Defer {  get { return this["defer"]; } }

        public string Language {  get { return this["language"]; } }

        public string Src {  get { return this["src"]; } }

        public string Type {  get { return this["type"]; } }

        public Script()
        {
            
        }
    }
}