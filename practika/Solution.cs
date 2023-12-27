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
        double[] u0;
        public Solution()
        {
            grid = new Grid();
            Run();
        }
        public void Run()
        {   
            Generator generator = new();
            Inputer inputer = new(grid);
            double nev;
            double[] nevV = new double[grid.nodes.Count];
            grid.time = inputer.time;
            u0 = new double[grid.nodes.Count];
            for (int i=0; i<grid.nodes.Count;i++)
            {
                u0[i] = Func.u(grid.nodes[i].r, grid.nodes[i].z, grid.time.startTime);
            }
            LocalMatrices localMatrices = new(grid,u0, grid.time.startTime);
            GlobalMatrices globalMatrices = new(grid);
            BoundaryConditions boundaryConditions = new(globalMatrices, localMatrices.hr, localMatrices.hz, grid, grid.time.startTime);
            Slau slau = new(globalMatrices, grid.nodes.Count);
            for(int t = 1;t<grid.time.nTime;t++)
            {
                localMatrices = new(grid, slau.q, grid.time.timeSloy[t]);
                globalMatrices = new(grid);
                boundaryConditions = new(globalMatrices, localMatrices.hr, localMatrices.hz, grid, grid.time.timeSloy[t]);
                slau = new(globalMatrices, grid.nodes.Count);
                for (int i = 0; i < grid.nodes.Count; i++)
                {
                    u0[i] = Func.u(grid.nodes[i].r, grid.nodes[i].z, grid.time.timeSloy[t]);
                }
                slau.q.summ(-1, u0, nevV);
                nev = nevV.Norm()/u0.Norm();
            }
            foreach(var it in slau.q)
            {
                Console.WriteLine(it.ToString());
            }
            Console.WriteLine("точка остановки");
        }
    }
}
