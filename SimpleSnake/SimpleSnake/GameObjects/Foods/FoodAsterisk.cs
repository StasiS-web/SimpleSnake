using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnake.GameObjects.Foods
{
    public class FoodAsterisk : Food
    {
        private const char FoodSymbol = '*';
        private const int FoodPoints = 1;
        private const ConsoleColor FoodColor = ConsoleColor.DarkYellow;

        public FoodAsterisk(Field field) 
            : base(field, FoodPoints, FoodSymbol, FoodColor)
        {
        }
    }
}
