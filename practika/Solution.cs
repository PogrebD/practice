using praktika.generators;
using praktika.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practika
{
    internal class Solution
    {
        Grid grid;
        public Solution()
        {
            grid = new Grid();
        }
        public void Run()
        {
            Generator generator = new();
            generator.Generate();
            Inputer inputer = new(grid);
            LocalMatrices localMatrices = new(grid);
            GlobalMatrices globalMatrices = new(grid);
            BoundaryConditions boundaryConditions = new(globalMatrices, localMatrices.hr, localMatrices.hz, grid);
            Slau slau = new(globalMatrices, grid.nodes.Count);
            foreach(var it in slau.q)
            {
                Console.WriteLine(it.ToString());
            }
            Console.WriteLine("точка остановки");
        }
    }
}
