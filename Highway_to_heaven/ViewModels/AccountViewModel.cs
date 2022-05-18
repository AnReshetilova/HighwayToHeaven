using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Highway_to_heaven.Models;
using Highway_to_heaven.Services;
using Highway_to_heaven.ViewModels.Base;

namespace Highway_to_heaven.ViewModels
{
    class AccountViewModel : ViewModel
    {
        const string NAME = "NAME: ";
        const string AGE = "AGE: ";
        const string ADDRESS = "ADDRESS: ";
        const string SCORE = "SCORE: ";

        private string name;
        private string age;
        private string address;
        private string picture;
        private string score;

        User userInfo;

        public string Score
        {
            get => score;
            set => Set(ref score, SCORE + value);
        }
        public string Name
        {
            get => name;
            set
            {
                Set(ref name, NAME + value);
            }
        }

        public string Age
        {
            get => age;
            set
            {
                Set(ref age, AGE + value);
            }
        }

        public string Address
        {
            get => address;
            set
            {
                Set(ref address, ADDRESS + value);
            }
        }

        public string Picture
        {
            get => picture;
            set
            {
                Set(ref picture, value);
            }
        }

        public AccountViewModel (Func<User> SetUser, UserService userService)
        {
            userInfo = SetUser();

            Name = userInfo.Name;
            Age = userInfo.Age.ToString();
            Address = userInfo.Address;
            Picture = userInfo.Picture;
            Score = userService.GetUserScore(userInfo.Login).ToString();
        }
    }
}
