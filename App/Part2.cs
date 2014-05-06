using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    class Part2
    {
        public static string matrixT;
        public static string matrixC;
        public static string matrixX;

        public static float k;
        public static float epsilon;
        public static float lambda;

        [STAThread]
        static void Main(string[] args)
        {

            Console.WriteLine("Choose files which contains seperated matrices (T and C)");

            // iniciate file chosers for each matrix
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MatrixFilesChooser(2));

            printLoadedMatrices();

            Console.Write("Type k (number of condition): ");
            k = float.Parse(Console.ReadLine());

            Console.Write("Type accuracy (epsilon): ");
            epsilon = float.Parse(Console.ReadLine());

            Console.Write("Type lambda: ");
            lambda = float.Parse(Console.ReadLine());

            Console.WriteLine("\n\n");

            Matrix loadedMatrixA = new Matrix(matrixT, "\n").addWith(new Matrix(matrixC, "\n").multiplyBy(k));
            Matrix loadedMatrixX = new Matrix(matrixX, "\n");

            Console.WriteLine("Calculated matrix A");
            loadedMatrixA.print();
            Console.WriteLine();

            InverseIteration steepestDescentMethod = new InverseIteration(
                loadedMatrixA, loadedMatrixX, epsilon, lambda
            );

            steepestDescentMethod.solve();

            Console.ReadKey();
        }

        private static void printLoadedMatrices()
        {
            Console.WriteLine();

            Console.WriteLine("Matrix T");
            Console.WriteLine(matrixT + "\n");

            Console.WriteLine("Matrix C");
            Console.WriteLine(matrixC + "\n");
        }
    }
}
