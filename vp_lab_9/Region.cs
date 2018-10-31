namespace vp_lab_9
{
    /// <summary>
    /// Регион
    /// </summary>
    public class Region
    {
        private double sum;

        // Сумма элементов области
        public double Sum {
            get
            {
                return sum;
            }
        }


        // Левая верхняя и правая нижняя точка региона
        public Point leftTop;
        public Point rightDown;

        /// <summary>
        /// Конструктор
        /// </summary>
        public Region()
        {
            this.leftTop = new Point(0, 0);
            this.rightDown = new Point(0, 0);
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// 
        /// (leftTop)
        ///     +---------+
        ///     |         |
        ///     |         |
        ///     |         |
        ///     +---------+
        ///          (rightDown)
        /// 
        /// <param name="leftTop">Левая верхняя точка региона</param>
        /// <param name="rightDown">Правая нижняя точка региона</param>
        public Region(Point leftTop, Point rightDown)
        {
            this.leftTop = leftTop;
            this.rightDown = rightDown;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// 
        /// (x0, y0)
        ///     +---------+
        ///     |         |
        ///     |         |
        ///     |         |
        ///     +---------+
        ///             (x1, y1)
        /// 
        /// <param name="x0">Координата x левой верхней точки</param>
        /// <param name="y0">Координата y левой верхней точки</param>
        /// <param name="x1">Координата x правой нижней точки</param>
        /// <param name="y1">Координата y правой нижней точки</param>
        public Region(int x0, int y0, int x1, int y1)
        {
            this.leftTop = new Point(x0, y0);
            this.rightDown = new Point(x1, y1);
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// 
        /// (x0, y0)
        ///     +---------+
        ///     |         |
        ///     |         |
        ///     |         |
        ///     +---------+
        ///             (x1, y1)
        /// 
        /// <param name="x0">Координата x левой верхней точки</param>
        /// <param name="y0">Координата y левой верхней точки</param>
        /// <param name="x1">Координата x правой нижней точки</param>
        /// <param name="y1">Координата y правой нижней точки</param>
        /// <param name="sum">Сумма элементов области</param>
        public Region(int x0, int y0, int x1, int y1, double sum)
        {
            leftTop = new Point(x0, y0);
            rightDown = new Point(x1, y1);
            this.sum = sum;
        }
    }
}
