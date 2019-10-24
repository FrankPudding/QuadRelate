using QuadRelate.Contracts;
using QuadRelate.Helpers;
using QuadRelate.Types;
using System;

namespace CPUPlayers
{
    public class CPUPlayerRandom : ICPUPlayer
    {
        public int NextMove(Board board)
        {
            var random = new Random();

            var index = random.Next(board.AvailableColumns().Count);

            return board.AvailableColumns()[index];
        }
    }
}
