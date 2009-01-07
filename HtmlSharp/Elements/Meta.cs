using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSharp.Elements
{
    public class Meta : Tag
    {
        public Meta()
        {
            Name = "meta";
            IsSelfClosing = true;
        }
    }
}
