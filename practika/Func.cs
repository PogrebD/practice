using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practika
{
    internal class Func
    {
        public static double FunctionB(double r, double z, double t)
        {
            return 20 * r * t + 4 - 5*t*t / r;
            //return r + z + t - 1 / r;
            //return z;
            //return r-1/r;
        }
        public static double u(double r, double z, double t)
        {
            return 5 * r * t * t + 2 * t;
            //return r + z + t;
            //return z;
            //return r;
        }
        public static double thetar(double r, double z, double t)
        {
            return 5*t*t;
        }
        public static double thetaz(double r, double z, double t)
        {
            return 0;
        }
    }
}
