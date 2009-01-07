using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSharp.Elements
{
    public class Div : Tag, INestableTag
    {
        public Type[] NestingBreakers
        {
            get { return new Type[0]; }
        }

        public Div()
        {
            Name = "Div";
        }
    }
}
