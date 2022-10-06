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
        
        private readonly Wall wall;
        private readonly Random random;
        private readonly ConsoleColor foodColor;
        protected Food(Wall wall, int foodPoints, char foodSymbol, ConsoleColor foodColor)
            : base(wall.LeftX, wall.TopY)
        {
            this.wall = wall;
            this.FoodPoints = foodPoints;
            this.FoodSymbol = foodSymbol;
            this.foodColor  = foodColor;

            random = new Random();
        }

        public int FoodPoints { get; }
        public char FoodSymbol { get; }

        public void SetRandomPosition(Queue<Point> snake)
        {
            do
            {
                LeftX = random.Next(2, wall.LeftX - 2);
                TopY = random.Next(2, wall.TopY - 2);
            }
            while (snake.Any(x => x.LeftX == LeftX && x.TopY == TopY));

            Console.BackgroundColor = this.foodColor;
            Draw(FoodSymbol);
            Console.BackgroundColor = ConsoleColor.White;
        }

        public bool IsFoodPoint(Point snake)
        {
            return snake.LeftX == LeftX &&
                   snake.TopY == TopY;
        }
    }
}
