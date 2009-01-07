using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSharp.Elements
{
    public class Base : Tag
    {
        public Base()
        {
            Name = "base";
            IsSelfClosing = true;
        }
    }
}
