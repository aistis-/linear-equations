using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    class JacobiMethod
    {
        private Matrix matrixA;
        private Matrix matrixB;

        private float epsilon;

        private Dictionary<int, Matrix> x;

        public JacobiMethod(Matrix matrixA, Matrix matrixB, Matrix matrixX, float epsilon)
        {
            this.matrixA = matrixA;
            this.matrixB = matrixB;

            this.epsilon = epsilon;

            this.x = new Dictionary<int, Matrix>();

            this.x.Add(0, matrixX);
        }

        public void solve()
        {
            int k = 0;
            int n = matrixA.getSize();

            float sigma;
            Matrix matrix = null;

            do
            {
                matrix = new Matrix(getZeroMatrixForX(n));

                for (int i = 0; i < n; i++)
                {
                    sigma = 0;

                    for (int j = 0; j < n; j++)
                    {
                        if (i != j)
                        {
                            sigma += matrixA.matrix[i, j] * x[k].matrix[j, 0];
                        }
                    }

                    matrix.matrix[i, 0] = (matrixB.matrix[i, 0] - sigma) / matrixA.matrix[i, i];
                }

                x.Add(k + 1, matrix);

                printIteration(k, matrix);

                if (isConvergenceReached(k + 1))
                {
                    break;
                }

                k++;
            } while (true);

            Console.WriteLine("Calculated in " + k + " iterations");
        }

        private void printIteration(int k, Matrix matrix)
        {
            Console.Write("Iteration: " + (k + 1) + "  X: [");

            int n = matrix.getSize();

            for (int i = 0; i < n; i++)
            {
                if (i + 1 == n)
                {
                    Console.Write(matrix.matrix[i, 0] + "]\n");
                }
                else
                {
                    Console.Write(matrix.matrix[i, 0] + ", ");
                }
            }
        }

        private bool isConvergenceReached(int k)
        {
            float coef;
            float result = 0;

            for (int i = 0; i < matrixA.getSize(); i++)
            {
                coef = 0;

                for (int j = 0; j < matrixA.getSize(); j++)
                {
                    coef += matrixA.matrix[j, i];
                }

                result += coef * x[k].matrix[i, 0];
                result -= matrixB.matrix[i, 0];
            }

            return epsilon >= Math.Abs(result);
        }

        private float[,] getZeroMatrixForX(int n)
        {
            float[,] matrix = new float[n, 1];

            for (int i = 0; i < n; i++)
            {
                matrix[i, 0] = 0;
            }

            return matrix;
        }
    }
}
