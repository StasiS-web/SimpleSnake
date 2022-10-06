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
        private const int FoodPoints = 3;
        private const ConsoleColor FoodColor = ConsoleColor.Red;

        public FoodHash(Field field)
            : base(field, FoodPoints, FoodSymbol, FoodColor)
        {
        }
    }
}
