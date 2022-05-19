using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Highway_to_heaven.Commands;
using Highway_to_heaven.Models;
using Highway_to_heaven.Services;
using Highway_to_heaven.Stores;
using Highway_to_heaven.ViewModels.Base;

namespace Highway_to_heaven.ViewModels
{
    class TourInfoViewModel : ViewModel
    {
        const string NAME = "НАЗВАНИЕ ТУРА:";
        const string PLANET = "ПЛАНЕТА: ";
        const string DAYS = "КОЛИЧЕСТВО ДНЕЙ: ";
        const string DISC = "ОПИСАНИЕ:\n";
        const string RAIT = "РЕЙТИНГ: ";

        private PackageTour packageTour;
        private string tourName;
        private string planetName;
        private string numberOFDays;
        private string discription;
        private string raiting;
        private string picturePath;
        private string isAllowed;

        public ICommand OpenCommentsCommand { get; }
        private ICommand createCommentViewModelCommand { get; }
        public ICommand OpenQuestionCommand { get; }
        private ICommand createQuestionViewModelCommand { get; }

        public string IsAllowed
        {
            get => isAllowed;
            set => Set(ref isAllowed, value);
        }
        public string TourName
        {
            get => tourName;
            set => Set(ref tourName, NAME + value);
        }
        public string PicturePath
        {
            get => picturePath;
            set => Set(ref picturePath, value);
        }
        public string PlanetName
        {
            get => planetName;
            set => Set(ref planetName, PLANET + value);
        }
        public string NumberOFDays
        {
            get => numberOFDays;
            set => Set(ref numberOFDays, DAYS + value);
        }
        public string Discription
        {
            get => discription;
            set => Set(ref discription, DISC + value);
        }
        public string Raiting
        {
            get => raiting;
            set => Set(ref raiting, RAIT + value);
        }
        public TourInfoViewModel(PackageTour packageTour, NavigationStore navigationStore, Func<object, ViewModel> createCommentsViewModel, Func<object, ViewModel> createQuestionViewModel, TravelService travelService, User user)
        {
            this.packageTour = packageTour;
            TourName = packageTour.TourName;
            PlanetName = packageTour.PlanetName;
            NumberOFDays = packageTour.NumberOfDays.ToString();
            Discription = packageTour.Description;
            Raiting = packageTour.Rating.ToString();
            PicturePath = packageTour.SecondPicture;

            OpenCommentsCommand = new ExternalCommand(onOpenCommentsCommand);
            OpenQuestionCommand = new ExternalCommand(onOpenQuestionCommand);
            createCommentViewModelCommand = new NavigateCommand(navigationStore, createCommentsViewModel);
            createQuestionViewModelCommand = new NavigateCommand(navigationStore, createQuestionViewModel);

            if (travelService.GetTravelByUserTourId(user.Login, packageTour.IdTour) != null || travelService.GetQuestionsByTourId(packageTour.IdTour).Count() == 0)
            {
                IsAllowed = "Collapsed";
            }
            else
            {
                IsAllowed = "Visible";
            }
        }

        private void onOpenCommentsCommand(object o)
        {
            createCommentViewModelCommand.Execute(packageTour);
        }

        private void onOpenQuestionCommand(object o)
        {
            createQuestionViewModelCommand.Execute(packageTour);
        }
    }
}
