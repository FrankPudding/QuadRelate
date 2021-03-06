﻿using QuadRelate.Contracts;
using QuadRelate.Models;
using QuadRelate.Types;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace QuadRelate.Players.Rory
{
    public class CPUPlayer03 : IPlayer
    {
        private Counter _currentColour;
        private const int _middleColumn = 3;
        private const string _fileName = "GameList.json";

        public string Name => "Swag Master General";

        private readonly IPlayerInitializer _playerInitializer;

        public CPUPlayer03(IPlayerInitializer playerInitializer)
        {
            _playerInitializer = playerInitializer;
            var previousGames = _playerInitializer.GameRepository.LoadGames(_fileName);
        }

        public int NextMove(Board board, Counter colour)
        {
            _currentColour = colour;
            var availableColumns = board.AvailableColumns();

            // Play bottom middle if available
            if (board[_middleColumn, 0] == Counter.Empty)
            {
                return _middleColumn;
            }

            // Play only move available
            if (availableColumns.Count == 1)
                return availableColumns[0];

            Board boardClone;

            // Play winning move
            foreach (var move in availableColumns)
            {
                boardClone = board.Clone();
                boardClone.PlaceCounter(move, colour);

                if (boardClone.IsGameOver())
                    return move;
            }

            // Play blocking move
            foreach (var move in availableColumns)
            {
                boardClone = board.Clone();
                boardClone.PlaceCounter(move, colour.ReverseCounter());

                if (boardClone.DoesWinnerExist())
                    return move;
            }

            var expectedScores = board.ExpectedScores(colour);
            var expectedScoresMax = expectedScores.Values.Max();
            var bestMoves = new List<int>();

            foreach (var move in expectedScores.Keys)
            {
                if (expectedScores[move] == expectedScoresMax)
                    bestMoves.Add(move);
            }

            var bestMoveIndex = _playerInitializer.Randomizer.Next(bestMoves.Count());

            return bestMoves[bestMoveIndex]; 
        }

        public void GameOver(GameResult result)
        {
            _playerInitializer.GameRepository.SaveGame(result, _fileName);

            if (result.Winner == _currentColour.Invert())
            {
                //Debug.WriteLine(string.Join('.', result.Moves));
            }
        }
    }
}
