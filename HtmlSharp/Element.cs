using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSharp
{
    public abstract class Element
    {
        private List<Element> children = new List<Element>();

        public Element Parent { get; set; }
        public Element Previous { get; set; }
        public Element Next { get; set; }
        public Element NextSibling { get; set; }
        public Element PreviousSibling { get; set; }

        public IEnumerable<Element> Children { get { return children; } }
        
        protected Element()
        {
        }

        public void Setup(Element parent, Element previous)
        {
            Parent = parent;
            Previous = previous;
            if (Parent != null && Parent.children.Count > 0)
            {
                PreviousSibling = Parent.children[Parent.children.Count - 1];
                PreviousSibling.NextSibling = this;
            }
        }

        internal void AddChild(Element element)
        {
            children.Add(element);
        }
    }
}
