using OfflineSyncDemo.Constants;
using OfflineSyncDemo.Contracts.Services.General;
using OfflineSyncDemo.Contracts.Services.Repository;
using OfflineSyncDemo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace OfflineSyncDemo.ViewModels
{
    public class AddStudentsViewModel : BaseViewModel
    {
        private readonly IGenericRepository _genericRepository;

        #region Properties

        private string _name;
        public string Name
        {
            get => _name;
            set => SetValue(ref _name, value);
        }

        private string _age;
        public string Age
        {
            get => _age;
            set => SetValue(ref _age, value);
        }

        #endregion

        public ICommand AddStudent { get; set; }

        public AddStudentsViewModel(INavigationService navigationService,
            IGenericRepository genericRepository)
            : base(navigationService)
        {
            _genericRepository = genericRepository;
            AddStudent = new Command(OnAddButtonClicked);
        }

        private async void OnAddButtonClicked(object obj)
        {
            IsBusy = true;
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = $"{ApiConstants.StudentsEndpoint}"
            };

            var studentObj = Activator.CreateInstance(typeof(Student)) as Student;
            studentObj.StudentName = Name;
            studentObj.StudentAge = Convert.ToInt32(Age);
            studentObj.CreatedAt = DateTime.Now;
            studentObj.UpdatedAt = DateTime.Now;
            studentObj.CreatedBy = "Suneel";
            studentObj.ModifiedBy = "Suneel";

            var student = await _genericRepository.PostAsync<Student>(builder.ToString(), studentObj);
            IsBusy = false;
            if (student != null)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Student has been created successfully.", "Okay");
                Name = string.Empty;
                Age = string.Empty;
            }
        }
    }
}
