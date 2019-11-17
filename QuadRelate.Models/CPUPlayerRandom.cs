using QuadRelate.Contracts;
using QuadRelate.Helpers;
using QuadRelate.Types;
using System;

namespace QuadRelate.Models
{
    public class CPUPlayerRandom : ICPUPlayer
    {
        private readonly Random _randomNumber;

        public CPUPlayerRandom()
        {
            _randomNumber = new Random();
        }

        public int NextMove(Board board, Cell colour)
        {
            var index = _randomNumber.Next(board.AvailableColumns().Count);

            return board.AvailableColumns()[index];
        }
    }
}
