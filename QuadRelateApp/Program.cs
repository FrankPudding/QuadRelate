using QuadRelate.Externals;
using QuadRelate.Helpers;
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

            board.Fill(Cell.Red);

            boardDrawer.DrawBoard(board);

            Console.ReadKey();
        }
    }
}