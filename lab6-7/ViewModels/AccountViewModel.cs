using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab6_7.Models;
using lab6_7.ViewModels.Base;

namespace lab6_7.ViewModels
{
    class AccountViewModel : ViewModel
    {
        private string name;
        private string age;
        private string nowNew;
        User userInfo;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                InvokePropertyChanged(nameof(Name));
            }
        }

        public string Age
        {
            get => age;
            set
            {
                age = value;
                InvokePropertyChanged(nameof(age));
            }
        }

        public AccountViewModel (Func<User> SetUser)
        {
            userInfo = SetUser();
            Name = userInfo.Name;
        }
    }
}
