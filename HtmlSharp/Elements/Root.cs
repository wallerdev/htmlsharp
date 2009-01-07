using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSharp.Elements
{
    class Root : Tag
    {
        public override string Name { get { return "[document]"; } }
        public override bool IsSelfClosing { get { return false; } }
        public override bool IsNestable { get { return false; } }

        public Root()
        {
            Hidden = true;
        }
    }
}
