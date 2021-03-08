using OfflineSyncDemo.Contracts.Services.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace OfflineSyncDemo.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Members

        protected readonly IConnectionService _connectionService;
        protected readonly INavigationService _navigationService;
        protected readonly IDialogService _dialogService;

        #endregion

        #region Properties

        private bool _hasLoadedData;
        public bool HasLoadedData
        {
            get { return _hasLoadedData; }
            set
            {
                if (_hasLoadedData != value)
                {
                    _hasLoadedData = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Commands

        private Command _onRetrieveCommand;
        public virtual ICommand OnRetrieveCommand
        {
            get
            {
                if (_onRetrieveCommand == null)
                {
                    _onRetrieveCommand = new Command(async () =>
                    {
                        try
                        {
                            await Retrieve();
                        }
                        catch (Exception ex)
                        {
                            // AppTracker.Report(ex);
                            // await Dialogs.ShowAlertAsync("ERROR", ex.Message, "OK");
                        }
                    });
                }

                return _onRetrieveCommand;
            }
        }

        #endregion

        #region Constructor

        // public BaseViewModel(IConnectionService connectionService, INavigationService navigationService, IDialogService dialogService)
        public BaseViewModel(INavigationService navigationService)
        {
            // _connectionService = connectionService;
            _navigationService = navigationService;
            // _dialogService = dialogService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Base class to fetch the data.
        /// Assumption is that in the derived classes, this method will be overwritten and implement the data retrieval.
        /// </summary>
        protected virtual Task Retrieve()
        {
            return Task.Delay(0);
        }

        public virtual Task InitializeAsync(object data)
        {
            return Task.FromResult(false);
        }

        #endregion

        #region Test Command

        //public ICommand ButtonClickedCommand { get; set; }

        //public BaseViewModel()
        //{
        //    ButtonClickedCommand = new Command(OnButtonClicked);
        //}

        //private void OnButtonClicked()
        //{
        //    string WelcomeText = "Hello Bound Command!!";
        //}

        #endregion

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
		/// Raises the property changed event.
		/// </summary>
		/// <param name="name">Name of property.</param>
		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetValue<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
                return;
            backingField = value;
            OnPropertyChanged(propertyName);
        }

        #endregion
    }
}