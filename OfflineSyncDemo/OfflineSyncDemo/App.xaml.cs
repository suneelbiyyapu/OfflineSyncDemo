using OfflineSyncDemo.Bootstrap;
using OfflineSyncDemo.Contracts.Services.General;
using OfflineSyncDemo.LocalDatabase;
using OfflineSyncDemo.ViewModels;
using OfflineSyncDemo.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OfflineSyncDemo
{
    public partial class App : Application
    {
        static StudentDatabase database;

        // Create the database connection as a singleton.
        public static StudentDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new StudentDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Students.db3"));
                }
                return database;
            }
        }


        public App()
        {
            InitializeComponent();

            InitializeApp();

            InitializeNavigation();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void InitializeApp()
        {
            AppContainer.RegisterDependencies();
        }

        private async void InitializeNavigation()
        {
            var navigationService = AppContainer.Resolve<INavigationService>();
            await navigationService.InitializeAsync();
        }
    }
}
