using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("     Решение квадратных систем линейных уравнений методом обратной матрицы");
            Console.WriteLine("Введите количество переменных");
            var n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите уравнения, после каждого нажимая enter");
            var list = new string[n];
            for (var i = 0; i < n; i++)                       
                list[i] = Console.ReadLine();
            
            Matrix.ConvertToMatrix(list);
            Console.Read();            
        }
    }
}
