using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSharp.Elements
{
    class P : Tag
    {
        public override string Name { get { return "p"; } }
        public override bool IsSelfClosing { get { return false; } }
        public override bool IsNestable { get { return false; } }
    }
}
