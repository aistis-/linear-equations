using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    class SteepestDescentMethod
    {
        private Matrix matrixA;
        private Matrix matrixB;

        private float epsilon;

        private Dictionary<int, Matrix> x;

        private int maxIterations = 100000;

        public SteepestDescentMethod(Matrix matrixA, Matrix matrixB, Matrix matrixX, float epsilon)
        {
            this.matrixA = matrixA;
            this.matrixB = matrixB;

            this.epsilon = epsilon;

            this.x = new Dictionary<int, Matrix>();

            this.x.Add(0, matrixX);
        }

        public void solve()
        {
            solve(true);
        }

        public void solve(bool print)
        {
            if (!converges())
            {
                Console.WriteLine("Matrix A does not satisfiy convergence conditions");
            }
            else
            {
                int k = 0;
                int n = matrixA.getSize();

                Matrix matrixX;

                Matrix matrixZ = matrixA.multiplyWith(x[k]).substractWith(matrixB);
                Matrix matrixR;

                float tau;

                do {
                    matrixR = matrixA.multiplyWith(matrixZ);

                    tau = matrixZ.getDotProduct(matrixZ) / matrixR.getDotProduct(matrixZ);

                    matrixX = x[k].substractWith(matrixZ.multiplyBy(tau));
                    matrixZ = matrixA.multiplyWith(matrixX).substractWith(matrixB);

                    x.Add(k + 1, matrixX);

                    if (print)
                    {
                        printIteration(k, matrixX, matrixZ);
                    }

                    if (isAccurateEnough(matrixZ, k))
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

                if (print)
                {
                    Console.WriteLine("Calculated in " + (k + 1) + " iterations");
                }
            }
        }

        private bool converges()
        {
            for (int i = 0; i < matrixA.getSize(); i++)
            {
                for (int j = 0; j < matrixA.getSize(); j++)
                {
                    if (matrixA.matrix[i, j] != matrixA.matrix[j, i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void printIteration(int k, Matrix matrixX, Matrix matrixZ)
        {
            Console.Write("Iteration: " + (k + 1) + "  X: [");

            int n = matrixX.getSize();

            for (int i = 0; i < n; i++)
            {
                if (i + 1 == n)
                {
                    Console.Write(matrixX.matrix[i, 0] + "]\n");
                }
                else
                {
                    Console.Write(matrixX.matrix[i, 0] + ", ");
                }
            }

            Console.Write("Iteration: " + (k + 1) + "  Z: [");

            for (int i = 0; i < n; i++)
            {
                if (i + 1 == n)
                {
                    Console.Write(matrixZ.matrix[i, 0] + "]\n");
                }
                else
                {
                    Console.Write(matrixZ.matrix[i, 0] + ", ");
                }
            }
        }

        private bool isAccurateEnough(Matrix matrixZ, int k)
        {
            float result;
            float accuracy = 0;

            for (int i = 0; i < matrixA.getSize(); i++)
            {
                result = 0;

                for (int j = 0; j < matrixA.getSize(); j++)
                {
                    result += matrixA.matrix[i, j] * x[k].matrix[j, 0];
                }

                result -= matrixB.matrix[i, 0];

                if (accuracy < Math.Abs(result))
                {
                    accuracy = Math.Abs(result);
                }
            }

            //Console.WriteLine("Iteration: " + (k + 1) + "  Accuracy = " + accuracy);

            return matrixZ.getDotProduct(matrixZ) < epsilon * epsilon;
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

        public Matrix getResult()
        {
            return x.Last().Value;
        }
    }
}
