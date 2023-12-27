using practika;
using practika.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace praktika.Input
{
    internal class Inputer
    {
        ElemInputer elemInputer = new();
        NodeInputer nodeInputer = new();
        TimeInputer timeInputer = new();
        public Time time = new();
        public Inputer (Grid grid)
        {
            elemInputer.Input(grid);
            nodeInputer.Input(grid);
            time = timeInputer.Input(grid);
        }
    }
}
