using System;
using System.Threading;
using System.Windows.Forms;

namespace vp_lab_9
{
    // Матрица
    public class Matrix
    {
        // Размеры матрицы
        private int n, m, s;

        // Двумерный массив хранищий в себе значения матрицы n x m
        public double[,] array;

        /// <summary>
        /// Конструктор матрицы
        /// </summary>
        /// <param name="n">Размер матрицы n</param>
        /// <param name="m">Размер матрицы m</param>
        /// <param name="s">Задержка междлу итерациями вычисления (мс)</param>
        public Matrix(int n, int m, int s)
        {
            this.n = n;
            this.m = m;
            this.s = s;

            // Инициализация рамера матрицы
            array = new double[n, m];
        }

        /// <summary>
        /// Заполнение массива случайными значениями
        /// </summary>
        public void SetRandomValue()
        {
            Random random = new Random();

            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    //array[i, j] = random.Next(-100, 100);
                    array[i, j] = random.Next(-1, 2);
                    //array[i, j] = random.NextDouble() * random.Next(-100, 100);
                    //array[i, j] = random.Next(0, 2);
        }

        /// <summary>
        /// Поиск региона с максимальной суммой элементов в матрице
        /// </summary>
        public void FindMaxRegion()
        {
            try
            {
                // Область с максимальной суммой элементов
                Region maxRegion = new Region(0, 0, 1, 1, array[0, 0] + array[0, 1] + array[1, 0] + array[1, 1]);

                // Проходимся по всем элементам матрицы
                // Координаты текущего элемента - i, j
                //
                //      +---------------+
                //      |               |
                //      |    *          |
                //      |     (i,j)     |
                //      |               |
                //      |               |
                //      +---------------+
                //
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {

                        // Проходимся по все возможным областям
                        // за счет изменения размера исследуемой области (a, b)
                        //
                        // За минимальную область считаем квадратную матрицу 2x2
                        // a = i + 1; b = j + 1; 
                        //
                        //      +---------------+
                        //      |  (i,j)        |
                        //      |    *---+      |
                        //      |    |   |      |
                        //      |    +---+      |
                        //      |        (a,b)  |
                        //      +---------------+
                        //
                        for (int a = i; a <= n; a++)
                        {
                            for (int b = j; b <= m; b++)
                            {

                                // Переменная для хранения суммы элементов исследуемой области
                                double sum = 0;

                                // Проходимся по всем элементам исследуемой области
                                // и вычисляем их сумму
                                for (int x = i; x < a; x++)
                                {
                                    for (int y = j; y < b; y++)
                                    {
                                        sum += array[x, y];
                                    }
                                }

                                // Если сумма элементов исследуемой области
                                // больше в области с максимальной суммой элементов
                                // Переназначаем область
                                if (maxRegion.Sum < sum) maxRegion = new Region(i, j, a, b, sum);

                                // Сообщаем о регионне с максимальной суммой элементов на данный момент
                                OnSelectedRegion(new Region(i, j, a, b));

                                // Задержка
                                Thread.Sleep(s);

                            }
                        }
                    }
                }

                // Сообщаем о найденом регионе с максимальной суммой элементов
                OnFinishedFindMaxRegion(maxRegion);
            }
            catch (ThreadAbortException e)
            {
                MessageBox.Show("Вычисления преждевременно прерваны.", "Внимание");
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// Обертка для события выбора региона
        /// с проверкой на пустату списка подписчиков
        /// </summary>
        /// <param name="region"></param>
        public void OnSelectedRegion(object region)
        {
            if (SelectedRegion != null) SelectedRegion((Region)region);
        }

        /// <summary>
        /// Обертка для события завершения поиска 
        /// региона с максимальной суммой элементов
        /// </summary>
        /// <param name="region"></param>
        public void OnFinishedFindMaxRegion(object region)
        {
            if (FinishedFindMaxRegion != null) FinishedFindMaxRegion((Region)region);
        }

        /// <summary>
        /// Событие выбора региона
        /// </summary>
        public event Action<Region> SelectedRegion;

        /// <summary>
        /// Событие завершения поиска региона с максимальной суммой элементов
        /// </summary>
        public event Action<Region> FinishedFindMaxRegion;
    }
}
