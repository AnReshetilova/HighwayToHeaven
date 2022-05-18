using Highway_to_heaven.Models;
using Highway_to_heaven.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Highway_to_heaven.ViewModels.Base;
using System.Windows.Input;
using Highway_to_heaven.Commands;
using System.Windows;

namespace Highway_to_heaven.ViewModels
{
    class CommentsViewModel : ViewModel
    {
        private ObservableCollection<CommentTemplate> commentsTemplates;
        private string likes;
        private string dislikes;
        private string currentComment;
        private string isLiked = "0,0,0,0";
        private string isDisliked = "0,0,0,0";
        private readonly UserService userService;
        private readonly ObservableCollection<User> users;
        private readonly PackageTour packageTour;
        private User user;

        public string CurrentComment
        {
            get => currentComment;
            set => Set(ref currentComment, value);
        }
        public string Likes
        {
            get => likes;
            set => Set(ref likes, value);
        }
        public string Dislikes
        {
            get => dislikes;
            set => Set(ref dislikes, value);
        }
        public string IsLiked
        {
            get => isLiked;
            set => Set(ref isLiked, value);
        }
        public string IsDisliked
        {
            get => isDisliked;
            set => Set(ref isDisliked, value);
        }
        public ObservableCollection<CommentTemplate> Comments
        {
            get => commentsTemplates;
        }

        public ICommand LikeCommand { get; }
        public ICommand DislikeCommand { get; }
        public ICommand AddCommand { get; set; }

        public CommentsViewModel(UserService userService, PackageTour packageTour, User user)
        {
            users = new ObservableCollection<User>(userService.GetUserList());
            commentsTemplates = new ObservableCollection<CommentTemplate>();
            ObservableCollection<Comment> comments = new ObservableCollection<Comment>(userService.GetCommentsCollectionByTourId(packageTour.IdTour));
            foreach (var el in comments)
            {
                commentsTemplates.Add(new CommentTemplate(el, userService));  
            }

            this.user = user;
            this.packageTour = packageTour;
            LikeCommand = new ExternalCommand(onLikeCommand);
            DislikeCommand = new ExternalCommand(onDislikeCommand);
            AddCommand = new ExternalCommand(onAddCommand);
            this.userService = userService;
        }

        private void onAddCommand (object o)
        {
            Comment comment = new Comment();
            comment.DateComment = DateTime.Now;
            comment.IdTour = packageTour.IdTour;
            comment.IdTraveler = user.Login;
            comment.TextComment = currentComment;
            userService.AddComment(comment);
            Refresh();
        }
        private void onLikeCommand(object o)
        {
            Comment comment = userService.FindCommentById((o as CommentTemplate).Id);

            if (!userService.AddRaiting(comment, user, true, false))
            {
                MessageBox.Show("Вы уже оценили комментарий!");
            }
            else
            {
                Refresh();
            }
        }

        private void onDislikeCommand(object o)
        {
            Comment comment = userService.FindCommentById((o as CommentTemplate).Id);

            if (!userService.AddRaiting(comment, user, false, true))
            {
                MessageBox.Show("Вы уже оценили комментарий!");
            }
            else
            {
                Refresh();
            }
        }

        private void Refresh()
        {
            commentsTemplates.Clear();
            ObservableCollection<Comment> comments = new ObservableCollection<Comment>(userService.GetCommentsCollectionByTourId(packageTour.IdTour));
            foreach (var el in comments)
            {
                commentsTemplates.Add(new CommentTemplate(el, userService));
            }
        }
    }
}
