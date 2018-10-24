using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        /// Получение региона максимальной матрицы
        /// </summary>
        /// <returns></returns>
        public double FindMaxRegion(int a, int b)
        {
            double[,] sum;
            double max = 0;

            if (n < m)
            {
                sum = new double[m - b + 1, n - a + 1];

                for (int i = 0; i < m - b; i++)
                {
                    for (int j = 0; j < n - a; j++)
                    {
                        for (int x = i; x < i + b; x++)
                        {
                            for (int y = j; y < j + a; y++)
                            {
                                sum[i, j] += array[x, y];
                            }
                        }

                        OnSelectedRegion(new Region(new Point(i, j), new Point(i + b, j + a)));

                        Thread.Sleep(100);
                    }
                }
            }
            else
            {
                sum = new double[n - a + 1, m - b + 1];

                for (int i = 0; i < n - a; i++)
                {
                    for (int j = 0; j < m - b; j++)
                    {
                        for (int x = i; x < i + a; x++)
                        {
                            for (int y = j; y < j + b; y++)
                            {
                                sum[i, j] += array[x, y];
                            }
                        }

                        OnSelectedRegion(new Region(new Point(i, j), new Point(i + a, j + b)));

                        Thread.Sleep(100);

                    }
                }
            }

            foreach (double el in sum)
                max = (el > max) ? el : max;

            return max;
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
        /// Событие выбора региона
        /// </summary>
        public event Action<Region> SelectedRegion;
    }
}
