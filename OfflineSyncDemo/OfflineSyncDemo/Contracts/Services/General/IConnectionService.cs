namespace OfflineSyncDemo.Contracts.Services.General
{
    public interface IConnectionService
    {
        bool IsConnected { get; }
        // event ConnectivityChangedEventHandler ConnectivityChanged;
    }
}
