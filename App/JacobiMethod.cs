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

        private Dictionary<int, Dictionary<int, float>> x;

        public JacobiMethod(Matrix matrixA, Matrix matrixB, float x)
        {
            this.matrixA = matrixA;
            this.matrixB = matrixB;

            this.x = new Dictionary<int, Dictionary<int, float>>();

            for (int i = 0; i < matrixA.getSize(); i++)
            {
                this.x[i][0] = x;
            }
        }

        public void solve()
        {
            int k = 0;
            int n = matrixA.getSize();

            float sigma;

            for (int i = 0; i < n; i++)
            {
                sigma = 0;

                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                    {
                        sigma += matrixA.matrix[i, j] * x[j][k];
                    }
                }

                x[i][k + 1] = (matrixB.matrix[i, 0] - sigma) / matrixA.matrix[i, i];
            }

            k++;


// k = 0 
//while convergence not reached do
//for i := 1 step until n do
// \sigma = 0 
//for j := 1 step until n do
//if j ≠ i then
// \sigma  = \sigma  + a_{ij} x_j^{(k)} 
//end if
//end (j-loop)
//  x_i^{(k+1)}  = {{\left( {b_i  - \sigma } \right)} \over {a_{ii} }} 
//end (i-loop)
//check if convergence is reached
//k = k + 1
//loop (while convergence condition not reached)
        }
    }
}
