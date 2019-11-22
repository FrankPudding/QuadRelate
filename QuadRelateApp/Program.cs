using QuadRelate.Externals;
using QuadRelate.Types;
using System;
using QuadRelate.Factories;
using QuadRelate.Models;
using QuadRelate.Players.Rory;
using QuadRelate.Players.Vince;

namespace QuadRelateApp
{
    class Program
    {
        static void Main()
        {
            var board = new Board();
            var boardDrawer = new BoardDrawerConsole();
            var randomizer = new Randomizer();
            var factory = new PlayerFactory(randomizer);
            var playerOne = factory.CreatePlayer(nameof(HumanPlayer));
            var playerTwo = factory.CreatePlayer(nameof(CpuPlayerBasic));

            board.Fill(Cell.Empty);
            boardDrawer.DrawBoard(board);
            Console.WriteLine($"'{playerOne.Name}' vs '{playerTwo.Name}'");

            for (var i = 0; i < 21; i++)
            {


                var move = playerOne.NextMove(board.Clone(), Cell.Yellow);
                board.PlaceCounter(move, Cell.Yellow);
                boardDrawer.DrawBoard(board);

                if (board.IsGameOver())
                {
                    var message = board.DoesWinnerExist() ? $"YELLOW WINS! ('{playerOne.Name}')" : "DRAW!";
                    Console.WriteLine(message);

                    break;
                }


                move = playerTwo.NextMove(board.Clone(), Cell.Red);
                board.PlaceCounter(move, Cell.Red);
                boardDrawer.DrawBoard(board);

                if (board.IsGameOver())
                {
                    var message = board.DoesWinnerExist() ? $"RED WINS! ('{playerTwo.Name}')" : "DRAW!";
                    Console.WriteLine(message);

                    break;
                }
            }

            Console.ReadKey();
        }
    }
}