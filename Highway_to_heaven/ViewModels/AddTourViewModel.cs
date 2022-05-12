using Highway_to_heaven.Commands;
using Highway_to_heaven.Models;
using Highway_to_heaven.Services;
using Highway_to_heaven.ViewModels.Base;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
        private string tourName;
        private string planetName;
        private string numberOFDays;
        private string discription;
        private string raiting;
        private string picturePath;
        private string secondImage = @"D:\вуз\4 сем\OOP_4_sem\Highway_to_heaven\View\Windows\resources\icons\planet.png";
        private string firstImage = @"D:\вуз\4 сем\OOP_4_sem\Highway_to_heaven\View\Windows\resources\icons\planet.png";

        public string SecondImage
        {
            get => secondImage;
            set => Set(ref secondImage, value);
        }
        public string FirstImage
        {
            get => firstImage;
            set => Set(ref firstImage, value);
        }
        public string TourName
        {
            get => tourName;
            set => Set(ref tourName, value);
        }
        public string PicturePath
        {
            get => picturePath;
            set => Set(ref picturePath, value);
        }
        public string PlanetName
        {
            get => planetName;
            set => Set(ref planetName, value);
        }
        public string NumberOFDays
        {
            get => numberOFDays;
            set => Set(ref numberOFDays, value);
        }
        public string Discription
        {
            get => discription;
            set => Set(ref discription, value);
        }
        public string Raiting
        {
            get => raiting;
            set => Set(ref raiting, value);
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
        }

        private void OnAddSecondPictureCommand(object o)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.TIF, *.PNG, *.ICO, *.EMF, *.WMF)|*.bmp;*.jpg;*.gif; *.tif; *.png; *.ico; *.emf; *.wmf";
            fileDialog.ShowDialog();
            SecondImage = fileDialog.FileName;
        }
        private void OnAddFirstPictureCommand(object o)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.TIF, *.PNG, *.ICO, *.EMF, *.WMF)|*.bmp;*.jpg;*.gif; *.tif; *.png; *.ico; *.emf; *.wmf";
            fileDialog.ShowDialog();
            FirstImage = fileDialog.FileName;
        }

        private void OnAddCommand(object o)
        {
            packageTour = new PackageTour();
            packageTour.NumberOfDays = Convert.ToInt32(numberOFDays);
            packageTour.PlanetName = planetName;
            packageTour.Rating = (float)Convert.ToDouble(raiting);
            packageTour.TourName = tourName;
            packageTour.Description = discription;
            packageTour.Picture = firstImage;
            packageTour.SecondPicture = secondImage;
            travelService.AddNewTour(packageTour);
        }
    }
}
