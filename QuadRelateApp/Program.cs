using QuadRelate.Types;
using QuadRelate.Models;
using QuadRelate.Players.Rory;
using QuadRelate.Players.Vince;
using QuadRelate.IocContainer;
using QuadRelate.Contracts;

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
            var playerTwo = factory.CreatePlayer(nameof(CpuPlayerVince));

            messageWriter.WriteMessage($"{playerOne.Name} vs {playerTwo.Name}");

            var score = gamePlayer.PlayMultipleGames(playerOne, playerTwo, 100);
            //var score = gamePlayer.PlayOneGame(playerOne, playerTwo);

            messageWriter.WriteMessage($"{playerOne.Name} {score.PlayerOne} : {score.PlayerTwo} {playerTwo.Name}");
        }
    }
}