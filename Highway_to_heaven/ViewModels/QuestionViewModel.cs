using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Highway_to_heaven.Commands;
using Highway_to_heaven.Models;
using Highway_to_heaven.Services;
using Highway_to_heaven.ViewModels.Base;

namespace Highway_to_heaven.ViewModels
{
    class QuestionViewModel : ViewModel
    {
        private const int MAX_QUESTION_COUNT = 4;

        private string question;
        private ObservableCollection<Question> questions;
        private ObservableCollection<Answer> answers;
        private int questionNumber = 0;
        private readonly TravelService travelService;
        private readonly PackageTour packageTour;
        private Answer checkedAnswer;
        private User user;
        private int score = 0;
        private string nextQuestion = "Visible";
        private string result = "Collapsed";
        private string journey = "Visible";
        private string journeyResult = "Collapsed";

        public string JourneyResult
        {
            get => journeyResult;
            set => Set(ref journeyResult, value);
        }
        public string Journey
        {
            get => journey;
            set => Set(ref journey, value);
        }
        public int Score
        {
            get => score;
            set => Set(ref score, value);
        }
        public string NextQuestion
        {
            get => nextQuestion;
            set => Set(ref nextQuestion, value);
        }
        public string Result
        {
            get => result;
            set => Set(ref result, value);
        }
        public string Question
        {
            get => question;
            set => Set(ref question, value);
        }
        public ObservableCollection<Question> Questions
        {
            get => questions;
        }
        
        public ObservableCollection<Answer> Answers
        {
            get => answers;
            set => Set(ref answers, value);
        }

        public ICommand NextCommand { get; }
        public ICommand ResultCommand { get; }
        public ICommand CheckedCommand { get; }

        public QuestionViewModel(TravelService travelService, User user, PackageTour packageTour)
        {
            this.travelService = travelService;
            this.user = user;
            this.packageTour = packageTour;

            questions = new ObservableCollection<Question>(travelService.GetQuestionsByTourId(packageTour.IdTour));
            NextCommand = new ExternalCommand(onNextCommand);
            CheckedCommand = new ExternalCommand(onCheckedCommand);
            ResultCommand = new ExternalCommand(onResultCommand);

            question = questions[questionNumber].Question1;
            answers = new ObservableCollection<Answer>(travelService.GetAnswersBuQuestionId(questions[questionNumber].IdQuestion));
        }

        private void onNextCommand(object o)
        {
            if (checkedAnswer.IdAnswer == answers.FirstOrDefault(t => t.IsCorrect == true).IdAnswer)
            {
                score++;
            }
            questionNumber++;
            Question = Questions[questionNumber].Question1;
            Answers = new ObservableCollection<Answer>(travelService.GetAnswersBuQuestionId(Questions[questionNumber].IdQuestion));

            if (questionNumber >= MAX_QUESTION_COUNT)
            {
                NextQuestion = "Collapsed";
                Result = "Visible";
            }
        }

        private void onCheckedCommand(object o)
        {
            checkedAnswer = o as Answer;
        }

        private void onResultCommand(object o)
        {
            if (checkedAnswer.IdAnswer == answers.FirstOrDefault(t => t.IsCorrect == true).IdAnswer)
            {
                score++;
            }
            Journey = "Collapsed";
            JourneyResult = "Visible";

            Travel travel = new Travel();
            travel.IdTour = packageTour.IdTour;
            travel.IdTraveler = user.Login;
            travel.Score = score;
            travelService.AddNewTravel(travel);
        }
    }
}
