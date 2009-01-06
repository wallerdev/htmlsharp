using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSharp.Elements
{
    public class Text : Element
    {
        public string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}
