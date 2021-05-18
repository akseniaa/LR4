using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR3
{
    class Matrix
    {

        public static double Determ(double[,] matrix)
        {
            if (matrix.GetLength(0) != matrix.GetLength(1)) throw new Exception("Матрица не квадратная");
            double det = 0;
            var num = matrix.GetLength(0);
            if (num == 1) det = matrix[0, 0];
            if (num == 2) det = matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            if (num > 2)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    det += Math.Pow(-1, 0 + j) * matrix[0, j] * Determ(GetMinor(matrix, 0, j));
                }
            }
            return det;
        }

        public static double[,] GetMinor(double[,] matrix, int row, int column)
        {
            if (matrix.GetLength(0) != matrix.GetLength(1)) throw new Exception("Матрица не квадратная");
            var arr = new double[matrix.GetLength(0) - 1, matrix.GetLength(0) - 1];
            for (var i = 0; i < matrix.GetLength(0); i++)
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    if ((i != row) || (j != column))
                    {
                        if (i > row && j < column) arr[i - 1, j] = matrix[i, j];
                        if (i < row && j > column) arr[i, j - 1] = matrix[i, j];
                        if (i > row && j > column) arr[i - 1, j - 1] = matrix[i, j];
                        if (i < row && j < column) arr[i, j] = matrix[i, j];
                    }
                }
            return arr;
        }

        static double[] result;
        public static void ConvertToMatrix(string[] input)
        {
            double num;
            var n = input.Length;
            var A = new double[n, n];
            var b = new double[n];           
            var flag = true;
            for (var i = 0; i < n; i++)
            {
                var j = 0;
                var s = input[i].Split('=');
                b[i] = Convert.ToDouble(s[1]);
                foreach(var item in s[0])
                {
                    if (item == '-') flag = false;
                    if (double.TryParse(item.ToString(), out num))
                    {
                        if (flag)
                            A[i, j] = Convert.ToDouble(item.ToString());
                        else
                        {
                            A[i, j] = -Convert.ToDouble(item.ToString());
                            flag = true;
                        }                             
                        j++;
                    }                        
                }
            }
            if (SLAU_InverseMatrix( A, b) == 1)
            {
                Console.WriteLine("Система не имеет решений или имеет бесконечно много решений");
                Console.Read();
                return;
            }
            else
            {
                for (int i = 0; i < n; i++)
                    Console.WriteLine("x" + i + " = " + result[i]);
                Console.Read();
            }                
        }

        public static double[,] MatrixInverse(double[,] matrix)
        {
            var n = matrix.GetLength(0);
            if (Determ(matrix) == 0)
            {
                Console.WriteLine("Матрица не имеет обратной");
                Console.Read();
                return matrix;
            }
            else
            {
                var result = new double[n, n];
                for (var i = 0; i < n; i++)
                    for (var j = 0; j < n; j++)
                    {
                        result[i, j] = Determ(GetMinor(matrix, i, j)) * Math.Pow(-1, i+j+2);
                    }
                result = Transpose(result, n);
                var det = Determ(matrix);
                for (var i = 0; i < n; i++)
                    for (var j = 0; j < n; j++)
                    {
                        result[i, j] /= det;
                    }
                return result;
            }            
        }

        static double[,] Transpose(double[,] a, int n)
        {
            double tmp;
            for (int i = 0; i < n; i++)            
                for (int j = 0; j < i; j++)
                {
                    tmp = a[i, j];
                    a[i, j] = a[j, i];
                    a[j, i] = tmp;
                }
            return a;
        }

        public static double[] Multiply(double[,] A, double[] B)
        {
            double[] res = new double[B.GetLength(0)];
            for (int i = 0; i < A.GetLength(0); i++)            
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    res[j] += A[i, j] * B[i];
                }            
            return res;
        }
        public static int SLAU_InverseMatrix( double[,] A, double[] b)
        {
            double det = Determ(A);
            if (det == 0) return 1;
            result = Multiply(MatrixInverse(A), b);
            return 0;
        }

        static double[,] GenerateMatrix()
        {
            var m = new double[3, 3];
            var r = new Random();
            for (var i = 0; i < 3; i++)            
                for (var j = 0; j < 3; j++)
                {
                    m[i, j] = r.Next(0, 50);
                }
            return m;
        }


    }
}
