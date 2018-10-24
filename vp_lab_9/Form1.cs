using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace vp_lab_9
{
    public partial class Form1 : Form
    {
        Matrix matrix;
        int a = 0, b = 0, n = 0, m = 0;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Событие при клике по ячейке таблицы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Меняем цвет выделения ячейки на цвет по-умолчанию
            dataGridView.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            // Считываем значения полей формы
            int.TryParse(tbA.Text, out a);
            int.TryParse(tbB.Text, out b);
            int.TryParse(tbN.Text, out n);
            int.TryParse(tbM.Text, out m);

            if (n < 1 || m < 1 || a < 1 || b < 1 || a > n || b > m)
            {
                MessageBox.Show("Проверте правильность заполнения полей.", "Ошибка");
                return;
            }

            // Формируем матрицу
            matrix = new Matrix(n, m);
            matrix.SetRandomValue();

            // Подписываемся на событие "Выделение региона"
            matrix.SelectedRegion += selectedRegion;

            // Подписываемся на событие "Завершение поиска региона с максимальной суммой элементов"
            matrix.FinishedFindMaxRegion += finishedFindMaxRegion;

            // Задаем размер таблице
            dataGridView.RowCount = n;
            dataGridView.ColumnCount = m;

            // Заполняем таблицу значениями матрицы
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    dataGridView.Rows[i].Cells[j].Value = matrix.array[i, j];

            Thread thread = new Thread(()=>matrix.FindMaxRegion(a, b));
            thread.Start();
        }

        /// <summary>
        /// Событие "Выделение региона"
        /// </summary>
        /// <param name="r">Регион</param>
        private void selectedRegion(object r)
        {
            // Преобразуем получаемый объект
            Region region = (Region)r;

            // Очищаем выделения
            dataGridView.ClearSelection();

            // Выделяем регион
            for (int i = region.leftTop.x; i < region.rightDown.x; i++)
                for (int j = region.leftTop.y; j < region.rightDown.y; j++)
                    dataGridView.Rows[i].Cells[j].Selected = true;
        }

        /// <summary>
        /// Событие "Завершение поиска региона с максимальной суммой элементов"
        /// </summary>
        /// <param name="r">Регион</param>
        private void finishedFindMaxRegion(object r)
        {
            // Преобразуем получаемый объект
            Region region = (Region)r;

            // Очищаем выделения
            dataGridView.ClearSelection();

            // Изменям цвет выделения ячеек
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.Green;

            // Выделяем регион
            for (int i = region.leftTop.x; i < region.rightDown.x; i++)
                for (int j = region.leftTop.y; j < region.rightDown.y; j++)
                    dataGridView.Rows[i].Cells[j].Selected = true;
        }
    }
}
