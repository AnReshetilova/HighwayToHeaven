using Highway_to_heaven.Commands;
using Highway_to_heaven.Models;
using Highway_to_heaven.Services;
using Highway_to_heaven.ViewModels.Base;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Highway_to_heaven.ViewModels
{
    class AddTourViewModel : ViewModel
    {
        private PackageTour packageTour;
        private readonly TravelService travelService;
        private ObservableCollection<Planet> planets;
        private string tourName = "";
        private string planetName = "";
        private string discription = "";
        private string secondImage = @"D:\вуз\4 сем\OOP_4_sem\Highway_to_heaven\View\Windows\resources\icons\planet.png";
        private string firstImage = @"D:\вуз\4 сем\OOP_4_sem\Highway_to_heaven\View\Windows\resources\icons\planet.png";
        private string info;

        public ObservableCollection<Planet> Planets
        {
            get => planets;
        }
        public string Info
        {
            get => info;
            set => Set(ref info, value);
        }
        public string SecondImage
        {
            get => secondImage;
            set
            {
                if (value.Equals(""))
                {
                    secondImage = @"D:\вуз\4 сем\OOP_4_sem\Highway_to_heaven\View\Windows\resources\icons\planet.png";
                }
                else
                {
                    Set(ref secondImage, value);
                }
            }
        }
        public string FirstImage
        {
            get => firstImage;
            set
            {
                if (value.Equals(""))
                {
                    firstImage = @"D:\вуз\4 сем\OOP_4_sem\Highway_to_heaven\View\Windows\resources\icons\planet.png";
                }
                else
                {
                    Set(ref firstImage, value);
                }
            }
        }
        public string TourName
        {
            get => tourName;
            set => Set(ref tourName, value);
        }
        public string PlanetName
        {
            get => planetName;
            set
            {
                if (planets.FirstOrDefault(t => t.Name.Equals(value)) != null)
                {
                    Set(ref planetName, value);
                    Info = "";
                }
                else 
                {
                    Set(ref planetName, value);
                    Info = "Такой планеты нет";
                }
            }
        }
        public string Description
        {
            get => discription;
            set => Set(ref discription, value);
        }
        public ICommand AddSecondPictureCommand{ get; }
        public ICommand AddFirstPictureCommand { get; }
        public ICommand AddCommand { get; }
        public AddTourViewModel(TravelService travelService)
        {
            this.travelService = travelService;
            AddSecondPictureCommand = new ExternalCommand(OnAddSecondPictureCommand);
            AddFirstPictureCommand = new ExternalCommand(OnAddFirstPictureCommand);
            AddCommand = new ExternalCommand(OnAddCommand);
            planets = new ObservableCollection<Planet>(travelService.GetPlanets());
        }

        private void OnAddSecondPictureCommand(object o)
        {
            FileDialog fileDialog = AddPicture();
            SecondImage = fileDialog.FileName;
        }
        private void OnAddFirstPictureCommand(object o)
        {
            FileDialog fileDialog = AddPicture();
            FirstImage = fileDialog.FileName;
        }

        private void OnAddCommand(object o)
        {
            packageTour = new PackageTour();
            packageTour.PlanetName = planetName;
            packageTour.TourName = tourName;
            packageTour.Description = discription;
            packageTour.Picture = firstImage;
            packageTour.SecondPicture = secondImage;

            if (planetName.Equals("") || tourName.Equals("") || discription.Equals(""))
            {
                Info = "Не все поля заполнены";
            }           
            else
            {
                if (planets.FirstOrDefault(t => t.Name.Equals(planetName)) != null)
                {
                    travelService.AddNewTour(packageTour);
                    Info = "Путешествие добавлено";
                }
                else
                {
                    Info = "Такой планеты нет";
                }
            }
        }
    }
}
