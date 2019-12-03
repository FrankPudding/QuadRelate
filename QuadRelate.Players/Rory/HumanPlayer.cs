using System;
using QuadRelate.Contracts;
using QuadRelate.Models;
using QuadRelate.Types;

namespace QuadRelate.Players.Rory
{
    public class HumanPlayer : IPlayer
    {
        public string Name => "Human";

        public int NextMove(Board board, Counter colour)
        {
            Console.WriteLine("Choose next move:\n");

            bool isValidInput;
            int inputInt;

            do
            {
                var input = Console.ReadLine();

                isValidInput = int.TryParse(input, out inputInt) && board.AvailableColumns().Contains(inputInt-1);
            } while (!isValidInput);

            return inputInt - 1;
        }

        public void GameOver(GameResult result)
        {
            // Ignore.
        }
    }
}