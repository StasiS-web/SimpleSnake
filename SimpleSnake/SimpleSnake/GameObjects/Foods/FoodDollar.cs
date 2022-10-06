using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnake.GameObjects.Foods
{
   public class FoodDollar : Food
   {
       private const char FoodSymbol = '$';
       private const int FoodPoints = 2;
       private const ConsoleColor FoodColor = ConsoleColor.Green;

        public FoodDollar(Field field)
           : base(field, FoodPoints, FoodSymbol, FoodColor)
       {
       }
    }
}
