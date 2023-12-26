using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace praktika.generators
{
    internal class Generator
    {
        public Generator()
        {
            elemGenerator = new ElemGenerator();
            matGenerator = new MatGenerator();
            nodeGenerator = new NodeGenerator();
        }

        public IGenerator elemGenerator;
        public IGenerator matGenerator;
        public IGenerator nodeGenerator;

        public void Generate()
        {
            elemGenerator.Generate();
            matGenerator.Generate();
            nodeGenerator.Generate();
        }
    }
}
