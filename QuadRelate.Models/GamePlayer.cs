﻿using QuadRelate.Contracts;
using QuadRelate.Types;
using System;
using System.Collections.Generic;

namespace QuadRelate.Models
{
    public class GamePlayer : IGamePlayer
    {
        private readonly IBoardDrawer _boardDrawer;
        private readonly IMessageWriter _messageWriter;

        public GamePlayer(IBoardDrawer boardDrawer, IMessageWriter messageWriter)
        {
            _boardDrawer = boardDrawer;
            _messageWriter = messageWriter;
        }

        public Score PlayOneGame(IPlayer playerOne, IPlayer playerTwo)
        {
            var board = new Board();
            var score = new Score();
            var moves = new List<int>();

            board.Fill(Counter.Empty);
            _boardDrawer.DrawBoard(board);
            _messageWriter.WriteMessage($"'{playerOne.Name}' vs '{playerTwo.Name}'");

            while (!board.IsGameOver())
            {
                var move = playerOne.NextMove(board.Clone(), Counter.Yellow);
                moves.Add(move);
                board.PlaceCounter(move, Counter.Yellow);
                _boardDrawer.DrawBoard(board);

                if (board.IsGameOver())
                {
                    var message = $"YELLOW WINS! ('{playerOne.Name}')";
                    _messageWriter.WriteMessage(message);

                    score.PlayerOne = 1;
                    var result = new GameResult(Counter.Yellow, moves);
                    playerOne.GameOver(result);
                    playerTwo.GameOver(result);

                    return score;
                }

                move = playerTwo.NextMove(board.Clone(), Counter.Red);
                moves.Add(move);
                board.PlaceCounter(move, Counter.Red);
                _boardDrawer.DrawBoard(board);

                if (board.IsGameOver())
                {
                    var isWin = board.DoesWinnerExist();

                    var message = isWin ? $"RED WINS! ('{playerTwo.Name}')" : "DRAW!";
                    _messageWriter.WriteMessage(message);

                    if (isWin)
                    {
                        score.PlayerTwo = 1;
                        var result = new GameResult(Counter.Red, moves);
                        playerOne.GameOver(result);
                        playerTwo.GameOver(result);
                    }
                    else
                    {
                        score.PlayerOne = 0.5f;
                        score.PlayerTwo = 0.5f;
                        var result = new GameResult(Counter.Empty, moves);
                        playerOne.GameOver(result);
                        playerTwo.GameOver(result);
                    }

                    return score;
                }
            }

            throw new InvalidOperationException("Game not handled");
        }

        public Score PlayMultipleGames(IPlayer playerOne, IPlayer playerTwo, int numberOfGames)
        {
            var totalScore = new Score();
            var yellowPlayer = playerOne;
            var redPlayer = playerTwo;

            for (var i = 0; i < numberOfGames; i++)
            {
                var gameScore = PlayOneGame(yellowPlayer, redPlayer);

                if (i % 2 == 0)
                {
                    totalScore.PlayerOne += gameScore.PlayerOne;
                    totalScore.PlayerTwo += gameScore.PlayerTwo;
                }
                else
                {
                    totalScore.PlayerOne += gameScore.PlayerTwo;
                    totalScore.PlayerTwo += gameScore.PlayerOne;
                }

                var tempPlayer = yellowPlayer;
                yellowPlayer = redPlayer;
                redPlayer = tempPlayer;
            }

            return totalScore;
        }
    }
}