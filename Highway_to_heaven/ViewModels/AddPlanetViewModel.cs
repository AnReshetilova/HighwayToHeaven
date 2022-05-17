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
        private string name;
        private string planetarySystem;
        private string surfaceArea;
        private string siderealRotationPeriod;
        private string minTemp;
        private string maxTemp;
        private string description;
        private string picture = @"D:\вуз\4 сем\OOP_4_sem\Highway_to_heaven\View\Windows\resources\icons\planet.png";
        private readonly TravelService travelService;

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
            set => Set(ref surfaceArea, value);
        }
        public string SiderealRotationPeriod
        {
            get => siderealRotationPeriod;
            set => Set(ref siderealRotationPeriod, value);
        }
        public string MinTemp
        {
            get => minTemp;
            set => Set(ref minTemp, value);
        }
        public string MaxTemp
        {
            get => maxTemp;
            set => Set(ref maxTemp, value);
        }
        public string Description
        {
            get => description;
            set => Set(ref description, value);
        }
        public string Picture
        {
            get => picture;
            set => Set(ref picture, value);
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
            planet.SurfaceArea = Convert.ToDouble(surfaceArea);
            planet.SiderealRotationPeriod = siderealRotationPeriod;
            planet.MaxTemp = Convert.ToDouble(maxTemp);
            planet.MinTemp = Convert.ToDouble(minTemp);
            planet.Description = description;
            planet.Picture = picture;

            travelService.AddNewPlanet(planet);
        }
        private void onAddPictureCommand(object o)
        {
            FileDialog fileDialog = AddPicture();
            Picture = fileDialog.FileName;
        }
    }
}
