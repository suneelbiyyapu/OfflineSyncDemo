﻿using OfflineSyncDemo.ViewModels;
using System;
using System.Threading.Tasks;

namespace OfflineSyncDemo.Contracts.Services.General
{
    public interface INavigationService
    {
        Task InitializeAsync();

        Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel;

        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel;

        Task NavigateToAsync(Type viewModelType);

        Task ClearBackStack();

        Task NavigateToAsync(Type viewModelType, object parameter);

        Task NavigateBackAsync();

        Task RemoveLastFromBackStackAsync();

        Task PopToRootAsync();
    }
}
