using QuadRelate.Types;

namespace QuadRelate.Contracts
{
    public interface IGamePlayer
    {
        Score PlayOneGame(IPlayer playerOne, IPlayer playerTwo);

        Score PlayMultipleGames(IPlayer playerOne, IPlayer playerTwo, int numberOfGames);
    }
}