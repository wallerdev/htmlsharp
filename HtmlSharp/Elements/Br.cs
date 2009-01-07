using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSharp.Elements
{
    public class Br : Tag
    {
        public Br()
        {
            Name = "br";
            IsSelfClosing = true;
        }
    }
}
