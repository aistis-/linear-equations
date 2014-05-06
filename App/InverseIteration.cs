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

                    SteepestDescentMethod calculations = new SteepestDescentMethod(matrixA, y[m], this.x[m], epsilon);
                    calculations.solve();

                    Matrix x = calculations.getResult();

                    vector = matrixA.multiplyWithVector(y[m]);
                    lambda = vector.getDotProduct(y[m]);

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
    }
}
