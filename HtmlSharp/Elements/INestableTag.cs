using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSharp.Elements
{
    public interface INestableTag
    {
        Type[] NestingBreakers { get; }
    }
}
