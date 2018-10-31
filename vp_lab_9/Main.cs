using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace vp_lab_9
{
    public partial class Form1 : Form
    {
        // Матрица
        Matrix matrix;

        // Поток
        Thread thread;

        // Размеры матриц
        int a = 0, b = 0, n = 0, m = 0, s = 0;

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

        /// <summary>
        /// Поиск региона с максимальной суммой элементов в матрице
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            // Останавливаем предыдущий поток, если он существует
            if (thread != null) thread.Abort();

            // Меняем цвет выделения ячейки на цвет по-умолчанию
            dataGridView.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;

            // Считываем значения полей формы
            int.TryParse(tbN.Text, out n);
            int.TryParse(tbM.Text, out m);
            int.TryParse(tbS.Text, out s);

            // проверка на корректность введенных данных
            if (n < 1 || m < 1 || s < 0)
            {
                MessageBox.Show("Проверте правильность заполнения полей.", "Ошибка");
                return;
            }

            // Формируем матрицу
            matrix = new Matrix(n, m, s);
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

            // Инициализируем и запускаем поток для поиска региона с макимальной суммой элементов
            thread = new Thread(()=>matrix.FindMaxRegion());
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
