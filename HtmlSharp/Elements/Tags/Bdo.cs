using System;

namespace HtmlSharp.Elements.Tags
{
    public class Bdo : Tag, IAllowsNesting
    {
        public Type[] NestingBreakers { get { return new Type[0]; } }

        public string Class { get { return this["class"]; } }

        public string Dir { get { return this["dir"]; } }

        public string Id { get { return this["id"]; } }

        public string Lang { get { return this["lang"]; } }

        public string Style { get { return this["style"]; } }

        public string Title { get { return this["title"]; } }

        public Bdo()
            : this(new Element[0])
        {
        }

        public Bdo(params Element[] children)
            : base(children)
        {
            TagName = "bdo";
        }
    }
}