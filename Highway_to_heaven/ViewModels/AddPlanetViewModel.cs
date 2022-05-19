using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Highway_to_heaven.Commands;
using Highway_to_heaven.Models;
using Highway_to_heaven.Services;
using Highway_to_heaven.ViewModels.Base;
using Microsoft.Win32;

namespace Highway_to_heaven.ViewModels
{
    class AddPlanetViewModel : ViewModel
    {
        private string name = "";
        private string planetarySystem = "";
        private string surfaceArea = "";
        private string siderealRotationPeriod = "";
        private string minTemp = "";
        private string maxTemp = "";
        private string description = "";
        private string picture = @"D:\вуз\4 сем\OOP_4_sem\Highway_to_heaven\View\Windows\resources\icons\planet.png";
        private readonly TravelService travelService;
        private string info;

        public string Info
        {
            get => info;
            set => Set(ref info, value);
        }
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }
        public string PlanetarySystem
        {
            get => planetarySystem;
            set => Set(ref planetarySystem, value);
        }
        public string SurfaceArea
        {
            get => surfaceArea;
            set
            {
                if (double.TryParse(value, out double temp) || value.Equals(""))
                {
                    Set(ref surfaceArea, value);
                    Info = "";
                }
                else
                {
                    Info = "Неверный формат площади";
                }
            }
        }
        public string SiderealRotationPeriod
        {
            get => siderealRotationPeriod;
            set => Set(ref siderealRotationPeriod, value);
        }
        public string MinTemp
        {
            get => minTemp;
            set
            {
                if (double.TryParse(value, out double temp) || value.Equals("") || value.Equals("-") || value.Equals("+"))
                {
                    Set(ref minTemp, value);
                    Info = "";
                }
                else
                {
                    Info = "Неверный формат температуры";
                }
            }
        }
        public string MaxTemp
        {
            get => maxTemp;
            set
            {
                if (double.TryParse(value, out double temp) || value.Equals("") || value.Equals("-") || value.Equals("+"))
                {
                    Set(ref maxTemp, value);
                    Info = "";
                }
                else
                {
                    Info = "Неверный формат температуры";
                }
            }
        }
        public string Description
        {
            get => description;
            set => Set(ref description, value);
        }
        public string Picture
        {
            get => picture;
            set
            {
                if (value.Equals(""))
                {
                    picture = @"D:\вуз\4 сем\OOP_4_sem\Highway_to_heaven\View\Windows\resources\icons\planet.png";
                }
                else
                {
                    Set(ref picture, value);
                }
            }
        }

        public ICommand AddCommand { get; }
        public ICommand AddPictureCommand { get; }

        public AddPlanetViewModel(TravelService travelService)
        {
            this.travelService = travelService;
            AddCommand = new ExternalCommand(onAddCommand);
            AddPictureCommand = new ExternalCommand(onAddPictureCommand);
        }

        private void onAddCommand(object o)
        {
           

            Planet planet = new Planet();
            planet.Name = name;
            planet.PlanetarySystem = planetarySystem;
            planet.SurfaceArea = surfaceArea;
            planet.SiderealRotationPeriod = siderealRotationPeriod;
            planet.MaxTemp = maxTemp;
            planet.MinTemp = minTemp;
            planet.Description = description;
            planet.Picture = picture;

            if (name.Equals("") || planetarySystem.Equals("") || surfaceArea.Equals("")
               || siderealRotationPeriod.Equals("") || maxTemp.Equals("") || minTemp.Equals("") || description.Equals(""))
            {
                Info = "Пожалуйста, заполните все поля";
            }
            else if (Convert.ToDouble(minTemp) > Convert.ToDouble(maxTemp))
            {
                Info = "Минимальная температура больше максимальной";
            }
            else if (!travelService.AddNewPlanet(planet))
            {
                Info = "Такая планета уже существует";
            }
            else
            {
                Info = "Планета добавлена";
            }
        }
        private void onAddPictureCommand(object o)
        {
            FileDialog fileDialog = AddPicture();
            Picture = fileDialog.FileName;
        }
    }
}
