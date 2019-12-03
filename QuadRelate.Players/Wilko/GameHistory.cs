using QuadRelate.Models;
using QuadRelate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuadRelate.Players.Wilko
{
    class GameHistory
    {
        public List<KeyValuePair<int, Move>> Moves = new List<KeyValuePair<int, Move>>();
        public Outcome Result = Outcome.Undecided;

        public GameHistory(List<int> moves, Counter firstPlayer)
        {
            var player = firstPlayer;
            foreach(var move in moves)
            {
                AddMove(move, firstPlayer, Outcome.Undecided);
                player = player.Invert();
            }
        }

        public void AddMove(int column, Counter player, Outcome result)
        {
            var newHash = GenerateHashForPosition(Moves);

            var nextMove = new Move
            {
                Column = column,
                Counter = player
            };

            newHash = newHash ^ nextMove.GetHashCode();
            Moves.Add(new KeyValuePair<int, Move>(newHash, nextMove));
            Result = result;
        }

        private int GenerateHashForPosition(List<KeyValuePair<int, Move>> game)
        {
            int newHash = 0;
            foreach (var kvp in game)
            {
                newHash = newHash ^ kvp.Value.GetHashCode();
            }

            return newHash;
        }

        public bool DoesGameContainPosition(List<KeyValuePair<int, Move>> currentPosition)
        {
            var item = Moves.FirstOrDefault(m => m.Key == currentPosition.Last().Key);
            return item.Key == currentPosition.Last().Key;
        }
    }
}
