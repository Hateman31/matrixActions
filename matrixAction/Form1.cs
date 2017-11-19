using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace matrixAction
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool isDigitValue(string value) 
        {
            bool flag = true;

            foreach (char ch in value)
            {
                if (!Char.IsDigit(ch))
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }

        int getDeterminant(int[,] matrix)
        {
            int determinant = 0;

            if (matrix.GetLength(0) == 1)
                return matrix[0,0];

            if (matrix.GetLength(0) == 2)
            {
                int a = matrix[0,0];
                int b = matrix[1,0];
                int c = matrix[1,1];
                int d = matrix[0,1];
                return a*c - b*d;
            }

            for (int j = 0; j < matrix.GetLength(0); j++)
            {
                int sign = Convert.ToInt32(Math.Pow(-1, j));
                int value = sign * matrix[j, 0] * getDeterminant(getMinorMatrix(matrix, 0,j));
                determinant += value;
            }

            return determinant;
        }



        int[,] getMinorMatrix(int[,] matrix, int y,int x)
        {
            int size = matrix.GetLength(0) - 1;

            int[,] minorMatrix = new int[size, size];

            for (int row_num = 0, count_x = 0; count_x < size; row_num++)
            {
                if (row_num != x)
                {
                    for (int col_num = 0,count_y = 0; count_y < size;col_num++)
                    {
                        if (col_num != y)
                        {
                            minorMatrix[count_x,count_y] = matrix[row_num, col_num];
                            count_y++;
                        }
                    }
                    count_x++;
                }                
            }

            return minorMatrix;
        }

        int[,] getNewMatrix(int[,] matrix, int col_num)
        {
            int[,] newMatrix = null;

            return newMatrix;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int x = dataGridView1.RowCount;
            int [,] matrix = new int[x,x];

            int determinant = getDeterminant(getMatrixFromGrid(dataGridView1));

            determinantLbl1.Text = determinant.ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        void button9_Click(object sender, EventArgs e)
        {
            buildMatrix(dataGridView1, array_size1.Text, checkBox2.Checked);
        }

        void button10_Click(object sender, EventArgs e)
        {
            buildMatrix(dataGridView2, array_size2.Text, checkBox1.Checked);
        }

        void buildMatrix(DataGridView grid,string size_txt,bool random)
        {
            if (isDigitValue(size_txt) && size_txt != "")
            {
                int size = Convert.ToInt32(size_txt);
                drawGrid(grid, size);
                if (random)
                    writeRandomMatrixToGrid(grid, size);
            }
            else
            {
                MessageBox.Show("Введите целое число!");
            }
        }


        void drawGrid(DataGridView grid, int rank)
        {
            grid.RowCount = rank;
            grid.ColumnCount = rank;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            turnGrid(dataGridView1);
        }

        void turnGrid(DataGridView grid)
        {
            int[,] matrix = getMatrixFromGrid(grid);

            for (int x=0; x < grid.RowCount; x++)
                for (int y = 0; y < grid.RowCount; y++)
                    grid[x, y].Value = matrix[y, x].ToString();
        }

        int[,] getMatrixFromGrid(DataGridView grid)
        {
            int[,] matrix = new int[grid.RowCount, grid.ColumnCount];

            for(int x=0;x<grid.RowCount;x++)
                for (int y = 0; y < grid.ColumnCount; y++)
                {
                    matrix[x, y] = Convert.ToInt32(grid[x, y].Value);
                }

            return matrix;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            turnGrid(dataGridView2);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            int flag = 0;

            if (dataGridView1.RowCount != 0 || dataGridView2.RowCount != 0)
                flag = 1;
            else
                MessageBox.Show("Одна или обе матрицы пустые");

            if (flag == 1)
            {
                if (dataGridView1.RowCount == dataGridView2.RowCount)
                    summMatrix(getMatrixFromGrid(dataGridView1), getMatrixFromGrid(dataGridView2));
                else
                    MessageBox.Show("Приведите матрицы к одному размеру!");
            }

        }

        void summMatrix(int[,] matrix1, int[,] matrix2)
        {
            for(int x=0;x<matrix1.Rank;x++)
                for(int y=0;y<matrix1.Rank;y++)
                    matrix1[x,y] = matrix1[x,y]+matrix2[x,y];

            writeMatrixToGrid(dataGridView3, matrix1);
        }

        void writeMatrixToGrid(DataGridView grid, int[,] matrix)
        {
            int size = matrix.GetLength(0);

            grid.RowCount = size;
            grid.ColumnCount = size;

            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    grid[x, y].Value = matrix[x, y].ToString();
        }

        void subMatrix(int[,] matrix1, int[,] matrix2)
        {
            for (int x = 0; x < matrix1.GetLength(0); x++)
                for (int y = 0; y < matrix1.GetLength(0); y++)
                    matrix1[x, y] = matrix1[x, y] - matrix2[x, y];

            writeMatrixToGrid(dataGridView3, matrix1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int flag = 0;

            if (dataGridView1.RowCount != 0 || dataGridView2.RowCount != 0)
                flag = 1;
            else
                MessageBox.Show("Одна или обе матрицы пустые");

            if (flag == 1)
            {
                if (dataGridView1.RowCount == dataGridView2.RowCount)
                    subMatrix(
                        getMatrixFromGrid(dataGridView1),
                        getMatrixFromGrid(dataGridView2)
                    );
                else
                    MessageBox.Show("Приведите матрицы к одному размеру!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int flag = 0;

            if (dataGridView1.RowCount != 0 || dataGridView2.RowCount != 0)
                flag = 1;
            else
                MessageBox.Show("Одна или обе матрицы пустые");

            if (flag == 1)
            {
                if (dataGridView1.RowCount == dataGridView2.RowCount)
                    multMatrix(
                        getMatrixFromGrid(dataGridView1),
                        getMatrixFromGrid(dataGridView2)
                    );
                else
                    MessageBox.Show("Приведите матрицы к одному размеру!");
            }

            

        }

        void multMatrix(int[,] matrix1, int[,] matrix2)
        {
            int size = matrix1.GetLength(0);
            int[,] matrix3 = new int[size, size];

            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    for(int k=0;k<size;k++)
                        matrix3[y,x] += matrix1[k,x] * matrix2[y,k];

            writeMatrixToGrid(dataGridView3, matrix3);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int x = dataGridView1.RowCount;
            int[,] matrix = new int[x, x];

            int determinant = getDeterminant(getMatrixFromGrid(dataGridView2));

            determinantLbl2.Text = determinant.ToString();
        }

        int[,] getRandomMatrix(int size)
        {
            int[,] matrix = new int[size,size];
            Random rnd = new Random();

            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    matrix[x, y] = rnd.Next(0,100);

            return matrix;
        }

        void writeRandomMatrixToGrid(DataGridView grid, int size)
        {
            if (size != 0)
                writeMatrixToGrid(grid, getRandomMatrix(size));
            else
                MessageBox.Show("Не задан размер матрицы!");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int size = dataGridView2.RowCount;

            if (size != 0)
                writeMatrixToGrid(dataGridView2, getRandomMatrix(size));
            else
            {
                MessageBox.Show("Не задан размер матрицы!");
            }
        }

    }
}
