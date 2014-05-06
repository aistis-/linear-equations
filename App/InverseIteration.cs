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

        private float epsilon;

        private int maxIterations = 5;

        private Dictionary<int, Matrix> x;
        Dictionary<int, float> lambda;

        public InverseIteration(Matrix matrixA, Matrix matrixX, float epsilon, float lambda)
        {
            this.matrixA = matrixA;
            this.matrixX = matrixX;

            this.epsilon = epsilon;

            this.x = new Dictionary<int, Matrix>();
            this.lambda = new Dictionary<int, float>();

            this.x.Add(0, matrixX);
            this.lambda.Add(0, lambda);

            createMatrixE();
        }

        public void solve()
        {
            Dictionary<int, Matrix> y = new Dictionary<int, Matrix>();
            Matrix vector;
            SteepestDescentMethod calculations;

            int m = 0;

            do
            {
                calculations = new SteepestDescentMethod(
                    matrixA.substractDiangleBy(lambda[m]),
                    this.x[m],
                    generateMatrixY(),
                    epsilon
                );

                calculations.solve(false);

                y.Add(m + 1, calculations.getResult());

                float norm = y[m + 1].normOfFrobenius();

                Matrix x = new Matrix(y[m + 1].getSize(), 1);

                for (int i = 0; i < y[m + 1].getSize(); i++)
                {
                    x.matrix[i, 0] = y[m + 1].matrix[i, 0] / norm;
                }

                this.x.Add(m + 1, x);

                vector = matrixA.multiplyWithVector(this.x[m + 1]);
                //vector.print();

                lambda[m + 1] = vector.getDotProduct(this.x[m + 1]);

                printIteration(m, y[m + 1]);

                if (isAccurateEnough(m))
                {
                    break;
                }

                if (this.x[m + 1].substractWith(this.x[m]).normOfFrobenius() >= 2)
                {
                    this.x[m + 1] = this.x[m + 1].multiplyBy(-1);
			    }


                m++;

                if (m >= maxIterations)
                {
                    Console.WriteLine("Reached max number of iterations: " + maxIterations);
                    break;
                }
            } while (true);

            Console.WriteLine("Calculated in " + (m + 1) + " iterations");
        }

        private bool isAccurateEnough(int m)
        {
            if (Math.Abs(lambda[m + 1] - lambda[m]) <= epsilon
                && x[m + 1].substractWith(x[m]).normOfFrobenius() <= epsilon)
            {
                return true;
            }
            else
            {
                return false;
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

        private void printIteration(int m, Matrix y)
        {
            Console.WriteLine("Iteration: " + (m + 1));
            Console.WriteLine("Lambda: " + lambda[m + 1]);
            Console.Write("X: [");

            int n = y.getSize();

            for (int i = 0; i < n; i++)
            {
                if (i + 1 == n)
                {
                    Console.Write(this.x[m + 1].matrix[i, 0] + "]\n");
                }
                else
                {
                    Console.Write(this.x[m + 1].matrix[i, 0] + ", ");
                }
            }

            Console.Write("Y: [");

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

            Console.WriteLine("================================");
        }
    }
}
