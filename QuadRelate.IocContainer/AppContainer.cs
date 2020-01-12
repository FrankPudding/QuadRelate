using Autofac;
using QuadRelate.Contracts;
using QuadRelate.Externals;
using QuadRelate.Factories;
using QuadRelate.Models;

namespace QuadRelate.IocContainer
{
    public static class AppContainer
    {
        private static readonly IContainer _container;

        static AppContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterTypes();

            _container = builder.Build();
        }

        private static void RegisterTypes(this ContainerBuilder builder)
        {
            // Player Factory
            builder.RegisterType<PlayerFactory>().As<IPlayerFactory>();

            // Models
            builder.RegisterType<GamePlayer>().As<IGamePlayer>();
            builder.RegisterType<Serializer>().As<ISerializer>();

            // Externals
            builder.RegisterType<BoardDrawerConsole>().As<IBoardDrawer>();
            builder.RegisterType<Randomizer>().As<IRandomizer>();
            builder.RegisterType<MessageWriterConsole>().As<IMessageWriter>();
            builder.RegisterType<GameRepository>().As<IGameRepository>();
            builder.RegisterType<FileReaderWriter>().As<IFileReaderWriter>();
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
