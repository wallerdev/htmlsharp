using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSharp.Elements
{
    public class Img : Tag
    {
        public Img()
        {
            Name = "img";
            IsSelfClosing = true;
        }
    }
}
