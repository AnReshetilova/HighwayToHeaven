using Highway_to_heaven.Stores;
using Highway_to_heaven.ViewModels;
using Highway_to_heaven.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Highway_to_heaven.Commands
{
    class NavigateCommand : CommandBase
    {
        private readonly NavigationStore navigationStore;
        private readonly Func<ViewModel> createViewModel;

        public NavigateCommand(NavigationStore navigationStore, Func<ViewModel> createViewModel)
        {
            this.navigationStore = navigationStore;
            this.createViewModel = createViewModel;
        }

        public override void Execute(object parameter)
        {
            navigationStore.CurrentViewModel = createViewModel();
        }
    }
}
