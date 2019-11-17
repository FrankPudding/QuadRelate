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
            var factory = new CPUPlayerFactory();
            var playerOne = factory.CreateCPUPlayer(nameof(CPUPlayerRandom));
            var playerTwo = factory.CreateCPUPlayer(nameof(CPUPlayerRandom));

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
                    if(board.DoesWinnerExist())
                    {
                        Console.WriteLine("\nYELLOW WINS!");
                    }
                    else
                    {
                        Console.WriteLine("\nDRAW!");
                    }
                    
                    break;
                }

                Console.ReadKey();

                move = playerTwo.NextMove(board.Clone(), Cell.Red);
                board.PlaceCounter(move, Cell.Red);
                boardDrawer.DrawBoard(board);

                if (board.IsGameOver())
                {
                    if (board.DoesWinnerExist())
                    {
                        Console.WriteLine("\nRED WINS!");
                    }
                    else
                    {
                        Console.WriteLine("\nDRAW!");
                    }

                    break;
                }
            }

            Console.ReadKey();
        }
    }
}