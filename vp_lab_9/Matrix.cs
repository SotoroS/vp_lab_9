using System;
using System.Threading;

namespace vp_lab_9
{
    // Матрица
    public class Matrix
    {
        // Размеры матрицы
        private int n, m;

        // Двумерный массив хранищий в себе значения матрицы n x m
        public double[,] array;

        /// <summary>
        /// Конструктор матрицы
        /// </summary>
        /// <param name="n">Размер матрицы n</param>
        /// <param name="m">Размер матрицы m</param>
        public Matrix(int n, int m)
        {
            this.n = n;
            this.m = m;

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
                    //array[i,j] = random.NextDouble() * random.Next(-100, 100);
                    array[i, j] = random.Next(0, 2);
        }

        /// <summary>
        /// Поиск региона с максимальной суммой элементов в матрице
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public void FindMaxRegion(int a, int b)
        {
            // Массив сумм всех регионов
            double[,] sum;

            // Искомый регион
            Region maxRegion = new Region();

            // Инициализация размера массива
            sum = new double[n - a + 1, m - b + 1];

            // Просчет всех сумм элементов всех регионов
            for (int i = 0; i < n - a + 1; i++)
            {
                for (int j = 0; j < m - b + 1; j++)
                {
                    for (int x = i; x < i + a; x++)
                    {
                        for (int y = j; y < j + b; y++)
                        {
                            sum[i, j] += array[x, y];
                        }
                    }

                    // Сообщаем о используемом регионне в данный момент
                    OnSelectedRegion(new Region(new Point(i, j), new Point(i + a, j + b)));

                    Thread.Sleep(100);
                }
            }

            // Инициализация максимального значения
            double max = sum[0, 0];

            // Поиск региона с максимальной суммой элементов региона
            for (int x = 1; x < n - a + 1; x++)
            {
                for (int y = 1; y < m - b + 1; y++)
                {
                    if (max < sum[x, y])
                    {
                        max = sum[x, y];
                        maxRegion = new Region(new Point(x, y), new Point(x + a, y + b));
                    }
                }
            }

            OnFinishedFindMaxRegion(maxRegion);
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
