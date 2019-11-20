using QuadRelate.Contracts;
using System;

namespace QuadRelate.Models
{
    public class PlayerFactory : IPlayerFactory
    { 
        public IPlayer CreatePlayer(string playerType)
        {
            if (playerType == nameof(CPUPlayerRandom))
            {
                return new CPUPlayerRandom();
            }

            throw new ArgumentOutOfRangeException(nameof(playerType), "That player does not exist");
        }
    }
}
