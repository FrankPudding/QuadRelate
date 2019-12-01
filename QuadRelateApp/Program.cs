using QuadRelate.Types;
using System;
using QuadRelate.Models;
using QuadRelate.Players.Rory;
using QuadRelate.Players.Vince;
using QuadRelate.IocContainer;
using QuadRelate.Contracts;

namespace QuadRelateApp
{
    class Program
    {
        static void Main()
        {
            var board = new Board();
            var boardDrawer = AppContainer.Resolve<IBoardDrawer>();
            var factory = AppContainer.Resolve<IPlayerFactory>();
            var playerOne = factory.CreatePlayer(nameof(HumanPlayer));
            var playerTwo = factory.CreatePlayer(nameof(CpuPlayerVince));

            board.Fill(Counter.Empty);
            boardDrawer.DrawBoard(board);
            Console.WriteLine($"'{playerOne.Name}' vs '{playerTwo.Name}'");

            for (var i = 0; i < 21; i++)
            {
                var move = playerOne.NextMove(board.Clone(), Counter.Yellow);
                board.PlaceCounter(move, Counter.Yellow);
                boardDrawer.DrawBoard(board);

                if (board.IsGameOver())
                {
                    var message = board.DoesWinnerExist() ? $"YELLOW WINS! ('{playerOne.Name}')" : "DRAW!";
                    Console.WriteLine(message);

                    break;
                }


                move = playerTwo.NextMove(board.Clone(), Counter.Red);
                board.PlaceCounter(move, Counter.Red);
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