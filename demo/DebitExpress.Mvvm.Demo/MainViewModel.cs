using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace DebitExpress.Mvvm.Demo
{
    public interface IMainViewModel : IValidatableBindableBase
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Birthday { get; set; }
        string Result { get; set; }

        DelegateCommand ComputeAgeCommand { get; set; }
        DelegateAsyncCommand ComputeAgeAsyncCommand { get; set; }
    }

    public class MainViewModel : ValidatableBindableBase, IMainViewModel
    {
        private string _firstName;
        private string _lastName;
        private string _birthday;
        private string _result;

        public MainViewModel()
        {
            InitializeCommands();
            RegisterValidators();
            PropertyChanged += OnPropertyChanged;
        }

        public string FirstName
        {
            get => _firstName;
            //Use Set() method to notify changes.
            set => Set(ref _firstName, value);
        }

        public string LastName
        {
            get => _lastName;
            //Use Set() method to notify changes.
            set => Set(ref _lastName, value);
        }

        public string Birthday
        {
            get => _birthday;
            //Use Set() method to notify changes.
            set => Set(ref _birthday, value);
        }

        public string Result
        {
            get => _result;
            //Use Set() method to notify changes.
            set => Set(ref _result, value);
        }

        private void InitializeCommands()
        {
            ComputeAgeCommand = new DelegateCommand(OnComputeAge, CanComputeAge);
            ComputeAgeAsyncCommand = new DelegateAsyncCommand(OnComputeAgeAsync, CanComputeAge);
        }

        private bool CanComputeAge()
        {
            if (string.IsNullOrWhiteSpace(FirstName)) return false;
            if (string.IsNullOrWhiteSpace(LastName)) return false;
            if (string.IsNullOrWhiteSpace(Birthday)) return false;
            if (HasErrors) return false;

            return true;
        }

        public DelegateCommand ComputeAgeCommand { get; set; }

        private void OnComputeAge()
        {
            DateTime.TryParse(Birthday, out var birthday);

            var age = GetAge(birthday, DateTime.Today);
            Result = $"{FirstName} {LastName} is {age:n0} year/s old.";
        }

        public DelegateAsyncCommand ComputeAgeAsyncCommand { get; set; }

        //Using async command

        private async Task OnComputeAgeAsync()
        {
            DateTime.TryParse(Birthday, out var birthday);

            var age = await GetAgeAsync(birthday, DateTime.Today);
            Result = $"{FirstName} {LastName} is {age:n0} year/s old.";
        }

        private static Task<int> GetAgeAsync(DateTime birthDay, DateTime comparedDate)
        {
            return Task.Run(() => GetAgeWithSleep(birthDay, comparedDate));
        }

        //To emulate loading.
        private static int GetAgeWithSleep(DateTime birthDay, DateTime comparedDate)
        {
            Thread.Sleep(5000);
            return GetAge(birthDay, comparedDate);
        }

        private static int GetAge(DateTime birthDay, DateTime comparedDate)
        {
            var nowDays = comparedDate.Day;
            var nowMonth = comparedDate.Month;
            var nowYear = comparedDate.Year;

            var day = birthDay.Day;
            var month = birthDay.Month;
            var year = birthDay.Year;

            if (nowDays < day)
            {
                nowMonth--;
            }

            if (nowMonth < month)
            {
                nowYear--;
            }

            var age = nowYear - year;

            return age < 0 ? 0 : age;
        }

        private void RegisterValidators()
        {
            //Use to validate birthday property asynchronously.
            RegisterValidator(() => Birthday, ValidateBirthdayAsync);
        }

        private Task<List<string>> ValidateBirthdayAsync()
        {
            return Task.Run(() => ValidateBirthday());
        }

        private List<string> ValidateBirthday()
        {
            if (string.IsNullOrWhiteSpace(Birthday))
                return new List<string>();

            var parsed = DateTime.TryParse(Birthday, out _);

            return parsed ? new List<string>() : new List<string> { "Invalid date entered." };
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(Result))
                Result = string.Empty;

            ComputeAgeCommand.RaiseCanExecuteChanged();
            ComputeAgeAsyncCommand.RaiseCanExecuteChanged();
        }
    }
}