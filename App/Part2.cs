﻿using System;
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

        public static float k;
        public static float epsilon;

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

            Console.WriteLine("\n\n");

            Matrix loadedMatrixA = new Matrix(matrixD, "\n").addWith(new Matrix(matrixC, "\n").multiplyBy(k));
            Matrix loadedMatrixB = new Matrix(matrixB, "\n");
            Matrix loadedMatrixX = new Matrix(matrixX, "\n");

            // some test data
            //loadedMatrixA.matrix = new float[3, 3] {{2, 1, 0.95f}, {1, 2, 1}, {0.95f, 1, 2}};
            //loadedMatrixB.matrix = new float[3, 1] {{3.95f}, {4}, {3.95f}};
            //loadedMatrixX.matrix = new float[3, 1] { { 0 }, { 0 }, { 0 } };

            JacobiMethod jacobiMethod = new JacobiMethod(
                loadedMatrixA, loadedMatrixB, loadedMatrixX, epsilon
            );

            jacobiMethod.solve();

            SteepestDescentMethod steepestDescentMethod = new SteepestDescentMethod(
                loadedMatrixA, loadedMatrixB, loadedMatrixX, epsilon
            );

            steepestDescentMethod.solve();

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

            Console.WriteLine("Matrix X");
            Console.WriteLine(matrixX + "\n");
        }
    }
}
