using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Highway_to_heaven.Models;
using Highway_to_heaven.Services;
using Highway_to_heaven.ViewModels.Base;

namespace Highway_to_heaven.ViewModels
{
    class StatisticsViewModel : ViewModel
    {
        private ObservableCollection<StatisticsTemplate> statisticsTemplates;
        private ObservableCollection<User> users;

        public ObservableCollection<StatisticsTemplate> StatisticsTemplates
        {
            get => statisticsTemplates;
        }

        public StatisticsViewModel(UserService userService)
        {
            users = new ObservableCollection<User>(userService.GetUserList());
            statisticsTemplates = new ObservableCollection<StatisticsTemplate>();

            foreach (var el in users)
            {
                statisticsTemplates.Add(new StatisticsTemplate(el, userService));
            }
        }
    }
}
