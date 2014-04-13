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

        private Dictionary<int, Matrix> x;

        public JacobiMethod(Matrix matrixA, Matrix matrixB, Matrix matrixX)
        {
            this.matrixA = matrixA;
            this.matrixB = matrixB;

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
                for (int i = 0; i < n; i++)
                {
                    sigma = 0;
                    matrix = new Matrix(getZeroMatrixForX(n));

                    for (int j = 0; j < n; j++)
                    {
                        if (i == j)
                        {
                            sigma += matrixA.matrix[i, j] * x[k].matrix[j, 0];
                        }
                    }

                    matrix.matrix[i, 0] = (matrixB.matrix[i, 0] - sigma) / matrixA.matrix[i, i];
                }

                x.Add(k + 1, matrix);

                k++;
            } while (isConvergenceReached());
        }

        private bool isConvergenceReached()
        {
            return false;
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
