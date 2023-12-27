using practika.InputBC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace practika
{
    internal class BoundaryConditions
    {
        public Bc1 bc1;
        public Bc2 bc2;
        public Bc3 bc3;
        BcInputer bcInputer;
        public BoundaryConditions(GlobalMatrices globalMatrices, double hr, double hz, Grid grid, double t)
        {
            bcInputer = new(this);

            bc2.Apply(globalMatrices, hr, hz, grid, t);
            bc3.Apply(globalMatrices, hr, hz, grid, t);
            bc1.Apply(globalMatrices, grid, t);
        }
    }
    class Bc1
    {
        public Bc1(List<int[]> nodeIndices, List<double> u, int nBc)
        {
            this.nodeIndices = nodeIndices;
            this.u = u;
            this.nBc = nBc;
        }
        private int nBc;
        private List<int[]> nodeIndices;
        private List<double> u;

        public void Apply(GlobalMatrices globalMatrices, Grid grid, double t)
        {
            for (int i = 0; i < nBc; i++)
            {
                for (int k = 0; k < 2; k++)
                {
                    if (nodeIndices[i][k] != 0)
                    {
                        for (int j = globalMatrices.ig[nodeIndices[i][k] - 1]; j < globalMatrices.ig[nodeIndices[i][k]]; j++)
                        {
                            globalMatrices._globalVectorB[globalMatrices.jg[j]] -= Func.u(grid.nodes[nodeIndices[i][k]].r, grid.nodes[nodeIndices[i][k]].z, t) * globalMatrices._globaleATriangle[j];
                            globalMatrices._globaleATriangle[j] = 0;
                        }
                    }
                    globalMatrices._globalVectorB[nodeIndices[i][k]] = Func.u(grid.nodes[nodeIndices[i][k]].r, grid.nodes[nodeIndices[i][k]].z, t);
                    globalMatrices._globaleAdiag[nodeIndices[i][k]] = 1;
                    for (int j = nodeIndices[i][k] + 1; j < grid.nodes.Count; j++)
                    {
                        for (int h = globalMatrices.ig[j - 1]; h < globalMatrices.ig[j]; h++)
                        {
                            if (globalMatrices.jg[h] == nodeIndices[i][k])
                            {
                                globalMatrices._globalVectorB[j] -= Func.u(grid.nodes[nodeIndices[i][k]].r, grid.nodes[nodeIndices[i][k]].z, t) * globalMatrices._globaleATriangle[h];
                                globalMatrices._globaleATriangle[h] = 0;
                            }
                        }
                    }
                }
            }
        }
    }
    class Bc2
    {
        public Bc2(List<int[]> nodeIndices, int nBc)
        {
            this.nodeIndices = nodeIndices;
            this.theta = new double[2];
            this.nBc = nBc;
        }
        private int nBc;
        private List<int[]> nodeIndices;
        private double[] theta;
        public void Apply(GlobalMatrices globalMatrices, double hr, double hz, Grid grid, double t)
        {
            for (int i = 0; i < nBc; i++)
            {
                int k = 1;
                if (nodeIndices[i][0] < Config.NElemX)
                {
                    k = -1;
                }
                if (nodeIndices[i][1] - nodeIndices[i][0] == 1)//1x1??
                {
                    theta[0] = k * Func.thetar(grid.nodes[nodeIndices[i][0]].r, grid.nodes[nodeIndices[i][0]].z, t);
                    theta[0] = k * Func.thetar(grid.nodes[nodeIndices[i][1]].r, grid.nodes[nodeIndices[i][1]].z, t);
                    globalMatrices._globalVectorB[nodeIndices[i][0]] += ((hr * grid.nodes[nodeIndices[i][0]].r / 6) * (theta[0] * 2 + theta[1])) + ((hr * hr / 12) * (theta[0] + theta[1]));
                    globalMatrices._globalVectorB[nodeIndices[i][1]] += ((hr * grid.nodes[nodeIndices[i][0]].r / 6) * (theta[0] + theta[1] * 2)) + ((hr * hr / 12) * (theta[0] + theta[1] * 3));
                }// ловушечная r
                else
                {
                    theta[0] = k * Func.thetaz(grid.nodes[nodeIndices[i][0]].r, grid.nodes[nodeIndices[i][0]].z, t);
                    theta[0] = k * Func.thetaz(grid.nodes[nodeIndices[i][1]].r, grid.nodes[nodeIndices[i][1]].z, t);
                    globalMatrices._globalVectorB[nodeIndices[i][0]] += (hz * grid.nodes[nodeIndices[i][0]].r / 6) * (theta[0] * 2 + theta[1]);
                    globalMatrices._globalVectorB[nodeIndices[i][1]] += (hz * grid.nodes[nodeIndices[i][0]].r / 6) * (theta[0] + theta[1] * 2);
                }
            }
        }
    }
    class Bc3
    {
        public Bc3(List<int[]> nodeIndices, List<double[]> u, List<double> beta, int nBc)
        {
            this.nodeIndices = nodeIndices;
            this.u = u;
            this.beta = beta;
            this.nBc = nBc;
        }
        private int nBc;
        private List<int[]> nodeIndices;
        private List<double[]> u;
        private List<double> beta;
        public void Apply(GlobalMatrices globalMatrices, double hr, double hz, Grid grid, double t)
        {
            for (int i = 0; i < nBc; i++)
            {
                if (nodeIndices[i][1] - nodeIndices[i][0] == 1)
                {
                    globalMatrices._globalVectorB[nodeIndices[i][0]] += beta[i] * ((hr * grid.nodes[nodeIndices[i][0]].r / 6) * (u[i][0] * 2 + u[i][1])) + ((hr * hr / 12) * (u[i][0] + u[i][1]));
                    globalMatrices._globalVectorB[nodeIndices[i][1]] += beta[i] * ((hr * grid.nodes[nodeIndices[i][0]].r / 6) * (u[i][0] + u[i][1] * 2)) + ((hr * hr / 12) * (u[i][0] + u[i][1] * 3));

                    for (int j = globalMatrices.ig[nodeIndices[i][1] - 1]; j < globalMatrices.ig[nodeIndices[i][1]]; j++)
                    {
                        if (globalMatrices.jg[j] == nodeIndices[i][0])
                        {
                            globalMatrices._globaleATriangle[j] += beta[i] * (hr * grid.nodes[nodeIndices[i][0]].r / 6) + (hr * hr / 12);
                        }
                    }

                    globalMatrices._globaleAdiag[nodeIndices[i][0]] += beta[i] * (hr * grid.nodes[nodeIndices[i][0]].r / 3) + (hr * hr / 12);
                    globalMatrices._globaleAdiag[nodeIndices[i][1]] += beta[i] * (hr * grid.nodes[nodeIndices[i][0]].r / 3) + (hr * hr / 4);
                }// ловушечная r
                else
                {
                    globalMatrices._globalVectorB[nodeIndices[i][0]] += beta[i] * (hz * grid.nodes[nodeIndices[i][0]].r / 6) * (u[i][0] * 2 + u[i][1]);
                    globalMatrices._globalVectorB[nodeIndices[i][1]] += beta[i] * (hz * grid.nodes[nodeIndices[i][0]].r / 6) * (u[i][0] + u[i][1] * 2);

                    for (int j = globalMatrices.ig[nodeIndices[i][1] - 1]; j < globalMatrices.ig[nodeIndices[i][1]]; j++)
                    {
                        if (globalMatrices.jg[j] == nodeIndices[i][0])
                        {
                            globalMatrices._globaleATriangle[j] += beta[i] * (hr * grid.nodes[nodeIndices[i][0]].r / 6);
                        }
                    }
                    globalMatrices._globaleAdiag[nodeIndices[i][0]] += beta[i] * (hr * grid.nodes[nodeIndices[i][0]].r / 3);
                    globalMatrices._globaleAdiag[nodeIndices[i][1]] += beta[i] * (hr * grid.nodes[nodeIndices[i][0]].r / 3);
                }
            }
        }
    }
}
