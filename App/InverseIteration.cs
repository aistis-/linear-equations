using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    class InverseIteration
    {
        private Matrix matrixA;
        private Matrix matrixX;
        private Matrix matrixE;

        private float lambda;
        private float epsilon;

        private int maxIterations = 5000;

        private Dictionary<int, Matrix> x;

        public InverseIteration(Matrix matrixA, Matrix matrixX, float lambda, float epsilon)
        {
            this.matrixA = matrixA;
            this.matrixX = matrixX;

            this.lambda = lambda;
            this.epsilon = epsilon;

            this.x = new Dictionary<int, Matrix>();

            this.x.Add(0, matrixX);

            createMatrixE();
        }

        public void solve()
        {
            Dictionary<int, Matrix> y = new Dictionary<int, Matrix>(); 
            Matrix vector;
            float mismatch;
            float lambdaBefore;
            SteepestDescentMethod calculations;

            //if (isValdid())
            //{
            //    Console.WriteLine("Matrix A does not satisfiy convergence conditions");
            //}
            //else
            {
                int m = 0;

                do
                {
                    y.Add(m, generateMatrixY());

                    calculations = new SteepestDescentMethod(matrixA, y[m], this.x[0], epsilon);
                    calculations.solve(false);

                    Matrix x = calculations.getResult();

                    vector = matrixA.multiplyWithVector(y[m]);

                    lambdaBefore = lambda;
                    lambda = vector.getDotProduct(y[m]);

                    printIteration(m, lambda, y[m]);

                    if (1 <= m)
                    {
                        mismatch = 0;

                        for (int i = 0; i < y[m].getSize(); i++)
                        {
                            mismatch += (y[m].matrix[i, 0] - y[m - 1].matrix[i, 0])
                                * (y[m].matrix[i, 0] - y[m - 1].matrix[i, 0]);

                            mismatch = (float)Math.Sqrt(mismatch);
                        }

                        if (2 <= mismatch)
                        {
                            y[m] = y[m].multiplyBy(-1);
                        }

                        if (Math.Abs(lambdaBefore - lambda) <= epsilon && mismatch <= epsilon)
                        {
                            break;
                        }
			        }


                    m++;

                    if (m >= maxIterations)
                    {
                        Console.WriteLine("Reached max number of iterations: " + maxIterations);
                        break;
                    }
                } while (true);

                Console.WriteLine("Calculated in " + m + " iterations");
            }
        }

        private void createMatrixE()
        {
            float[,] E = new float[matrixA.getSize(), matrixA.getSize()];

            for (int i = 0; i < matrixA.getSize(); i++)
                for (int j = 0; j < matrixA.getSize(); j++)
                    if (i == j)
                    {
                        E[i, j] = 1;
                    }
                    else
                    {
                        E[i, j] = 0;
                    }

            matrixE = new Matrix(E);
        }

        private Matrix generateMatrixY()
        {
            float[,] y = new float[matrixA.getSize(), 1];

            for (int i = 0; i < matrixA.getSize(); i++)
            {
                y[i, 0] = 0;
            }

            y[0, 0] = 1;

            return new Matrix(y);
        }

        private void printIteration(int m, float lambda, Matrix y)
        {
            Console.Write("Iteration: " + (m + 1) + " Lambda: " + lambda + " Y: [");

            int n = y.getSize();

            for (int i = 0; i < n; i++)
            {
                if (i + 1 == n)
                {
                    Console.Write(y.matrix[i, 0] + "]\n");
                }
                else
                {
                    Console.Write(y.matrix[i, 0] + ", ");
                }
            }
        }
    }
}
