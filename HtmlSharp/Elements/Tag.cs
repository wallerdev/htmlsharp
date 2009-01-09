using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlSharp.Elements.Tags;
using System.Collections.ObjectModel;

namespace HtmlSharp.Elements
{
    public abstract class Tag : Element
    {
        Dictionary<string, TagAttribute> attributes = new Dictionary<string, TagAttribute>();
        public bool Hidden { get; protected set; }
        public IEnumerable<TagAttribute> Attributes { get { return attributes.Values; } }
        public string TagName { get; protected set; }
        public bool IsSelfClosing { get; protected set; }
        public bool ResetsNesting { get; protected set; }

        static Dictionary<string, Func<Tag>> tagMap = new Dictionary<string, Func<Tag>>()
        {
            {"a", () => new A()},
            {"abbr", () => new Abbr()},
            {"acronym", () => new Acronym()},
            {"address", () => new Address()},
            {"applet", () => new Applet()},
            {"area", () => new Area()},
            {"b", () => new B()},
            {"base", () => new Base()},
            {"basefont", () => new BaseFont()},
            {"bdo", () => new Bdo()},
            {"big", () => new Big()},
            {"blockquote", () => new BlockQuote()},
            {"body", () => new Body()},
            {"br", () => new Br()},
            {"button", () => new Button()},
            {"caption", () => new Caption()},
            {"center", () => new Center()},
            {"cite", () => new Cite()},
            {"code", () => new Code()},
            {"col", () => new Col()},
            {"colgroup", () => new ColGroup()},
            {"dd", () => new DD()},
            {"del", () => new Del()},
            {"dfn", () => new Dfn()},
            {"dir", () => new Dir()},
            {"div", () => new Div()},
            {"dl", () => new DL()},
            {"dt", () => new DT()},
            {"em", () => new Em()},
            {"fieldset", () => new FieldSet()},
            {"font", () => new Font()},
            {"form", () => new Form()},
            {"frame", () => new Frame()},
            {"frameset", () => new Frameset()},
            {"h1", () => new H1()},
            {"h2", () => new H2()},
            {"h3", () => new H3()},
            {"h4", () => new H4()},
            {"h5", () => new H5()},
            {"h6", () => new H6()},
            {"head", () => new Head()},
            {"hr", () => new HR()},
            {"html", () => new Html()},
            {"i", () => new I()},
            {"iframe", () => new IFrame()},
            {"img", () => new Img()},
            {"input", () => new Input()},
            {"ins", () => new Ins()},
            {"isindex", () => new IsIndex()},
            {"kbd", () => new Kbd()},
            {"label", () => new Label()},
            {"legend", () => new Legend()},
            {"li", () => new LI()},
            {"link", () => new Link()},
            {"map", () => new Map()},
            {"menu", () => new Menu()},
            {"meta", () => new Meta()},
            {"noframes", () => new NoFrames()},
            {"noscript", () => new NoScript()},
            {"object", () => new Tags.Object()},
            {"ol", () => new OL()},
            {"optgroup", () => new OptGroup()},
            {"option", () => new Option()},
            {"p", () => new P()},
            {"param", () => new Param()},
            {"pre", () => new Pre()},
            {"q", () => new Q()},
            {"s", () => new S()},
            {"samp", () => new Samp()},
            {"script", () => new Script()},
            {"select", () => new Select()},
            {"small", () => new Small()},
            {"span", () => new Span()},
            {"strike", () => new Strike()},
            {"strong", () => new Strong()},
            {"style", () => new Style()},
            {"sub", () => new Sub()},
            {"sup", () => new Sup()},
            {"table", () => new Table()},
            {"tbody", () => new TBody()},
            {"td", () => new TD()},
            {"textarea", () => new TextArea()},
            {"tfoot", () => new TFoot()},
            {"th", () => new TH()},
            {"thead", () => new THead()},
            {"title", () => new Title()},
            {"tr", () => new TR()},
            {"tt", () => new TT()},
            {"u", () => new U()},
            {"ul", () => new UL()},
            {"var", () => new Var()},
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
            attributes = new Dictionary<string, TagAttribute>();
        }

        protected Tag(params Element[] children)
            : this()
        {
            foreach (var child in children)
            {
                AddChild(child);
            }
        }

        public string this[string attribute]
        {
            get
            {
                return attributes[attribute].Value;
            }
        }

        public void AddAttribute(TagAttribute attribute)
        {
            attributes.Add(attribute.Name, attribute);
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
                if (TagName != t.TagName)
                {
                    return false;
                }
                return Attributes.SequenceEqual(t.Attributes) && Children.SequenceEqual(t.Children);
            }
        }

        public override int GetHashCode()
        {
            return TagName.GetHashCode() ^ Attributes.Aggregate(0, (a, b) => a ^= b.GetHashCode());
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder("<");
            builder.Append(TagName);
            foreach (TagAttribute attribute in Attributes)
            {
                builder.Append(" ").Append(attribute);
            }
            builder.Append(">");
            foreach (Element element in Children)
            {
                builder.Append(element);
            }
            builder.AppendFormat("</{0}>", TagName);
            return builder.ToString();
        }
    }
}
