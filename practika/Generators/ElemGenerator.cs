using practika;
using praktika.generators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace praktika
{
    internal class ElemGenerator : IGenerator
    {
        public void Generate()
        {
            File.WriteAllText(Config.elemPath, Config.NElem.ToString() + "\n");
            for (int i = 0; i < Config.NElemY; i++)
            {
                for (int j = 0; j < Config.NElemX; j++)
                {
                    double x1 = i * (Config.NElemX + 1) + j;
                    double y1 = (i + 1) * (Config.NElemX + 1) + j;
                    string str = string.Format("{0} {1} {2} {3}\n", x1, x1 + 1, y1, y1 + 1);
                    File.AppendAllText(Config.elemPath, str);
                }
            }
        }
    }
}
