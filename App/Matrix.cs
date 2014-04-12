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
        private float[][] matrix
        {
            get { return matrix; }
            set { matrix = value; }
        }

        public Matrix(string matrixInString, char lineSeparator)
        {
            string[] rows = matrixInString.Split(lineSeparator);

            for (int i = 0; i < rows.Length; i++)
            {
                // lets trim all white-spaces to one space
                string[] values = Regex.Replace(rows[i], @"\s+", " ").Split(' ');

                for (int j = 0; j < values.Length; i++)
                {
                    matrix[i][j] = float.Parse(values[j]);
                }
            }
        }

    }
}
