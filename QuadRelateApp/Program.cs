using QuadRelate.Externals;
using QuadRelate.Types;
using System;

namespace QuadRelateApp
{
    class Program
    {
        static void Main()
        {
            var board = new Board();
            BoardDrawerConsole boardDrawer = new BoardDrawerConsole();

            for (var y = 0; y < Board.Height; y++)
            {
                for (var x = 0; x < Board.Width; x++)
                {
                    board.PlaceCounter(x, Cell.Red);
                }
            }

            boardDrawer.DrawBoard(board);

            Console.ReadKey();
        }
    }
}