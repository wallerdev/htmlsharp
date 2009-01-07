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
        public abstract string Name { get; }
        public abstract bool IsSelfClosing { get; }
        public abstract bool IsNestable { get; }

        static Dictionary<string, Func<Tag>> tagMap = new Dictionary<string, Func<Tag>>()
        {
            {"p", () => new P()},
            {"[document]", () => new Root()}
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
            StringBuilder builder = new StringBuilder();
            string atts = string.Join(" ",
                Attributes.Select(a => string.Format("{0}=\"{1}\"", a.Name, a.Value))
                          .ToArray());

            if (atts.Length > 0)
            {
                builder.AppendFormat("<{0} {1}>", Name, atts);
            }
            else
            {
                builder.AppendFormat("<{0}>", Name);
            }
            foreach (Element ele in Children)
            {
                builder.Append(ele.ToString());
            }
            builder.AppendFormat("</{0}>", Name);
            return builder.ToString();
        }
    }
}
