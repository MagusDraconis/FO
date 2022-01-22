using Autofac;
using FO.DataAccess;
using FO.UI.Data;
using FO.UI.ViewModel;
using Prism.Events;
using System;
using System.Linq;

namespace FO.UI.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();
            // dbContext
            builder.RegisterType<FoDbContext>().AsSelf();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<FriendDetailViewModel>().As<IFriendDetailViewModel>();
            builder.RegisterType<FriendDataService>().As<IFriendDataService>();
            builder.RegisterType<LookupDataService>().AsImplementedInterfaces();



            // finally
            return builder.Build();
        }

    }
}
