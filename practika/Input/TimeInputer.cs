using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practika.Input
{
    internal class TimeInputer
    {
        public Time Input(Grid grid)
        {
            using (StreamReader reader = new(Config.timePath))
            {
                int nTime = int.Parse(reader.ReadLine()); ///??????
                    var line = reader.ReadLine();
                    var elemArray = line.Split(' ').ToArray();
                    grid.time = new Time( nTime, int.Parse(elemArray[0]), int.Parse(elemArray[1]));
            }
            return grid.time;
        }
    }
}
