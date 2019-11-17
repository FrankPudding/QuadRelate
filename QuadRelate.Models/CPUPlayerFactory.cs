using QuadRelate.Contracts;
using QuadRelate.Types;
using System;

namespace QuadRelate.Models
{
    public class CPUPlayerFactory : ICPUPlayerFactory
    { 
        public ICPUPlayer CreateCPUPlayer(string typeName)
        {
            ICPUPlayer CPUPlayer;

            if (typeName == nameof(CPUPlayerRandom))
            {
                CPUPlayer = new CPUPlayerRandom();

                return CPUPlayer;
            }

            throw new ArgumentException(nameof(typeName));
        }
    }
}
