using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace practika
{
    internal class LocalMatrices
    {
        public double hr;
        public double hz;
        private Grid _grid;
        private double[,] _stiffnessR;
        private double[,] _stiffnessZ;
        private double[,] _stiffness;
        private double[,] _massR1;
        private double[,] _massR2;
        private double[,] _massZ;
        private double[,] _mass;
        private double[] _q;
        private double _t;
        public LocalMatrices(Grid grid, double[] q, double t)
        {
            hr = grid.nodes[1].r - grid.nodes[0].r;
            hz = grid.nodes[Config.NElemX + 1].z - grid.nodes[0].z;
            _grid = grid;
            _stiffnessR = new double[2, 2];
            _stiffnessZ = new double[2, 2];
            _stiffness = new double[4, 4];
            _massR1 = new double[2, 2];
            _massR2 = new double[2, 2];
            _massZ = new double[2, 2];
            _mass = new double[4, 4];
            _q = q;
            _t = t;
            CalcLocalMatrices(grid);
        }
        public void CalcLocalMatrices(Grid grid)
        {
            for (int i = 0; i < _grid.elems.Count; i++)
            {
                grid.elems[i] = new Elem(CalcAMatrix(_grid.elems[i]),CalcMassMatrix(_grid.elems[i]), _grid.elems[i]);
                grid.elems[i] = new Elem(CalcBVector(_grid.elems[i]), _grid.elems[i]);
            }
        }
        public double[,] CalcAMatrix(Elem elem)
        {
            CalcMassMatrix(elem);
            _mass.Multiply(1 / _grid.time.ht, _mass);
            CalcStiffnessMatrix(elem);
            double[,] result = new double[4, 4];
            _mass.Sum(_stiffness, result);
            return result;
        }
        public double[,] CalcMassMatrix(Elem elem)
        {
            var massR = CalcLocalMassRMatrix(elem);
            var massZ = CalcLocalMassZMatrix();

            for (var i = 0; i < elem.index.Length; i++)
            {
                for (var j = 0; j <= i; j++)
                {
                    _mass[i, j] = Config.gamma * massR[GetMuIndex(i), GetMuIndex(j)] * massZ[GetNuIndex(i), GetNuIndex(j)];
                    _mass[j, i] = _mass[i, j];
                }
            }
            return _mass;
        }
        public double[] CalcBVector(Elem elem)
        {
            CalcMassMatrix(elem);
            var b = new double[4];
            for (var i = 0; i < _mass.GetUpperBound(0) + 1; i++)
            {
                for (var j = 0; j < _mass.Length / (_mass.GetUpperBound(0) + 1); j++)
                {
                    b[j] += _mass[j, i] / Config.gamma * Func.FunctionB(_grid.nodes[elem.index[i]].r, _grid.nodes[elem.index[i]].z, _t) + (_mass[j, i] / _grid.time.ht) *_q[j];
                }
            }
            return b;
        }

        public void CalcStiffnessMatrix(Elem elem)
        {
            var massR = CalcLocalMassRMatrix(elem);
            var massZ = CalcLocalMassZMatrix();

            var stiffnessR = CalcLocalStiffnessRMatrix(elem);
            var stiffnessZ = CalcLocalStiffnessZMatrix();

            for (var i = 0; i < elem.index.Length; i++)
            {
                for (var j = 0; j <= i; j++)
                {
                    _stiffness[i, j] = Config.lambda * stiffnessR[GetMuIndex(i), GetMuIndex(j)] * massZ[GetNuIndex(i), GetNuIndex(j)] +
                                       massR[GetMuIndex(i), GetMuIndex(j)] * stiffnessZ[GetNuIndex(i), GetNuIndex(j)];
                    _stiffness[j, i] = _stiffness[i, j];
                }
            }
        }

        public double[,] CalcLocalStiffnessRMatrix(Elem elem)
        {
            GetStiffnessMatrix().Multiply((_grid.nodes[elem.index[0]].r + (hr / 2)) / (hr), _stiffnessR);

            return _stiffnessR;
        }
        public double[,] CalcLocalMassRMatrix(Elem elem)
        {
            GetMassRMatrix().Multiply(hr * hr / 12d, _massR1);

            GetMassZMatrix().Multiply(hr * _grid.nodes[elem.index[0]].r / 6d, _massR2);

            _massR1.Sum(_massR2, _massR1);

            return _massR1;
        }

        public double[,] CalcLocalStiffnessZMatrix()
        {
            GetStiffnessMatrix().Multiply(1d / hz, _stiffnessZ);

            return _stiffnessZ;
        }
        public double[,] CalcLocalMassZMatrix()
        {
            GetMassZMatrix().Multiply(hz / 6d, _massZ);

            return _massZ;
        }

        public double[,] GetStiffnessMatrix() => new double[2, 2] { { 1d, -1d }, { -1d, 1d } };
        public double[,] GetMassZMatrix() => new double[2, 2] { { 2d, 1d }, { 1d, 2d } };
        public double[,] GetMassRMatrix() => new double[2, 2] { { 1d, 1d }, { 1d, 3d } };

        private static int GetMuIndex(int i)
        {
            return i % 2;
        }
        private static int GetNuIndex(int i)
        {
            return i / 2;
        }
    }
}


