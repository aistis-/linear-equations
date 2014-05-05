using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace App
{
    public partial class MatrixFilesChooser : Form
    {
        private int part;

        public MatrixFilesChooser(int part)
        {
            this.part = part;

            InitializeComponent();
        }

        private void MatrixFilesChooser_Load(object sender, EventArgs e)
        {
            string[] letters;
            
            if (part == 1)
            {
                letters = new string[] { "B", "C", "D", "X" };
            }
            else {
                letters = new string[] { "T", "C", "X"};
            }

            for (int i = 0; i < letters.Length; i++)
            {
                Stream fileStream = null;
                OpenFileDialog theDialog = new OpenFileDialog();
                theDialog.Title = "Choose matrix " + letters[i];
                theDialog.Filter = "TXT files|*.txt";
                theDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                theDialog.Multiselect = false;

                if (theDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if ((fileStream = theDialog.OpenFile()) != null)
                        {
                            using (fileStream)
                            {
                                StreamReader reader = new StreamReader(fileStream);
                                string content = reader.ReadToEnd();

                                if (1 == part)
                                {
                                    switch (i)
                                    {
                                        case 0:
                                            Part1.matrixB = content;
                                            break;
                                        case 1:
                                            Part1.matrixC = content;
                                            break;
                                        case 2:
                                            Part1.matrixD = content;
                                            break;
                                        case 3:
                                            Part1.matrixX = content;

                                            this.Close();

                                            break;
                                    }
                                }
                                else
                                {
                                    switch (i)
                                    {
                                        case 0:
                                            Part2.matrixT = content;
                                            break;
                                        case 1:
                                            Part2.matrixC = content;
                                            break;
                                        case 2:
                                            Part2.matrixX = content;
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    }
                }
                else
                {
                    Environment.Exit(1);
                }
            }
        }
    }
}
