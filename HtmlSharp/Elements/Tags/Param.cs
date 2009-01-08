using System;

namespace HtmlSharp.Elements.Tags
{
    class Param : Tag
    {
        public string Id {  get { return this["id"]; } }

        public string Name {  get { return this["name"]; } }

        public string Type {  get { return this["type"]; } }

        public string Value {  get { return this["value"]; } }

        public string Valuetype {  get { return this["valuetype"]; } }

        public Param()
        {
            
        }
    }
}