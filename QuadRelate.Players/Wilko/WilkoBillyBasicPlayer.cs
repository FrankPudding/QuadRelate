using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using QuadRelate.Contracts;
using QuadRelate.Models;
using QuadRelate.Types;

namespace QuadRelate.Players.Wilko
{
    public class WilkoBillyBasicPlayer : IPlayer
    {
        private readonly Random _rnd = new Random();
        private bool _firstSpace;
        public string Name => "WilkoBillyBasic";

        public int NextMove(Board board, Counter colour)
        {
            var tempBoard = board.Clone();
            var opponentCounter = OpponentCounter(colour);

            return PlayMoves(tempBoard, colour, opponentCounter);
        }

        private static Counter OpponentCounter(Counter currentPlayer)
        {
            switch (currentPlayer)
            {
                case Counter.Red:
                    return Counter.Yellow;
                case Counter.Yellow:
                    return Counter.Red;
                default:
                    throw new InvalidEnumArgumentException("Counter not recognised");
            }
        }

        private static void RemoveCounter(Board board, int col)
        {
            for (var y = Board.Height - 1; y >= 0; y--)
                if (board[col, y] != Counter.Empty)
                {
                    board[col, y] = Counter.Empty;
                    break;
                }
        }

        private int PlayMoves(Board tempBoard, Counter me, Counter opponent)
        {
            var avaliableCols = tempBoard.AvailableColumns();

            int move;
            // Check for winning move for me
            if ((move = CheckNextMoveWin(tempBoard, avaliableCols, me)) != -1)
                return move;

            // Check for opponent win
            if ((move = CheckNextMoveWin(tempBoard, avaliableCols, opponent)) != -1)
                return move;

            var stupidMoves = new Dictionary<int, int>();
            var possibleMoves = new Dictionary<int, int>();
            // Now check for stupid next move
            foreach (var col in avaliableCols)
            {
                tempBoard.PlaceCounter(col, me);
                var nextDepthCols = tempBoard.AvailableColumns();
                var nextMove = CheckNextMoveWin(tempBoard, nextDepthCols, opponent);
                if (nextMove != -1)
                    stupidMoves.Add(col, 1);
                RemoveCounter(tempBoard, col);
            }

            if (stupidMoves.Count > 0 && stupidMoves.Count == avaliableCols.Count)
                // I've lost regardless
                return avaliableCols[0];
            if (stupidMoves.Count > 0)
            {
                var items = avaliableCols.Where(c => stupidMoves.All(s => s.Key != c)).ToList();
                return items[_rnd.Next(items.Count)];
            }

            // Now check for possible future wins
            foreach (var col in avaliableCols)
            {
                tempBoard.PlaceCounter(col, me);
                var nextDepthCols = tempBoard.AvailableColumns();

                foreach (var nextCol in nextDepthCols)
                {
                    tempBoard.PlaceCounter(nextCol, opponent);
                    if (tempBoard.DoesWinnerExist()) throw new Exception("This shouldn't be");

                    if (CheckNextMoveWin(tempBoard, tempBoard.AvailableColumns(), me) != -1)
                    {
                        if (possibleMoves.ContainsKey(col))
                            possibleMoves[col]++;
                        else
                            possibleMoves.Add(col, 1);
                    }

                    foreach (var myNextCol in tempBoard.AvailableColumns())
                    {
                        tempBoard.PlaceCounter(myNextCol, me);
                        if (CheckNextMoveWin(tempBoard, tempBoard.AvailableColumns(), opponent) != -1)
                        {
                            if (stupidMoves.ContainsKey(col))
                                stupidMoves[col]++;
                            else
                                stupidMoves.Add(col, 1);
                        }

                        RemoveCounter(tempBoard, myNextCol);
                    }

                    RemoveCounter(tempBoard, nextCol);
                }

                RemoveCounter(tempBoard, col);
            }

            if (stupidMoves.Count == avaliableCols.Count)
                // All moves depth of 2 possible lose, so attempt to block by placing beside opponent.
//                var tuple = LookForTwoInARow(tempBoard, opponent);
//                if (tuple == null) return avaliableCols[_rnd.Next(avaliableCols.Count)];
//                if (tuple.Item1 < Board.Width) return tuple.Item1+1;
//                if (tuple.Item1 == Board.Width) return tuple.Item1 - 2;

                // Pick the least stupid
                return stupidMoves.First(sm => sm.Value == stupidMoves.Min(kvp => kvp.Value)).Key;

            //if (stupidMoves.Count == avaliableCols.Count) return avaliableCols[0];

            if (stupidMoves.Count > 0)
            {
                var items = avaliableCols.Where(c => stupidMoves.All(s => s.Key != c)).ToList();
                return items[_rnd.Next(items.Count)];
            }

            if (possibleMoves.Count > 0)
                return possibleMoves.First(k => k.Value == possibleMoves.Max(m => m.Value)).Key;

            if ((move = FindFirstSpace(tempBoard, me)) != -1)
                return move;

            return avaliableCols[_rnd.Next(avaliableCols.Count)];
        }


        private int CheckNextMoveWin(Board board, List<int> avaliableMoves, Counter colour)
        {
            foreach (var col in avaliableMoves)
            {
                board.PlaceCounter(col, colour);
                if (board.DoesWinnerExist())
                {
                    RemoveCounter(board, col);
                    return col;
                }

                RemoveCounter(board, col);
            }

            return -1;
        }

        private int FindFirstSpace(Board board, Counter colour)
        {
            for (var x = 0; x < Board.Width; x++)
            for (var y = 0; y < Board.Height; y++)
            {
                if (board[x, y] != colour)
                    continue;

                // Up
                if (_firstSpace && y < Board.Height - 3 && board[x, y + 1] == Counter.Empty)
                {
                    _firstSpace = !_firstSpace;
                    return x;
                }

                // Right
                if (x < Board.Width - 2 && board[x + 1, y] == Counter.Empty)
                {
                    _firstSpace = !_firstSpace;
                    return x + 1;
                }

                // Left
                if (x > 0 && board[x - 1, y] == Counter.Empty) return x - 1;

                // Diagonal Right
                if (x < Board.Width - 1 && y < Board.Height - 1 && board[x + 1, y + 1] == Counter.Empty) return x + 1;

                // Diagonal Left
                if (x > 0 && y < Board.Height - 1 && board[x - 1, y + 1] == Counter.Empty) return x - 1;
            }

            return -1;
        }

        public void GameOver(GameResult result)
        {
            //Ignore
        }
    }
}