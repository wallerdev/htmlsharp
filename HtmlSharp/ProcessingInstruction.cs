using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSharp
{
    public class ProcessingInstruction : Text
    {
        public override string ToString()
        {
            return string.Format("<?{0}?>", base.ToString());
        }
    }
}
