using practika;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace praktika.generators
{
    internal class NodeGenerator : IGenerator
    {
        public void Generate()
        {
            File.WriteAllText(Config.nodePath, Config.NNode.ToString() + "\n");
            for (int i = 0; i < Config.NElemY + 1; i++)
            {
                for (int j = 0; j < Config.NElemX + 1; j++)
                {
                    double hx = (Config.x2 - Config.x1) / Config.NElemX;
                    double hy = (Config.y2 - Config.y1) / Config.NElemY;
                    string str = string.Format("{0} {1}\n", Config.x1 + (hx * j), Config.y1 + (hy * i));
                    File.AppendAllText(Config.nodePath, str);
                }
            }
        }
    }
}
