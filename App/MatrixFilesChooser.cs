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
        public MatrixFilesChooser()
        {
            InitializeComponent();
        }

        private void MatrixFilesChooser_Load(object sender, EventArgs e)
        {
            string[] letters = { "B", "C", "D", "X" };

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

                                switch (i)
                                {
                                    case 0:
                                        Program.matrixB = content;
                                        break;
                                    case 1:
                                        Program.matrixC = content;
                                        break;
                                    case 2:
                                        Program.matrixD = content;
                                        break;
                                    case 3:
                                        Program.matrixX = content;

                                        this.Close();

                                        break;
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
