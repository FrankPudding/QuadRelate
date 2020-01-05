using System.Collections.Generic;

namespace QuadRelate.Players.Vince.Helpers
{

    /// <summary>
    /// Each cell's score is equal to how many four-in-a-rows it is part of.
    ///  So the corner cells all equal 3, one horizontal, one vertical and one diagonal.
    /// </summary>
    internal static class CellEvaluator
    {
        private static readonly IDictionary<Cell, int> _cellValues = new Dictionary<Cell, int>(new CellComparer());

        static CellEvaluator()
        {
            _cellValues[new Cell(0, 0)] = 3;
            _cellValues[new Cell(1, 0)] = 4;
            _cellValues[new Cell(2, 0)] = 5;
            _cellValues[new Cell(3, 0)] = 7;
            _cellValues[new Cell(4, 0)] = 5;
            _cellValues[new Cell(5, 0)] = 4;
            _cellValues[new Cell(6, 0)] = 3;
            _cellValues[new Cell(0, 1)] = 4;
            _cellValues[new Cell(1, 1)] = 6;
            _cellValues[new Cell(2, 1)] = 8;
            _cellValues[new Cell(3, 1)] = 10;
            _cellValues[new Cell(4, 1)] = 8;
            _cellValues[new Cell(5, 1)] = 6;
            _cellValues[new Cell(6, 1)] = 4;
            _cellValues[new Cell(0, 2)] = 5;
            _cellValues[new Cell(1, 2)] = 8;
            _cellValues[new Cell(2, 2)] = 11;
            _cellValues[new Cell(3, 2)] = 13;
            _cellValues[new Cell(4, 2)] = 11;
            _cellValues[new Cell(5, 2)] = 8;
            _cellValues[new Cell(6, 2)] = 5;
            _cellValues[new Cell(0, 3)] = 5;
            _cellValues[new Cell(1, 3)] = 8;
            _cellValues[new Cell(2, 3)] = 11;
            _cellValues[new Cell(3, 3)] = 13;
            _cellValues[new Cell(4, 3)] = 11;
            _cellValues[new Cell(5, 3)] = 8;
            _cellValues[new Cell(6, 3)] = 5;
            _cellValues[new Cell(0, 4)] = 4;
            _cellValues[new Cell(1, 4)] = 6;
            _cellValues[new Cell(2, 4)] = 8;
            _cellValues[new Cell(3, 4)] = 10;
            _cellValues[new Cell(4, 4)] = 8;
            _cellValues[new Cell(5, 4)] = 6;
            _cellValues[new Cell(6, 4)] = 4;
            _cellValues[new Cell(0, 5)] = 3;
            _cellValues[new Cell(1, 5)] = 4;
            _cellValues[new Cell(2, 5)] = 5;
            _cellValues[new Cell(3, 5)] = 7;
            _cellValues[new Cell(4, 5)] = 5;
            _cellValues[new Cell(5, 5)] = 4;
            _cellValues[new Cell(6, 5)] = 3;
        }

        public static int GetValue(Cell cell)
        {
            return _cellValues[new Cell(cell.X, cell.Y)];
        }
    }

    internal class CellComparer : EqualityComparer<Cell>
    {
        public override bool Equals(Cell cell1, Cell cell2)
        {
            if (cell1 == null || cell2 == null)
                return false;

            return cell1.X == cell2.X && cell1.Y == cell2.Y;
        }

        public override int GetHashCode(Cell cell)
        {
            return cell.X.GetHashCode() + cell.Y.GetHashCode();
        }
    }
}