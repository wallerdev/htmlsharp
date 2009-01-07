using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSharp.Elements
{
    public class Hr : Tag
    {
        public Hr()
        {
            Name = "hr";
            IsSelfClosing = true;
        }
    }
}
