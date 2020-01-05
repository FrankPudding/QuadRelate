using QuadRelate.Contracts;
using QuadRelate.Players.Vince.Helpers;
using QuadRelate.Types;

namespace QuadRelate.Players.Vince
{
    public class CpuPlayerCellEvaluator : IPlayer
    {
        private readonly IRandomizer _randomizer;

        public CpuPlayerCellEvaluator(IRandomizer randomizer)
        {
            _randomizer = randomizer;
        }

        public string Name => "Splintered Cell";

        public int NextMove(Board board, Counter colour)
        {
            if (MovesHelper.TryGetBasicMove(board, colour, out var move))
                return move;

            var cells = CellsHelper.GetPlayableCells(board);
            cells = CellsHelper.GetHighestScoringCells(cells);

            return cells[_randomizer.Next(cells.Count)].X;
        }

        public void GameOver(GameResult result)
        {
        }
    }
}