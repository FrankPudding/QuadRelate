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
            var playerOne = factory.CreatePlayer(nameof(HumanPlayer));
            var playerTwo = factory.CreatePlayer(nameof(CPUPlayer03));

            messageWriter.WriteMessage($"'{playerOne.Name}' vs '{playerTwo.Name}'\n");

            //var score = gamePlayer.PlayMultipleGames(playerOne, playerTwo, 1000);
            var score = gamePlayer.PlayOneGame(playerOne, playerTwo) + gamePlayer.PlayOneGame(playerTwo, playerOne).ReverseScore();

            messageWriter.WriteMessage($"'{playerOne.Name}' {score.PlayerOne} : {score.PlayerTwo} '{playerTwo.Name}'");
        }
    }
}