using QuadRelate.Externals;
using QuadRelate.Helpers;
using QuadRelate.Models;
using QuadRelate.Types;
using System;

namespace QuadRelateApp
{
    class Program
    {
        static void Main()
        {
            var board = new Board();
            var boardDrawer = new BoardDrawerConsole();
            var player = new CPUPlayerRandom();

            board.Fill(Cell.Empty);
            boardDrawer.DrawBoard(board);

            for (var i = 0; i < 21; i++)
            {
                Console.ReadKey();

                board.PlaceCounter(player.NextMove(board), Cell.Yellow);
                boardDrawer.DrawBoard(board);

                if (board.IsGameOver())
                {
                    Console.WriteLine("\nYELLOW WINS!");
                    break;
                }

                Console.ReadKey();

                board.PlaceCounter(player.NextMove(board), Cell.Red);
                boardDrawer.DrawBoard(board);

                if (board.IsGameOver())
                {
                    Console.WriteLine("\nRED WINS!");
                    break;
                }
            }

            Console.ReadKey();
        }
    }
}