using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Data
{
    public class ScreenLineEntry
    {
        public string backgroundColor { get; set; }
        public string foregroundColor { get; set; }
        public string text { get; set; }

        public ScreenLineEntry(string backgroundColor, string foregroundColor, string text)
        {
            this.backgroundColor = backgroundColor;
            this.foregroundColor = foregroundColor;
            this.text = text;
        }
    }
}
