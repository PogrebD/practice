using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace praktika.generators
{
    internal class MatGenerator : IGenerator
    {
        public MatGenerator()
        {
            _path = @"C:\Users\dimap\source\repos\practika\practika\Input\Mat.txt";
        }
        private string _path;
        public void Generate()
        {
                File.WriteAllText(_path, "empty(");
        }
    }
}
