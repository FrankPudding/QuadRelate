using System;
using Moq;
using QuadRelate.Contracts;
using QuadRelate.Factories;
using QuadRelate.Players.Rory;
using QuadRelate.Players.Vince;
using Xunit;

namespace QuadRelate.Tests
{
    public class PlayerFactoryTests
    {
        private readonly PlayerFactory _playerFactory;

        public PlayerFactoryTests()
        {
            var randomizer = new Mock<IRandomizer>();
            var gameRepository = new Mock<IGameRepository>();
            _playerFactory = new PlayerFactory(randomizer.Object, gameRepository.Object);
        }
        
        [Theory]
        [InlineData(typeof(CpuPlayerRandom))]
        [InlineData(typeof(CpuPlayerVince))]
        [InlineData(typeof(CpuPlayerLefty))]
        [InlineData(typeof(HumanPlayer))]
        public void CreatePlayer_ForValidPlayerClassNames_ReturnsObjectsOfCorrectType(Type type)
        {
            var p = _playerFactory.CreatePlayer(type.Name);

            Assert.IsAssignableFrom(type, p);
        }

        [Fact]
        public void CreatePlayer_ForInvalidArgument_ThrowsException()
        {
            var expected = Assert.Throws<ArgumentOutOfRangeException>(() => _playerFactory.CreatePlayer(""));

            Assert.Equal("playerType", expected.ParamName);
            Assert.Equal("That player does not exist\r\nParameter name: playerType", expected.Message);
        }
    }
}
