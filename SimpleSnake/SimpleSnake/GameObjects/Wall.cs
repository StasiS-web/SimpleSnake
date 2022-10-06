using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnake.GameObjects
{
    public class Wall : Point
    {
        private const char WallSymbol = '\u25A0';
        private int playerPoints;

        public Wall(int leftX, int topY)
            : base(leftX, topY)
        {
            this.InitializeWallBorder();
            this.PlayerInfo();
        }

        public bool IsPointOfWall(Point snakeHead)
        {
            return snakeHead.LeftX == 0 || snakeHead.LeftX == this.LeftX - 1 ||
                   snakeHead.TopY == 0 || snakeHead.TopY == this.TopY;
        }

        private void InitializeWallBorder()
        {
            this.SetHorizontalLine(0); // Top border
            this.SetHorizontalLine(this.TopY); //Bottom border
            this.SetVerticalLine(0);  // Left border
            this.SetVerticalLine(this.LeftX - 1);  //
        }

        private void SetHorizontalLine(int topY)
        {
            for (int leftX = 0; leftX < this.LeftX; leftX++)
            {
                this.Draw(leftX, topY, WallSymbol);
            }
        }

        private void SetVerticalLine(int leftX)
        {
            for (int topY = 0; topY < this.TopY; topY++)
            {
                this.Draw(leftX, topY, WallSymbol);
            }
        }

        public void AddPoints(Queue<Point> snakeElements)
        {
            this.playerPoints = snakeElements.Count - 6;
        }

        public void PlayerInfo()
        {
            Console.SetCursorPosition(this.LeftX + 3, 0);
            Console.Write($"Player points: {this.playerPoints}");

            Console.SetCursorPosition(this.LeftX + 3, 1);
            Console.Write($"Player level: {this.playerPoints / 10}");
        }
    }
}
