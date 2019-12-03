using QuadRelate.Types;
using QuadRelate.Models;
using QuadRelate.Players.Rory;
using QuadRelate.Players.Vince;
using QuadRelate.IocContainer;
using QuadRelate.Contracts;
using System;

namespace QuadRelateApp
{
    class Program
    {
        static void Main()
        {
            var messageWriter = AppContainer.Resolve<IMessageWriter>();
            var gamePlayer = AppContainer.Resolve<IGamePlayer>();
            var factory = AppContainer.Resolve<IPlayerFactory>();
            var playerOne = factory.CreatePlayer(nameof(CpuPlayer02));
            var playerTwo = factory.CreatePlayer(nameof(CpuPlayerRandom));

            var score = gamePlayer.PlayMultipleGames(playerOne, playerTwo, 10);
            //var score = gamePlayer.PlayOneGame(playerOne, playerTwo);

            messageWriter.WriteMessage($"{playerOne.GetType().Name} {score.PlayerOne} : {score.PlayerTwo} {playerTwo.GetType().Name}");

            Console.ReadLine();
        }
    }
}