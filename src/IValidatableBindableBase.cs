using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace DebitExpress.Mvvm
{
    public interface IValidatableBindableBase
    {
        bool IsValidating { get; }
        bool IsValid { get; }
        bool HasErrors { get; }
        FocusTrigger FocusTrigger { get; }
        void ClearErrors();
        event PropertyChangedEventHandler PropertyChanged;
        List<string> GetErrors();
        event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        IEnumerable GetErrors(string propertyName);
        Task ValidateAllAsync();
    }
}
