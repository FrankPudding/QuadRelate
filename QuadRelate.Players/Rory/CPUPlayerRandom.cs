using System;
using QuadRelate.Contracts;
using QuadRelate.Helpers;
using QuadRelate.Types;

namespace QuadRelate.Players.Rory
{
    public class CpuPlayerRandom : IPlayer
    {
        private readonly Random _randomNumber;

        public CpuPlayerRandom()
        {
            _randomNumber = new Random();
        }

        public string Name => "The Randomizer";

        public int NextMove(Board board, Cell colour)
        {
            var index = _randomNumber.Next(board.AvailableColumns().Count);

            return board.AvailableColumns()[index];
        }
    }
}