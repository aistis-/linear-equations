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

        private float lambda;
        private float epsilon;

        public InverseIteration(Matrix matrixA, Matrix matrixX, float lambda, float epsilon)
        {
            this.matrixA = matrixA;
            this.matrixX = matrixX;

            this.lambda = lambda;
            this.epsilon = epsilon;
        }

        public void solve()
        {
        }
    }
}
