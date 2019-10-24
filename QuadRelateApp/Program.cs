using CPUPlayers;
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
            var boardDrawer = new BoardDrawerConsole();
            var player = new CPUPlayerRandom();

            board.Fill(Cell.Empty);

            boardDrawer.DrawBoard(board);

            Console.ReadKey();

            board.PlaceCounter(player.NextMove(board), Cell.Red);

            for (var i = 0; i < 21; i++)
            {
                boardDrawer.DrawBoard(board);

                Console.ReadKey();

                board.PlaceCounter(player.NextMove(board), Cell.Yellow);

                boardDrawer.DrawBoard(board);

                Console.ReadKey();

                board.PlaceCounter(player.NextMove(board), Cell.Red);
            }
        }
    }
}