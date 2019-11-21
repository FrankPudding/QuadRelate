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
            var factory = new PlayerFactory();
            var playerOne = factory.CreatePlayer(nameof(CPUPlayerRandom));
            var playerTwo = factory.CreatePlayer(nameof(CPUPlayerRandom));

            board.Fill(Cell.Empty);
            boardDrawer.DrawBoard(board);

            for (var i = 0; i < 21; i++)
            {
                Console.ReadKey();

                var move = playerOne.NextMove(board.Clone(), Cell.Yellow);
                board.PlaceCounter(move, Cell.Yellow);
                boardDrawer.DrawBoard(board);

                if (board.IsGameOver())
                {
                    var message = board.DoesWinnerExist() ? "YELLOW WINS!" : "DRAW!";
                    Console.WriteLine(message);

                    break;
                }

                Console.ReadKey();

                move = playerTwo.NextMove(board.Clone(), Cell.Red);
                board.PlaceCounter(move, Cell.Red);
                boardDrawer.DrawBoard(board);

                if (board.IsGameOver())
                {
                    var message = board.DoesWinnerExist() ? "RED WINS!" : "DRAW!";
                    Console.WriteLine(message);

                    break;
                }
            }

            Console.ReadKey();
        }
    }
}