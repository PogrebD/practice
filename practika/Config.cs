using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practika
{
    struct Config
    {
        public const double gamma = 1;
        public const double lambda = 1;
        public const int NElemX = 2;
        public const int NElemY = 2;
        public const int NElem = NElemX * NElemY;
        public const int NNode = (NElemX + 1) * (NElemY + 1);
        public const int Nmat = 1;
        public const double x1 = 2, x2 = 4, y1 = 2, y2 = 4;
        public const string elemPath = @"C:\Users\dimap\source\repos\practika\practika\Input\Elem.txt";
        public const string nodePath = @"C:\Users\dimap\source\repos\practika\practika\Input\Node.txt";
        public const string timePath = @"C:\Users\dimap\source\repos\practika\practika\Input\Time.txt";
        public const string bc1Path = @"C:\Users\dimap\source\repos\practika\practika\InputBC\Bc1.txt";
        public const string bc2Path = @"C:\Users\dimap\source\repos\practika\practika\InputBC\Bc2.txt";
        public const string bc3Path = @"C:\Users\dimap\source\repos\practika\practika\InputBC\Bc3.txt";
    }
}
