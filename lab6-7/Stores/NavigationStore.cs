using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab6_7.ViewModels.Base;

namespace lab6_7.Stores
{
    public class NavigationStore
    {
        ViewModel currentViewModel;
        public ViewModel CurrentViewModel
        {
            get => currentViewModel;
            set
            {
                currentViewModel = value;
                onCurrentViewModelChanged();
            }
        }

        public event Action CurrentViewModelChanged;

        private void onCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
