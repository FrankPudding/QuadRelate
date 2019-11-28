using Autofac;
using QuadRelate.Contracts;
using QuadRelate.Externals;
using QuadRelate.Factories;

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

            // Externals
            builder.RegisterType<BoardDrawerConsole>().As<IBoardDrawer>();
            builder.RegisterType<Randomizer>().As<IRandomizer>();
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
