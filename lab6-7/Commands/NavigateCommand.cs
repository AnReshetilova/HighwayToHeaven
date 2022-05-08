using lab6_7.Stores;
using lab6_7.ViewModels;
using lab6_7.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6_7.Commands
{
    class NavigateCommand : CommandBase
    {
        readonly NavigationStore navigationStore;
        readonly Func<ViewModel> createViewModel;

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
