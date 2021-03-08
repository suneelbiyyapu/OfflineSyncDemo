using OfflineSyncDemo.Constants;
using OfflineSyncDemo.Contracts.Services.General;
using OfflineSyncDemo.Contracts.Services.Repository;
using OfflineSyncDemo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace OfflineSyncDemo.ViewModels
{
    public class StudentsListViewModel : BaseViewModel
    {
        private readonly IGenericRepository _genericRepository;

        #region Properties

        private ObservableCollection<Student> _students;
        public ObservableCollection<Student> Students
        {
            get => _students;
            set => SetValue(ref _students, value);
        }

        #endregion


        public ICommand AddStudentCommand { get; set; }

        public StudentsListViewModel(INavigationService navigationService,
            IGenericRepository genericRepository)
            : base(navigationService)
        {
            _genericRepository = genericRepository;
            AddStudentCommand = new Command(OnAddButtonClicked);
            // GetAllStudents();
        }

        private async void OnAddButtonClicked()
        {
            await _navigationService.NavigateToAsync<AddStudentsViewModel>();
        }

        public override async Task InitializeAsync(object data)
        {
            IsBusy = true;
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = $"{ApiConstants.StudentsEndpoint}"
            };
            Students = await _genericRepository.GetAsync<ObservableCollection<Student>>(builder.ToString());
            IsBusy = false;
        }
    }
}
