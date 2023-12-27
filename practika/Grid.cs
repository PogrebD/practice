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
        public Time time;

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
        public double[,] matrixM;
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
        public Elem(double[,] matrix, double[,] matrix2, Elem elem)
        {
            index = elem.index;
            matrixA = matrix;
            matrixM = matrix2;
            vectorB = elem.vectorB;
        }
        public Elem(double[] list, Elem elem)
        {
            index = elem.index;
            matrixA = elem.matrixA;
            vectorB = list;
        }
    }

    class Time
    {
        public int nTime;
        public int startTime;
        public int endTime;
        public double[] timeSloy;
        public double ht;

        public Time()
        {
            timeSloy = new double[0];
        }

        public Time(int nTime, int startTime, int endTime)
        {
            this.nTime = nTime;
            this.startTime = startTime;
            this.endTime = endTime;
            ht = (endTime - startTime)/nTime;
            CutTime();
        }
        private void CutTime()
        {
            timeSloy = new double[nTime];
            for (int i = 0;i< nTime; i++)
            {
                timeSloy[i] = startTime + ht*i; 
            }
        }
    }
}
