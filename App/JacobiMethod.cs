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

        private int maxIterations = 5000;

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
            if (!converges())
            {
                Console.WriteLine("Matrix A does not satisfiy convergence conditions");
            }
            else
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

                    if (isAccurateEnough(k + 1))
                    {
                        break;
                    }

                    k++;

                    if (k >= maxIterations)
                    {
                        Console.WriteLine("Reached max number of iterations: " + maxIterations);
                        break;
                    }
                } while (true);

                Console.WriteLine("Calculated in " + k + " iterations");
            }
        }

        private bool converges()
        {
            float sum;

            for (int i = 0; i < matrixA.getSize(); i++)
            {
                sum = 0;

                for (int j = 0; j < matrixA.getSize(); j++)
                {
                    if (i != j) {
                        sum += matrixA.matrix[i, j];
                    }
                }

                if (Math.Abs(matrixA.matrix[i, i]) < Math.Abs(sum))
                {
                    return false;
                }
            }

            return true;
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

        private bool isAccurateEnough(int k)
        {
            float result;

            for (int i = 0; i < matrixA.getSize(); i++)
            {
                result = 0;

                for (int j = 0; j < matrixA.getSize(); j++)
                {
                    result += matrixA.matrix[i, j] * x[k].matrix[j, 0];
                }

                result -= matrixB.matrix[i, 0];

                if (epsilon < Math.Abs(result))
                {
                    return false;
                }
            }

            return true;
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
