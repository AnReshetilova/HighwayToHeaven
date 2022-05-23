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
        private const int MAX_QUESTION_COUNT = 4;
        private int counter = 0;

        private PackageTour packageTour;
        private readonly TravelService travelService;
        private ObservableCollection<Planet> planets;
        private string tourName = "";
        private string planetName = "";
        private string discription = "";
        private int tourId;
        private string question = "";
        private string correctAnswer ="";
        private string firstAnswer ="";
        private string secondAnswer ="";
        private string thirdAnswer ="";
        private string secondImage = @"D:\вуз\4 сем\OOP_4_sem\Highway_to_heaven\View\Windows\resources\icons\planet.png";
        private string firstImage = @"D:\вуз\4 сем\OOP_4_sem\Highway_to_heaven\View\Windows\resources\icons\planet.png";
        private string info;
        private ObservableCollection<Question> questions;
        private string canAddMoreQuestions = "Visible";
        private string lastQuestion = "Collapsed";
        private string addQuestion = "Collapsed";
        private string addTour = "Visible";

        public string AddTour
        {
            get => addTour;
            set => Set(ref addTour, value);
        }
        public string AddQuestion
        {
            get => addQuestion;
            set => Set(ref addQuestion, value);
        }
        public string LastQuestion
        {
            get => lastQuestion;
            set => Set(ref lastQuestion, value);
        }
        public string CanAddMoreQuestions
        {
            get => canAddMoreQuestions;
            set => Set(ref canAddMoreQuestions, value);
        }
        public string ThirdAnswer
        {
            get => thirdAnswer;
            set => Set(ref thirdAnswer, value);
        }
        public string SecondAnswer
        {
            get => secondAnswer;
            set => Set(ref secondAnswer, value);
        }
        public string FirstAnswer
        {
            get => firstAnswer;
            set => Set(ref firstAnswer, value);
        }
        public string CorrectAnswer
        {
            get => correctAnswer;
            set => Set(ref correctAnswer, value);
        }
        public string Question
        {
            get => question;
            set => Set(ref question, value);
        }
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
        public ICommand AddQuestionCommand { get; }
        public ICommand LastQuestionCommand { get; }
        public AddTourViewModel(TravelService travelService)
        {
            this.travelService = travelService;
            AddSecondPictureCommand = new ExternalCommand(OnAddSecondPictureCommand);
            AddFirstPictureCommand = new ExternalCommand(OnAddFirstPictureCommand);
            AddQuestionCommand = new ExternalCommand(OnAddQuestionCommand);
            LastQuestionCommand = new ExternalCommand(OnLastQuestionCommand);
            AddCommand = new ExternalCommand(OnAddCommand);

            planets = new ObservableCollection<Planet>(travelService.GetPlanets());
            questions = new ObservableCollection<Question>();
        }

        private void OnLastQuestionCommand(object o)
        {
            if (question.Equals("") || correctAnswer.Equals("") || firstAnswer.Equals("") || secondAnswer.Equals("") || thirdAnswer.Equals(""))
            {
                Info = "Заполните все поля";
            }
            else
            {
                Question tempQuestion = new Question();
                tempQuestion.IdTour = tourId;
                tempQuestion.Question1 = question;
                tempQuestion.Answers.Add(new Answer() { Answer1 = correctAnswer, IsCorrect = true});
                tempQuestion.Answers.Add(new Answer() { Answer1 = firstAnswer, IsCorrect = false });
                tempQuestion.Answers.Add(new Answer() { Answer1 = secondAnswer, IsCorrect = false });
                tempQuestion.Answers.Add(new Answer() { Answer1 = thirdAnswer, IsCorrect = false });

                questions.Add(tempQuestion);

                foreach (var el in questions)
                {
                    travelService.AddNewQuestion(el);
                }

                Info = "Добавление путешествия завершено";
                LastQuestion = "Collapsed";
            }
        }

        private void OnAddQuestionCommand(object o)
        {
            if (question.Equals("") || correctAnswer.Equals("") || firstAnswer.Equals("") || secondAnswer.Equals("") || thirdAnswer.Equals(""))
            {
                Info = "Заполните все поля";
            }
            else
            {
                Question tempQuestion = new Question();
                tempQuestion.IdTour = tourId;
                tempQuestion.Question1 = question;
                tempQuestion.Answers.Add(new Answer() { Answer1 = correctAnswer });
                tempQuestion.Answers.Add(new Answer() { Answer1 = firstAnswer });
                tempQuestion.Answers.Add(new Answer() { Answer1 = secondAnswer });
                tempQuestion.Answers.Add(new Answer() { Answer1 = thirdAnswer });

                questions.Add(tempQuestion);
                Info = "Вопрос добавлен";
                counter++;

                if (counter >= MAX_QUESTION_COUNT)
                {
                    CanAddMoreQuestions = "Collapsed";
                    LastQuestion = "Visible";
                }
                CorrectAnswer = "";
                FirstAnswer = "";
                SecondAnswer = "";
                ThirdAnswer = "";
                Question = "";
            }
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
                    tourId = travelService.AddNewTour(packageTour);
                    Info = "Путешествие добавлено";
                    AddQuestion = "Visible";
                    AddTour = "Collapsed";

                }
                else
                {
                    Info = "Такой планеты нет";
                }
            }
        }
    }
}
