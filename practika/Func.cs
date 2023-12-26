using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practika
{
    internal class Func
    {
        public static double FunctionB(double r, double z)
        {
            return r + z - 1/r;
            //return z;
            //return r-1/r;
        }
        public static double u(double r, double z)
        {
            return r + z;
            //return z;
            //return r;
        }
    }
}
