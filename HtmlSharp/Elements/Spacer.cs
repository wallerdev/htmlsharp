using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSharp.Elements
{
    public class Spacer : Tag
    {
        public Spacer()
        {
            Name = "spacer";
            IsSelfClosing = true;
        }
    }
}
