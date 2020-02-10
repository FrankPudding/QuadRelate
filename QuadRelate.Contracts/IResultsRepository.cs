using System.Collections.Generic;
using System.Threading.Tasks;
using QuadRelate.Types;

namespace QuadRelate.Contracts
{
    public interface IResultsRepository
    {
        Task SaveGameAsync(Board board, Counter winner);

        Task<IEnumerable<BoardResult>> GetPreviousResultsAsync();

        Task CloseAsync();
    }
}