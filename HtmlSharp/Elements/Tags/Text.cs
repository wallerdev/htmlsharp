using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlSharp.Extensions;

namespace HtmlSharp.Elements
{
    public class Text : Element
    {
        string _value;
        public string Value
        {
            get { return _value.HtmlDecode(); }
            set { _value = value; }
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
