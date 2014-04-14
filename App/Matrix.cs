using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace App
{
    class Matrix
    {
        public float[,] matrix;

        public Matrix(float[,] matrix)
        {
            this.matrix = matrix;
        }

        public Matrix(string matrixInString, string lineSeparator)
        {
            string[] rows = matrixInString.Split(new string[] { lineSeparator }, StringSplitOptions.None);

            for (int i = 0; i < rows.Length; i++)
            {
                // lets trim all white-spaces to one space
                string[] values = Regex.Replace(rows[i], @"\s+", " ").Trim().Split(' ');

                if (0 == i)
                {
                    matrix = new float[rows.Length, values.Length];
                }

                for (int j = 0; j < values.Length; j++)
                {
                    matrix[i, j] = float.Parse(values[j]);
                }
            }
        }

        public int getSize()
        {
            return matrix.GetLength(0);
        }

        public Matrix multiplyBy(float multiplier)
        {
            float[,] result = matrix;

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int column = 0; column < matrix.GetLength(1); column++)
                {
                    result[row, column] *= multiplier;
                }
            }

            return new Matrix(result);
        }

        public Matrix addWith(Matrix matrix)
        {
            float[,] result = new float[this.matrix.GetLength(0), this.matrix.GetLength(1)];

            for (int row = 0; row < this.matrix.GetLength(0); row++)
            {
                for (int column = 0; column < this.matrix.GetLength(1); column++)
                {
                    result[row, column] = this.matrix[row, column] + matrix.matrix[row, column];
                }
            }

            return new Matrix(result);
        }

        public Matrix substractWith(Matrix matrix)
        {
            float[,] result = new float[this.matrix.GetLength(0), this.matrix.GetLength(1)];

            for (int row = 0; row < this.matrix.GetLength(0); row++)
            {
                for (int column = 0; column < this.matrix.GetLength(1); column++)
                {
                    result[row, column] = this.matrix[row, column] - matrix.matrix[row, column];
                }
            }

            return new Matrix(result);
        }

        public Matrix multiplyWith(Matrix matrix)
        {
            float[,] result = new float[this.matrix.GetLength(0), 1];

            for (int row = 0; row < this.matrix.GetLength(0); row++)
            {
                result[row, 0] = 0;

                for (int column = 0; column < this.matrix.GetLength(1); column++)
                {
                    result[row, 0] += this.matrix[row, column] * matrix.matrix[column, 0];
                }
            }

            return new Matrix(result);
        }

        public float getDotProduct(Matrix matrix)
        {
            float result = 0;

            for (int row = 0; row < this.matrix.GetLength(0); row++)
            {
                result += this.matrix[row, 0] * matrix.matrix[row, 0];
            }

            return result;
        }

        public void print()
        {
            for (int row = 0; row < this.matrix.GetLength(0); row++)
            {
                for (int column = 0; column + 1 < this.matrix.GetLength(1); column++)
                {
                    Console.Write(String.Format("{0:0.#####}", matrix[row, column]) + " ");
                }

                Console.Write(String.Format("{0:0.#####}", matrix[row, this.matrix.GetLength(1) - 1]) + "\n");
            }
        }

        public float normOfFrobenius()
        {
            float result = 0;

            for (int row = 0; row < this.matrix.GetLength(0); row++)
            {
                for (int column = 0; column + 1 < this.matrix.GetLength(1); column++)
                {
                    result += this.matrix[row, column] * this.matrix[row, column];
                }
            }

            return (float) Math.Sqrt(result);
        }
    }
}
