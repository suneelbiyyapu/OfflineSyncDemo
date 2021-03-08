using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Essentials;

namespace OfflineSyncDemo.Services.General
{
    public class ConnectionService : INotifyPropertyChanged
    {
        public ConnectionService()
        {
            _isConnected = Connectivity.NetworkAccess == NetworkAccess.Internet;
            StopListening();
            StartListening();
        }

        public void StartListening()
        {
            // Register for connectivity changes
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        public void StopListening()
        {
            // Un-register listener for changes
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            var access = e.NetworkAccess;
            var profiles = e.ConnectionProfiles;
            if (access == NetworkAccess.Internet)
            {
                _isConnected = false;
            }
            else
            {
                _isConnected = true;
            }
        }
        public static bool IsInternetConnected { get; set; }

        private bool _isConnected;

        public bool IsConnected
        {
            get
            {
                IsInternetConnected = _isConnected;
                return _isConnected;
            }
            set
            {
                _isConnected = value;
                IsInternetConnected = _isConnected;
                OnPropertyChanged(nameof(IsConnected));
            }
        }

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
		/// Raises the property changed event.
		/// </summary>
		/// <param name="name">Name of property.</param>
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
