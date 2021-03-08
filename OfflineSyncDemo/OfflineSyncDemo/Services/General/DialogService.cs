using OfflineSyncDemo.Contracts.Services.General;
using System.Threading.Tasks;

namespace OfflineSyncDemo.Services.General
{
    public class DialogService : IDialogService
    {
        public Task ShowDialog(string message, string title, string buttonLabel)
        {
            return null; // UserDialogs.Instance.AlertAsync(message, title, buttonLabel);
        }

        public void ShowToast(string message)
        {
            // UserDialogs.Instance.Toast(message);
        }
    }
}
