using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnake.GameObjects.Foods
{
    public class FoodHash : Food
    {
        private const char FoodSymbol = '#';
        private const int HashFoodPoints = 3;
        private const ConsoleColor FoodColor = ConsoleColor.Red;

        public FoodHash(Wall wall)
            : base(wall, HashFoodPoints, FoodSymbol, FoodColor)
        {
        }
    }
}
