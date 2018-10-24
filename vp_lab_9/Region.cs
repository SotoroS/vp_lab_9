namespace vp_lab_9
{
    /// <summary>
    /// Регион
    /// </summary>
    public class Region
    {
        // Левая верхняя и правая нижняя точка региона
        public Point leftTop;
        public Point rightDown;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="leftTop">Левая верхняя точка региона</param>
        /// <param name="rightDown">Правая нижняя точка региона</param>
        public Region(Point leftTop, Point rightDown)
        {
            this.leftTop = leftTop;
            this.rightDown = rightDown;
        }
    }
}
