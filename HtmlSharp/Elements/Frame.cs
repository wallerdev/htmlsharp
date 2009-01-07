using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSharp.Elements
{
    public class Frame : Tag
    {
        public Frame()
        {
            Name = "frame";
            IsSelfClosing = true;
        }
    }
}
