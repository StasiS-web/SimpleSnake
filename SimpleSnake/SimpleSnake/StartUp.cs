using System;
using SimpleSnake.Core;
using SimpleSnake.Core.Interfaces;
using SimpleSnake.GameObjects;

namespace SimpleSnake
{
    using Utilities;

    public class StartUp
    {
        public static void Main()
        {
            ConsoleWindow.CustomizeConsole();

            Field field = new Field(60, 20);
            Snake snake = new Snake(field);

            IEngine engine = new Engine(field, snake);
            engine.Run();
        }
    }
}
