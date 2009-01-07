using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSharp.Elements
{


    public abstract class Tag : Element
    {
        public bool Hidden { get; protected set; }
        public List<TagAttribute> Attributes { get; private set; }
        public string Name { get; protected set; }
        public bool IsSelfClosing { get; protected set; }
        public bool ResetsNesting { get; protected set; }

        static Dictionary<string, Func<Tag>> tagMap = new Dictionary<string, Func<Tag>>()
        {
            {"p", () => new P()},
            {"[document]", () => new Root()},
            {"div", () => new Div()}
        };

        public static Tag Create(string name)
        {
            if (tagMap.ContainsKey(name))
            {
                return tagMap[name]();
            }
            return new UnknownTag(name);
        }

        protected Tag()
        {
            Children = new List<Element>();
            Attributes = new List<TagAttribute>();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                Tag t = (Tag)obj;
                if (Name != t.Name)
                {
                    return false;
                }
                return Attributes.SequenceEqual(t.Attributes);
            }
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Attributes.Aggregate(0, (a, b) => a ^= b.GetHashCode());
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder("<");
            builder.Append(Name);
            foreach (TagAttribute attribute in Attributes)
            {
                builder.Append(" ").Append(attribute);
            }
            builder.Append(">");
            foreach (Element element in Children)
            {
                builder.Append(element);
            }
            builder.AppendFormat("</{0}>", Name);
            return builder.ToString();
        }
    }
}
