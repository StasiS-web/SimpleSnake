using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleSnake.GameObjects.Foods
{
    public abstract class Food : Point
    {
        private readonly Random random;
        private readonly Field field;
        private readonly ConsoleColor foodColor;
        protected Food(Field field, int foodPoints, char foodSymbol, ConsoleColor foodColor)
            : base(field.LeftX, field.TopY)
        {
            random = new Random();

            this.field = field;
            this.FoodPoints = foodPoints;
            this.FoodSymbol = foodSymbol;
            this.foodColor  = foodColor;
        }

        public int FoodPoints { get; }
        public char FoodSymbol { get; }

        public void SetRandomPosition(Queue<Point> snake)
        {
            do
            {
                LeftX = random.Next(2, field.LeftX - 2);
                TopY = random.Next(2, field.TopY - 2);
            }
            while (snake.Any(x => x.LeftX == LeftX && x.TopY == TopY));

            Console.BackgroundColor = this.foodColor;
            Draw(FoodSymbol);
            Console.BackgroundColor = ConsoleColor.White;
        }

        public bool IsFoodPoint(Point snake)
        {
            return snake.TopY == TopY &&
                   snake.LeftX == LeftX;
        }
    }
}
