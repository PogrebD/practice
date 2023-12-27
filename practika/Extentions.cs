using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace practika
{
    public static class Extentions
    {
        public static double[,] Multiply(this double[,] matrix, double coefficient, double[,] result)
        {
            for (var i = 0; i < matrix.GetUpperBound(0) + 1; i++)
            {
                for (var j = 0; j < matrix.Length / (matrix.GetUpperBound(0) + 1); j++)
                {
                    result[i, j] = matrix[i, j] * coefficient;
                }
            }
            return result;
        }
        public static double[] MultiplyVector(this double[,] matrix, double[] vector, double[] result)
        {
            for (var i = 0; i < matrix.GetUpperBound(0) + 1; i++)
            {
                for (var j = 0; j < matrix.Length / (matrix.GetUpperBound(0) + 1); j++)
                {
                    result[i] += matrix[i, j] * vector[j];
                }
            }
            return result;
        }
        public static double[,] Sum(this double[,] matrix1, double[,] matrix2, double[,] result)
        {
            for (var i = 0; i < matrix1.GetUpperBound(0) + 1; i++)
            {
                for (var j = 0; j < matrix1.Length / (matrix1.GetUpperBound(0) + 1); j++)
                {
                    result[i, j] = matrix1[i, j] + matrix2[i, j];
                }
            }
            return result;
        }
        public static double[] summ(this double[] a, double b, double[] c, double[] d)
        {
            for (int i = 0; i < d.Length; i++)
                d[i] = a[i] + b * c[i];
            return d;
        }
        public static double Norm(this double[] y)
        {
            double norma = 0;
            for (int i = 0; i < y.Length; i++)
                norma += y[i] * y[i];
            return Math.Sqrt(norma);
        }
        public static List<List<double>> ToList(this double[,] matrix)
        {
            List<List<double>> result = new();
            for (var i = 0; i < matrix.GetUpperBound(0) + 1; i++)
            {
                for (var j = 0; j < matrix.Length / (matrix.GetUpperBound(0) + 1); j++)
                {
                    result[i][j] = matrix[i, j];
                }
            }
            return result;
        }
        public static void Init(this List<List<int>> list, int n)
        {
            for (var i = 0; i < n; i++)
            {
                list.Add(new(0));
            }
        }
        public static void Init(this List<List<double>> list, int n)
        {
            for (var i = 0; i < n; i++)
            {
                list.Add(new(0));
            }
        }
    }
}
