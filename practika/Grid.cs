using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practika
{
    struct Grid
    {
        public List<Node> nodes = new();
        public List<Elem> elems = new();

        //List<Vector> vectorB;
        //vector<Mat> Mats;
        public Grid()
        {
        }
    }
    struct Node
    {
        public double r;
        public double z;

        public Node(double r, double z)
        {
            this.r = r;
            this.z = z;
        }
    }
    struct Elem
    {
        public int[] index;
        public double[,] matrixA;
        public double[] vectorB;
        public Elem()
        {
            index = new int[4];
            matrixA = new double[4, 4];
            vectorB = new double[4];
        }
        public Elem(int[] ints)
        {
            index = ints;
            matrixA = new double[4, 4];
            vectorB = new double[4];
        }
        public Elem(double[,] matrix, Elem elem)
        {
            index = elem.index;
            matrixA = matrix;
            vectorB = elem.vectorB;
        }
        public Elem(double[] list, Elem elem)
        {
            index = elem.index;
            matrixA = elem.matrixA;
            vectorB = list;
        }
    }
}
