using System;

namespace DddDemo.AnemicModel
{
    /// <summary>
    /// Разделение на данные
    ///
    /// Это НЕ анемичный класс, так как есть неизменяемость
    /// Не нужно беспокоится о нарушении согласованности
    /// Нет необходимости в инкапсуляции
    /// </summary>
    public sealed class Square
    {
        public double SideLength { get; }

        public Square(double sideLength)
        {
            if (sideLength <= 0)
            {
                throw new ArgumentException($"Invalid side length: {sideLength}");
            }

            SideLength = sideLength;
        }
    }

    /// <summary>
    /// И на операцию над этими данными
    /// </summary>
    public class Services
    {
        public static double CalculateArea(Square square)
            => square.SideLength * square.SideLength;

        public static Square Double(Square old)
            => new Square(old.SideLength * 2);
    }
}
