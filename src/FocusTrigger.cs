using System.ComponentModel;
using System.Runtime.CompilerServices;
using DebitExpress.Mvvm.Annotations;

namespace DebitExpress.Mvvm
{
    /// <inheritdoc />
    /// <summary>
    /// Notify focus to bounded UIElement. For internal use of ValidatableBindableBase.
    /// </summary>
    public class FocusTrigger : INotifyPropertyChanged
    {
        private bool _focus;

        public bool Focus
        {
            get => _focus;
            private set
            {
                _focus = value;
                OnPropertyChanged(nameof(Focus));
            }
        }

        internal void Notify()
        {
            Focus = !Focus;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
