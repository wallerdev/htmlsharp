using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

namespace TagGenerator
{
    enum Dir
    {
        Ltr,
        Rtl
    }
    class TagClass
    {
        public string Name { get; set; }
        public List<KeyValuePair<string, object>> Attributes { get; set; }

        public bool StartTagOptional { get; set; }
        public bool EndTagOptional { get; set; }
        public bool AllowsNesting { get; set; }
        public bool ResetsNesting { get; set; }
        public List<string> NestingBreakers = new List<string>();

        public TagClass(string name)
        {
            Name = name.ToLower();
            Attributes = new List<KeyValuePair<string, object>>();
        }

        public void AddStringAttribute(string name)
        {
            Attributes.Add(new KeyValuePair<string, object>(name, typeof(string)));
        }

        public void AddIntAttribute(string name)
        {
            Attributes.Add(new KeyValuePair<string, object>(name, typeof(int)));
        }

        public void AddAttribute(string name, Type t)
        {
            Attributes.Add(new KeyValuePair<string, object>(name, t));
        }

        public void AddCoreAttributes()
        {
            string[] coreAttributes = { "id", "class", "style", "title" };
            foreach (string a in coreAttributes)
            {
                AddStringAttribute(a);
            }
        }

        public void AddI18nAttributes()
        {
            AddStringAttribute("lang");
            AddAttribute("dir", typeof(Dir));
        }

        public string GetNestingBreakersString()
        {
            if (NestingBreakers.Count > 0)
            {
                string breakers = string.Join(", ",
                    NestingBreakers.Select(s => string.Format("typeof({0})",
                            GetClassName(s)))
                        .ToArray());
                return string.Format("public IEnumerable<Type> NestingBreakers {{ get {{ return new Type[] {{ {0} }}; }} }}\r\n\r\n        ",
                    breakers);
            }
            else
            {
                return "public IEnumerable<Type> NestingBreakers { get { return new Type[0]; } }\r\n\r\n        ";
            }
        }

        public string GetAttributesString()
        {
            string atts = "";
            foreach (var att in Attributes)
            {
                if (Name != att.Key)
                {
                    atts += string.Format("public string {0} {{ get {{ return this[\"{1}\"]; }} }}\r\n\r\n        ",
                        CleanAttribute(att.Key),
                        att.Key);
                }
            }
            return atts;
        }

        private string CleanAttribute(string s)
        {
            if (s == "readonly")
            {
                return "ReadOnly";
            }
            else
            {
                return new CultureInfo("en-US", false).TextInfo.ToTitleCase(s.Replace("-", ""));
            }
        }

        public string GetClassDefinition()
        {
            return string.Format(@"using System;
using System.Collections.Generic;

namespace HtmlSharp.Elements.Tags
{{
    public class {0} : Tag{1}
    {{
        {2}{4}public {0}()
            : this(new Element[0])
        {{
        }}

        public {0}(params Element[] children)
            : this(new TagAttribute[0], children)
        {{
        }}

        public {0}(params TagAttribute[] attributes)
            : this(attributes, new Element[0])
        {{
        }}

        public {0}(IEnumerable<TagAttribute> attributes, params Element[] children)
            : base(attributes, children)
        {{
            {3}TagName = ""{5}"";
        }}
    }}
}}", GetClassName(Name)
  , AllowsNesting ? ", IAllowsNesting" : ""
  , AllowsNesting ? GetNestingBreakersString() : ""
  , ResetsNesting ? "ResetsNesting = true;\r\n            " : ""
  , GetAttributesString()
  , Name);
        }

        public static string GetClassName(string Name)
        {
            if (Name.Length == 2 && !new[] { "br", "em" }.Contains(Name))
            {
                return Name.ToUpper();
            }
            else if (Name == "basefont")
            {
                return "BaseFont";
            }
            else if (Name == "blockquote")
            {
                return "BlockQuote";
            }
            else if (Name == "colgroup")
            {
                return "ColGroup";
            }
            else if (Name == "fieldset")
            {
                return "FieldSet";
            }
            else if (Name == "iframe")
            {
                return "IFrame";
            }
            else if (Name == "isindex")
            {
                return "IsIndex";
            }
            else if (Name == "noframes")
            {
                return "NoFrames";
            }
            else if (Name == "noscript")
            {
                return "NoScript";
            }
            else if (Name == "optgroup")
            {
                return "OptGroup";
            }
            else if (Name == "tbody")
            {
                return "TBody";
            }
            else if (Name == "textarea")
            {
                return "TextArea";
            }
            else if (Name == "tfoot")
            {
                return "TFoot";
            }
            else if (Name == "thead")
            {
                return "THead";
            }
            else
            {
                return new CultureInfo("en-US", false).TextInfo.ToTitleCase(Name);
            }
        }
    }
    class Program
    {
        static List<TagClass> tags = new List<TagClass>();
        static Regex tableRow = new Regex("<tr.*?>(.*?)</tr>", RegexOptions.Singleline);
        static Regex tableData = new Regex("<td.*?>(.*?)</td>", RegexOptions.Singleline);

        static void Main(string[] args)
        {
            ParseTags();
            ParseAttributes();
            foreach (TagClass t in tags)
            {
                //Console.WriteLine(t.GetClassDefinition());
                //File.Delete(@"C:\Documents and Settings\Administrator\My Documents\Visual Studio 2008\Projects\HtmlSharp\HtmlSharp\Elements\" +
                //    new CultureInfo("en-US", false).TextInfo.ToTitleCase(t.Name) + ".cs");

                File.WriteAllText(@"C:\Documents and Settings\Administrator\My Documents\Visual Studio 2008\Projects\HtmlSharp\HtmlSharp\Elements\Tags\" +
                    TagClass.GetClassName(t.Name) + ".cs",
                    t.GetClassDefinition());

                Console.WriteLine("{{\"{0}\", () => new {1}()}},", t.Name, TagClass.GetClassName(t.Name));
            }

            Console.WriteLine(tags.Count);
        }

        static void ParseTags()
        {
            string html = File.ReadAllText("tags.html");
            foreach (Match match in tableRow.Matches(html))
            {
                string row = match.Groups[1].Value;
                List<string> infos = new List<string>();
                foreach (Match m in tableData.Matches(row))
                {
                    infos.Add(Clean(m.Value));
                }
                if (infos.Count > 0)
                {
                    TagClass t = new TagClass(infos[0]);
                    t.StartTagOptional = infos[1] == "O";
                    t.EndTagOptional = infos[2] == "O";
                    t.AllowsNesting = infos[3] != "E";

                    if (t.Name == "li")
                    {
                        t.NestingBreakers.AddRange(new[] { "ul", "ol" });
                    }
                    else if (t.Name == "dd" || t.Name == "dt")
                    {
                        t.NestingBreakers.Add("dl");
                    }
                    else if (t.Name == "tr")
                    {
                        t.NestingBreakers.AddRange(new[] { "table", "tbody", "tfoot", "thead" });
                    }
                    else if (t.Name == "td" || t.Name == "th")
                    {
                        t.NestingBreakers.Add("tr");
                    }
                    else if (t.Name == "thead" || t.Name == "tbody" || t.Name == "tfoot")
                    {
                        t.NestingBreakers.Add("table");
                    }

                    if (new[] { "address", "form", "p", "pre", "table", "tr", "td", "th", "thead", "tbody",
                        "tfoot", "ol", "ul", "li", "dl", "dd", "dt", "blockquote", "div", "fieldset", "ins",
                        "del", "noscript" }.Contains(t.Name))
                    {
                        t.ResetsNesting = true;
                    }
                    tags.Add(t);
                }
            }
        }

        static string Clean(string s)
        {
            string cleaned = Regex.Replace(s, "<.*?>", "", RegexOptions.Singleline);
            return Regex.Replace(cleaned, @"\n|\r", "");
        }

        static void ParseAttributes()
        {
            string html = File.ReadAllText("attributes.html");

            foreach (Match match in tableRow.Matches(html))
            {
                string row = match.Groups[1].Value;
                List<string> infos = new List<string>();
                foreach (Match m in tableData.Matches(row))
                {
                    infos.Add(Clean(m.Value));
                }
                if (infos.Count > 0)
                {
                    string attribute = infos[0];
                    string appliesTo = infos[1];
                    if (appliesTo.StartsWith("All elements but"))
                    {
                        string filtered = appliesTo.Replace("All  elements but ", "");
                        var tagsFound = appliesTo.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                        var lowerTags = tagsFound.Select(x => x.ToLower());
                        foreach (var tag in tags.Where(t => !lowerTags.Contains(t.Name)))
                        {
                            tag.AddAttribute(attribute, typeof(string));
                        }
                    }
                    else
                    {
                        var tagsFound = appliesTo.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                        var lowerTags = tagsFound.Select(x => x.ToLower());
                        foreach (var tag in tags.Where(t => lowerTags.Contains(t.Name)))
                        {
                            tag.AddAttribute(attribute, typeof(string));
                        }
                    }
                }
            }
        }
    }
}
