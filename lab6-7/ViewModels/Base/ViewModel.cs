using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace lab6_7.ViewModels.Base
{
    // INotifyPropertyChanged - для обновления информации об элементах

    public abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void InvokePropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        //обновление значений поля, для которого определено свойство
        protected virtual bool Set<T>(ref T field, T value, string PropertyName = null)
        {
            if (Equals(field, value))
            {
                return false;
            }

            field = value;
            InvokePropertyChanged(PropertyName);
            return true;
        }
    }
}
