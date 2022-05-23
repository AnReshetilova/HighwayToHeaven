using Highway_to_heaven.Commands;
using Highway_to_heaven.Models;
using Highway_to_heaven.Services;
using Highway_to_heaven.Stores;
using Highway_to_heaven.ViewModels.Base;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Highway_to_heaven.ViewModels
{
    class RegistrationViewModel : ViewModel
    {
        private string login = "";
        private string password = "";
        private string username = "";
        private string address ="";
        private string age = "";
        private string avatar = @"D:\вуз\4 сем\OOP_4_sem\Highway_to_heaven\View\Windows\resources\icons\astronaut.jpg";
        private readonly UserService userService;
        private User user;
        private string info;

        public string Info
        {
            get => info;
            set => Set(ref info, value);
        }
        public string Login
        {
            get => login;
            set => Set(ref login, value);
        }
        public string Password
        {
            get => password;
            set => Set(ref password, value);
        }
        public string Username
        {
            get => username;
            set => Set(ref username, value);
        }
        public string Address
        {
            get => address;
            set => Set(ref address, value);
        }
        public string Age
        {
            get => age;
            set
            {
                if ((!int.TryParse(value, out int temp) && !value.Equals("")) || Convert.ToInt32(value) > 100)
                {
                    Info = "Неверный формат возраста";
                }
                else
                {
                    Set(ref age, value);
                    Info = ""; 
                }
            }
        }
        public string Avatar
        {
            get => avatar;
            set
            {
                if (value.Equals(""))
                {
                    avatar = @"D:\вуз\4 сем\OOP_4_sem\Highway_to_heaven\View\Windows\resources\icons\astronaut.jpg";
                }
                else
                {
                    Set(ref avatar, value);
                }
            }
        }

        public ICommand RegistrationCommand { get; }
        public ICommand AddAvatarCommand { get; }
        private ICommand openLoginViewModel { get; }
        public RegistrationViewModel(UserService userService, NavigationStore navigationStore, Func<ViewModel> createLoginViewModel)
        {
            this.userService = userService;
            RegistrationCommand = new ExternalCommand(OnRegistrationCommand);
            AddAvatarCommand = new ExternalCommand(OnAddAvatarCommand);
            openLoginViewModel = new NavigateCommand(navigationStore, createLoginViewModel);
        }
        public void OnAddAvatarCommand(object o)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.TIF, *.PNG, *.ICO, *.EMF, *.WMF)|*.bmp;*.jpg;*.gif; *.tif; *.png; *.ico; *.emf; *.wmf";
            fileDialog.ShowDialog();
            Avatar = fileDialog.FileName;
        }
        public void OnRegistrationCommand(object o)
        {
            MD5 passwordHasher = MD5.Create();
            user = new User();
            user.Password = Convert.ToBase64String(passwordHasher.ComputeHash(Encoding.UTF8.GetBytes(password)));
            user.Login = login;
            user.Name = username;
            user.Address = address;
            user.Age = age;
            user.Picture = avatar;
            user.IsAdmin = false;


            if (login.Equals("") || password.Equals("") || username.Equals("") || address.Equals("") || age.Equals(""))
            {
                Info = "Не все поля заполнены";
            }
            else if (!userService.AddNewUser(user))
            {
                Info = "Пользователь с таким логином уже существует";
            }
            else
            {
                Info = "Регистрация пошла успешно";
                openLoginViewModel.Execute(o);
            }   
        }
    }
}
