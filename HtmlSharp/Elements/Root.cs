using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSharp.Elements
{
    class Root : Tag
    {
        public Root()
        {
            Hidden = true;
            Name = "[document]";
        }
    }
}
