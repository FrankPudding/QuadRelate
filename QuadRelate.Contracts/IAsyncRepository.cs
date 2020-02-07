﻿using System.Threading.Tasks;
using QuadRelate.Types;

namespace QuadRelate.Contracts
{
    public interface IAsyncRepository
    {
        Task SaveGameAsync(Board board, Counter winner);

        Task CloseAsync();
    }
}