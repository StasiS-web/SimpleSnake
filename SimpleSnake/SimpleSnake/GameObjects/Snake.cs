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
        private readonly IList<Food> food;
        private readonly Field field;

        private int nextLeftX;
        private int nextTopY;
        private int foodIndex;

        private Snake()
        {
            this.snakeElements = new Queue<Point>();
            this.food = new List<Food>();
            this.foodIndex = this.RandomFoodNumber;
        }

        public Snake(Field field)
            : base()
        {
            this.field = field;
            
            this.GetFoods();
            this.CreateSnake();
        }

        public bool CanMove(Point direction)
        {
            Point currSnakeHead = this.snakeElements.Last();
            GetNextPoint(direction, currSnakeHead);

            bool isNextPointOfSnake = this.snakeElements
                .Any(p => p.LeftX == this.nextLeftX && p.TopY == this.nextTopY);

            if (isNextPointOfSnake)
            {
                return false;
            }

            Point newSnakeHead = new Point(this.nextLeftX, this.nextTopY);

            if (this.field.IsPointOfWall(newSnakeHead))
            {
                return false;
            }

            this.snakeElements.Enqueue(newSnakeHead);
            newSnakeHead.Draw(SnakeSymbol);

            if (this.food[foodIndex].IsFoodPoint(newSnakeHead))
            {
                this.Eat(direction, currSnakeHead);
            }

            Point snakeTail = this.snakeElements.Dequeue();
            snakeTail.Draw(EmptySpace);

            return true;
        }

        private void CreateSnake()
        {
            for (int topY = 1; topY <= 6; topY++)
            {
                this.snakeElements.Enqueue(new Point(2, topY));
            }

            this.foodIndex = this.RandomFoodNumber;
            this.food[this.foodIndex].SetRandomPosition(this.snakeElements); // Spawn initial food
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
                    .CreateInstance(foodType, new object[] {this.field});
                this.food.Add(currentFood);
            }
        }

        private void GetNextPoint(Point direction, Point snakeHead)
        {
            this.nextLeftX = snakeHead.LeftX + direction.LeftX;
            this.nextTopY = snakeHead.TopY + direction.TopY;
        }
        
        private void Eat(Point direction, Point currSnakeHead)
        {
            int points = this.food[foodIndex].FoodPoints;

            for (int i = 0; i < points; i++)
            {
                Point newPoint = new Point(this.nextLeftX, this.nextTopY);
                this.snakeElements.Enqueue(newPoint);
                newPoint.Draw(SnakeSymbol);

                this.GetNextPoint(direction, currSnakeHead);
            }

            this.foodIndex = this.RandomFoodNumber;
            this.food[this.foodIndex].SetRandomPosition(this.snakeElements); // Spawn new food
        }

        private int RandomFoodNumber => new Random().Next(0, this.food.Count);
    }
}
