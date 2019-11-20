using QuadRelate.Models;
using System;
using Xunit;

namespace QuadRelate.Tests
{
    public class PlayerFactoryTests
    {
        private readonly PlayerFactory _playerFactory;

        public PlayerFactoryTests()
        {
            _playerFactory = new PlayerFactory();
        }
        
        [Fact]
        public void CreatePlayer_ForCPUPlayerRandom_ReturnsCPUPlayerRandom()
        {
            Assert.True(_playerFactory.CreatePlayer(nameof(CPUPlayerRandom)) is CPUPlayerRandom);
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
