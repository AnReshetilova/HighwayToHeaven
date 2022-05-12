using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Highway_to_heaven.ViewModels.Base;

namespace Highway_to_heaven.Stores
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
