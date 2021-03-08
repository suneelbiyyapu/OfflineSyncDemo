using OfflineSyncDemo.Bootstrap;
using OfflineSyncDemo.Contracts.Services.General;
using OfflineSyncDemo.ViewModels;
using OfflineSyncDemo.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OfflineSyncDemo.Services.General
{
    public class NavigationService : INavigationService
    {
        // private readonly IAuthenticationService _authenticationService;
        private readonly Dictionary<Type, Type> _mappings;

        protected Application CurrentApplication => Application.Current;

        public NavigationService(/* IAuthenticationService authenticationService */)
        {
            // _authenticationService = authenticationService;
            _mappings = new Dictionary<Type, Type>();

            CreatePageViewModelMappings();
        }

        public async Task InitializeAsync()
        {
            //if (_authenticationService.IsUserAuthenticated())
            //{
            //    await NavigateToAsync<MainViewModel>();
            //}
            //else
            //{
            //    await NavigateToAsync<LoginViewModel>();
            //}
            await NavigateToAsync<StudentsListViewModel>();
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task NavigateToAsync(Type viewModelType)
        {
            return InternalNavigateToAsync(viewModelType, null);
        }

        public async Task ClearBackStack()
        {
            await CurrentApplication.MainPage.Navigation.PopToRootAsync();
        }

        public Task NavigateToAsync(Type viewModelType, object parameter)
        {
            return InternalNavigateToAsync(viewModelType, parameter);
        }

        public async Task NavigateBackAsync()
        {
            //if (CurrentApplication.MainPage is MainView mainPage)
            //{
            //    await mainPage.Detail.Navigation.PopAsync();
            //}
            //else 
            if (CurrentApplication.MainPage != null)
            {
                await CurrentApplication.MainPage.Navigation.PopAsync();
            }
        }

        public virtual Task RemoveLastFromBackStackAsync()
        {
            //if (CurrentApplication.MainPage is MainView mainPage)
            //{
            //    mainPage.Detail.Navigation.RemovePage(
            //        mainPage.Detail.Navigation.NavigationStack[mainPage.Detail.Navigation.NavigationStack.Count - 2]);
            //}

            return Task.FromResult(true);
        }

        public async Task PopToRootAsync()
        {
            //if (CurrentApplication.MainPage is MainView mainPage)
            //{
            //    await mainPage.Detail.Navigation.PopToRootAsync();
            //}
        }

        protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreateAndBindPage(viewModelType, parameter);
            page.Appearing += async (object sender, EventArgs e)=>
            {
                var viewModel = page.BindingContext as BaseViewModel;
                await viewModel.InitializeAsync(parameter);
                page.BindingContext = viewModel;
            };
            // page.Disappearing += Page_Disappearing;

            //if (page is MainView || page is RegistrationView)
            //{
            //    CurrentApplication.MainPage = page;
            //}
            //else if (page is LoginView)
            //{
            //    CurrentApplication.MainPage = page;
            //}
            if (page is StudentsListView)
            {
                var navPage = new CustomNavigationPage(page);
                NavigationPage.SetHasBackButton(navPage, false);
                NavigationPage.SetHasNavigationBar(navPage, false);
                CurrentApplication.MainPage = navPage;
            }
            //else if (CurrentApplication.MainPage is MainView)
            //{
            //    var mainPage = CurrentApplication.MainPage as MainView;

            //    if (mainPage.Detail is CustomNavigationPage navigationPage)
            //    {
            //        var currentPage = navigationPage.CurrentPage;

            //        if (currentPage.GetType() != page.GetType())
            //        {
            //            await navigationPage.PushAsync(page);
            //        }
            //    }
            //    else
            //    {
            //        navigationPage = new CustomNavigationPage(page);
            //        mainPage.Detail = navigationPage;
            //    }

            //    mainPage.IsPresented = false;
            //}
            else
            {
                var navigationPage = CurrentApplication.MainPage as CustomNavigationPage;

                if (navigationPage != null)
                {
                    NavigationPage.SetHasBackButton(page, true);
                    await navigationPage.PushAsync(page);
                }
                else
                {
                    CurrentApplication.MainPage = new CustomNavigationPage(page);
                }
            }

            await (page.BindingContext as BaseViewModel).InitializeAsync(parameter);
        }

        protected Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!_mappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
            }

            return _mappings[viewModelType];
        }

        protected Page CreateAndBindPage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new Exception($"Mapping type for {viewModelType} is not a page");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            BaseViewModel viewModel = AppContainer.Resolve(viewModelType) as BaseViewModel;
            page.BindingContext = viewModel;

            return page;
        }

        private void CreatePageViewModelMappings()
        {

            _mappings.Add(typeof(StudentsListViewModel), typeof(StudentsListView));
            _mappings.Add(typeof(AddStudentsViewModel), typeof(AddStudentsView));
        }
    }
}
