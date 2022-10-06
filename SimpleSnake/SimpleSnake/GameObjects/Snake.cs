using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SimpleSnake.GameObjects.Foods;

namespace SimpleSnake.GameObjects
{
    public class Snake
    {
        private const char SnakeSymbol = '\u25CF';
        private const char EmptySpace = ' ';

        private readonly Queue<Point> snakeElements;
        private readonly IList<Food> foods;
        private readonly Wall wall;

        private int nextLeftX;
        private int nextTopY;
        private int foodIndex;

        private Snake()
        {
            this.foodIndex = this.RandomFoodNumber;
            this.snakeElements = new Queue<Point>();
            this.foods = new List<Food>(3);
        }

        public Snake(Wall wall)
            : base()
        {
            this.wall = wall;
            
            this.GetFoods();
            this.CreateSnake();
        }

        private void CreateSnake()
        {
            for (int leftX = 1; leftX <= 6; leftX++)
            {
                this.snakeElements.Enqueue(new Point(leftX, 2));
            }

            this.foodIndex = this.RandomFoodNumber;
            this.foods[this.foodIndex].SetRandomPosition(this.snakeElements); // Spawn initial food
        }

        public int RandomFoodNumber => new Random().Next(0, this.foods.Count);

        public bool CanMoving(Point direction)
        {
            Point currentSnakeHead = this.snakeElements.Last();
            GetNextPoint(direction, currentSnakeHead);

            bool isNextPointOfSnake = this.snakeElements
                .Any(p => p.LeftX == this.nextLeftX && p.TopY == this.nextTopY);

            if (isNextPointOfSnake)
            {
                return false;
            }

            Point newSnakeHead = new Point(this.nextLeftX, this.nextTopY);

            if (this.wall.IsPointOfWall(newSnakeHead))
            {
                return false;
            }

            this.snakeElements.Enqueue(newSnakeHead);
            newSnakeHead.Draw(SnakeSymbol);

            if (this.foods[foodIndex].IsFoodPoint(newSnakeHead))
            {
                this.Eat(direction, currentSnakeHead);
            }

            Point snakeTail = this.snakeElements.Dequeue();
            snakeTail.Draw(EmptySpace);

            return true;
        }
        private void Eat(Point direction, Point currentSnakeHead)
        {
            int length = this.foods[foodIndex].FoodPoints;

            for (int i = 0; i < length; i++)
            {
                Point newPoint = new Point(this.nextLeftX, this.nextTopY);
                this.snakeElements.Enqueue(newPoint);
                newPoint.Draw(SnakeSymbol);

                this.GetNextPoint(direction, currentSnakeHead);
            }

            this.wall.AddPoints(this.snakeElements);
            this.wall.PlayerInfo();

            this.foodIndex = this.RandomFoodNumber;
            this.foods[this.foodIndex].SetRandomPosition(this.snakeElements); // Spawn new food
        }


        private void GetFoods()
        {
            Type[] foodTypes = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.Name.ToLower().StartsWith("food") && 
                            !t.IsAbstract)
                .ToArray();

            foreach (Type foodType in foodTypes)
            {
                Food currentFood = (Food)Activator
                    .CreateInstance(foodType, new object[] {this.wall});
                this.foods.Add(currentFood);
            }
        }

        private void GetNextPoint(Point direction, Point snakeHead)
        {
            this.nextLeftX = snakeHead.LeftX + direction.LeftX;
            this.nextTopY = snakeHead.TopY + direction.TopY;
        }
    }
}
