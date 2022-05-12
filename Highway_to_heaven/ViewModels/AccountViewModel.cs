using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Highway_to_heaven.Models;
using Highway_to_heaven.ViewModels.Base;

namespace Highway_to_heaven.ViewModels
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
