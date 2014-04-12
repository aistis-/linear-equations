using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    class Program
    {
        public static string matrixB;
        public static string matrixC;
        public static string matrixD;

        public static float k;
        public static float x;

        [STAThread]
        static void Main(string[] args)
        {

            Console.WriteLine("Choose files which contains seperated matrix (B, C and D)");

            // iniciate file chosers for each matrix
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MatrixFilesChooser());

            printLoadedMatrices();

            Console.Write("Type k (number of condition): ");
            k = float.Parse(Console.ReadLine());

            Console.Write("Type x (x zero): ");
            x = float.Parse(Console.ReadLine());

            Console.WriteLine("\n\n");

            Matrix matrixA = new Matrix(matrixD, "\n").addWith(new Matrix(matrixC, "\n").multiplyBy(k));

            JacobiMethod calculations = new JacobiMethod(matrixA, new Matrix(matrixB, "\n"), x);
            calculations.solve();

            Console.ReadKey();
        }

        private static void printLoadedMatrices()
        {
            Console.WriteLine();

            Console.WriteLine("Matrix B");
            Console.WriteLine(matrixB + "\n");

            Console.WriteLine("Matrix C");
            Console.WriteLine(matrixC + "\n");

            Console.WriteLine("Matrix D");
            Console.WriteLine(matrixD + "\n");
        }
    }
}
