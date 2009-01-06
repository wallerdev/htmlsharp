using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSharp
{
    public abstract class Element
    {
        public Element Parent { get; set; }
        public Element Previous { get; set; }
        public Element Next { get; set; }
        public Element NextSibling { get; set; }
        public Element PreviousSibling { get; set; }

        public List<Element> Children { get; set; }

        public virtual string Name { get; set; }

        public Element()
        {
            Children = new List<Element>();
        }

        public void Setup(Element parent, Element previous)
        {
            Parent = parent;
            Previous = previous;
            if (Parent != null && Parent.Children.Count > 0)
            {
                PreviousSibling = Parent.Children[Parent.Children.Count - 1];
                PreviousSibling.NextSibling = this;
            }
        }
    }
}
