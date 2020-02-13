using QuadRelate.Contracts;
using QuadRelate.Players.Vince.Helpers;
using QuadRelate.Types;

namespace QuadRelate.Players.Vince
{
    public class CpuPlayerCellEvaluator : IPlayer
    {
        private readonly IPlayerInitializer _playerInitializer;

        public CpuPlayerCellEvaluator(IPlayerInitializer playerInitializer)
        {
            _playerInitializer = playerInitializer;
        }

        public string Name => "Splintered Cell";

        public int NextMove(Board board, Counter colour)
        {
            if (MovesHelper.TryGetBasicMove(board, colour, out var move))
                return move;

            var cells = CellsHelper.GetPlayableCells(board);
            cells = CellsHelper.GetHighestScoringCells(cells);

            return cells[_playerInitializer.Randomizer.Next(cells.Count)].X;
        }

        public void GameOver(GameResult result)
        {
        }
    }
}