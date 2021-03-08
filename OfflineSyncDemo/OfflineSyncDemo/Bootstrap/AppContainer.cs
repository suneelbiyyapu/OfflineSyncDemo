using Autofac;
using OfflineSyncDemo.Contracts.Services.General;
using OfflineSyncDemo.Contracts.Services.Repository;
using OfflineSyncDemo.Repository;
using OfflineSyncDemo.Services.General;
using OfflineSyncDemo.ViewModels;
using System;

namespace OfflineSyncDemo.Bootstrap
{
    public class AppContainer
    {
        private static IContainer _container;

        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            //ViewModels
            builder.RegisterType<StudentsListViewModel>();
            builder.RegisterType<AddStudentsViewModel>();
            //builder.RegisterType<ShoppingCartViewModel>().SingleInstance();

            //services - data
            //builder.RegisterType<CatalogDataService>().As<ICatalogDataService>();
            //builder.RegisterType<ShoppingCartDataService>().As<IShoppingCartDataService>();

            //services - general
            // builder.RegisterType<ConnectionService>().As<IConnectionService>();
            builder.RegisterType<NavigationService>().As<INavigationService>();
            builder.RegisterType<DialogService>().As<IDialogService>();
            builder.RegisterType<SettingsService>().As<ISettingsService>().SingleInstance();

            //General
            builder.RegisterType<GenericRepository>().As<IGenericRepository>();

            _container = builder.Build();
        }


        public static object Resolve(Type typeName)
        {
            try
            {
                return _container.Resolve(typeName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
