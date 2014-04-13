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
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int column = 0; column < matrix.GetLength(1); column++)
                {
                    matrix[row, column] *= multiplier;
                }
            }

            return this;
        }

        public Matrix addWith(Matrix matrix)
        {
            for (int row = 0; row < this.matrix.GetLength(0); row++)
            {
                for (int column = 0; column < this.matrix.GetLength(1); column++)
                {
                    this.matrix[row, column] += matrix.matrix[row, column];
                }
            }

            return this;
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
    }
}
