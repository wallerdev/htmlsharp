using System;

namespace HtmlSharp.Elements.Tags
{
    public class Iframe : Tag, IAllowsNesting
    {
        public Type[] NestingBreakers { get { return new Type[0]; } }

        public string Align { get { return this["align"]; } }

        public string Class { get { return this["class"]; } }

        public string Frameborder { get { return this["frameborder"]; } }

        public string Height { get { return this["height"]; } }

        public string Id { get { return this["id"]; } }

        public string Longdesc { get { return this["longdesc"]; } }

        public string Marginheight { get { return this["marginheight"]; } }

        public string Marginwidth { get { return this["marginwidth"]; } }

        public string Name { get { return this["name"]; } }

        public string Scrolling { get { return this["scrolling"]; } }

        public string Src { get { return this["src"]; } }

        public string Style { get { return this["style"]; } }

        public string Title { get { return this["title"]; } }

        public string Width { get { return this["width"]; } }

        public Iframe()
            : this(new Element[0])
        {
        }

        public Iframe(params Element[] children)
            : base(children)
        {
            TagName = "iframe";
        }
    }
}