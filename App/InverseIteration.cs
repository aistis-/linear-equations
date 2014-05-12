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

        private int maxIterations = 1000000;

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
            Matrix calculations;

            int m = 0;

            do
            {
                calculations = calculateGradient(
                    matrixA.substractDiangleBy(lambda[m]),
                    this.x[m],
                    generateMatrixY(),
                    epsilon
                );

                y.Add(m + 1, calculations);

                float norm = y[m + 1].normOfFrobenius();

                Matrix x = new Matrix(y[m + 1].getSize(), 1);

                for (int i = 0; i < y[m + 1].getSize(); i++)
                {
                    x.matrix[i, 0] = y[m + 1].matrix[i, 0] / norm;
                }

                this.x.Add(m + 1, x);

                vector = matrixA.multiplyWithVector(this.x[m + 1]);

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

                if (m >= maxIterations)
                {
                    Console.WriteLine("Reached max number of iterations: " + maxIterations);
                    break;
                }

                m++;
            } while (true);

            Console.WriteLine("Calculated in " + (m + 1) + " iterations");
        }

        private bool isAccurateEnough(int m)
        {
            if (Math.Abs(lambda[m + 1] - lambda[m]) <= epsilon
                //&& x[m + 1].substractWith(x[m]).normOfFrobenius() <= epsilon
                )
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

            Console.WriteLine("================================");
        }

        private Matrix calculateGradient(Matrix A, Matrix X, Matrix B, float epsilon)
        {
            int size = X.getSize();

            double[] p0 = new double[size];
            double[] z0 = new double[size];
            double[] p = new double[size];
            double[] r = new double[size];
            double[] z = new double[size];
            double[] n = new double[size];
            double[] x = new double[size];
            double[] x0 = matrixVectorToDouble(X);
            double tk;
            double Bk;
            int k = 0;
            double mistake = Math.Pow(epsilon, 2);

            for (int i = 0; i < size; i++)
                x[i] = matrixVectorToDouble(X)[i];

            for (int i = 0; i < size; i++)
            {
                p[i] = 0;

                for (int j = 0; j < size; j++)
                    p[i] = p[i] + A.matrix[i, j] * x0[j];

                p[i] = p[i] - B.matrix[i, 0];
                z[i] = p[i];
            }

            while (true)
            {
                for (int i = 0; i < size; i++)
                {
                    x0[i] = x[i];
                    p0[i] = p[i];
                    z0[i] = z[i];
                }

                for (int i = 0; i < size; i++)
                {
                    r[i] = 0;
                    for (int j = 0; j < size; j++)
                        r[i] = r[i] + A.matrix[i, j] * p0[j];
                }

                double vectorZ0 = vector(z0, z0);
                tk = vectorZ0 / vector(r, p0);

                for (int i = 0; i < size; i++)
                {
                    x[i] = x0[i] - tk * p0[i];
                    z[i] = z0[i] - tk * r[i];
                }

                for (int i = 0; i < size; i++)
                {
                    n[i] = 0;
                    for (int j = 0; j < size; j++)
                        n[i] = n[i] + A.matrix[i, j] * x[j];
                    n[i] = n[i] - B.matrix[i, 0];
                }

                double vectorZ = vector(z, z);

                if (vectorZ < mistake)
                    break;
                else
                    Bk = vectorZ / vectorZ0;
                for (int i = 0; i < size; i++)
                    p[i] = z[i] + Bk * p0[i];
                k++;
            }

            float[,] result = new float[size, 1];

            for (int i = 0; i < size; i++)
            {
                result[i, 0] = (float)x[i];
            }

            return new Matrix(result);
        }

        private double[] matrixVectorToDouble(Matrix matrix)
        {
            double[] result = new double[matrix.getSize()];

            for (int i = 0; i < matrix.getSize(); i++)
            {
                result[i] = matrix.matrix[i, 0];
            }

            return result;
        }

        private double vector(double[] x, double[] y)
        {
            double sum = 0;

            for (int i = 0; i < x.Length; i++)
                sum = sum + x[i] * y[i];

            return sum;
        }
    }
}
