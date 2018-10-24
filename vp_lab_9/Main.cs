using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace vp_lab_9
{
    public partial class Main : Form
    {
        Matrix matrix;
        int a, b, n, m;


        public Main()
        {
            InitializeComponent();

        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            int.TryParse(tbA.Text, out a);
            int.TryParse(tbB.Text, out b);
            int.TryParse(tbN.Text, out n);
            int.TryParse(tbM.Text, out m);

            matrix = new Matrix(n, m);
            matrix.SetRandomValue();

            matrix.SelectedRegion += selectedRegion;

            dataGridView.RowCount = n;
            dataGridView.ColumnCount = m;

            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    dataGridView.Rows[i].Cells[j].Value = matrix.array[i, j];

            Console.WriteLine("Result {0}", matrix.FindMaxRegion(a, b));
        }

        private void selectedRegion(object r)
        {
            // Преобразуем получаемый объект
            Region region = (Region)r;

            // Очищаем выделения
            dataGridView.ClearSelection();

            // выделяем регион
            for (int i = region.leftTop.x; i < region.rightDown.x - region.leftTop.x; i++)
                for (int j = region.leftTop.y; j < region.rightDown.y - region.leftTop.y; j++)
                    dataGridView.Rows[i].Cells[j].Selected = true;
        }
    }
}
