using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSharp
{
    public class TagAttribute
    {
        public string Name { get; private set; }
        public string Value { get; private set; }

        public TagAttribute(string name, string vaue)
        {
            Name = name;
            Value = Value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                TagAttribute t = (TagAttribute)obj;
                return Name == t.Name && Value == t.Value;
            }
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Value.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0}=\"{1}\"", Name, Value);
        }
    }
}
