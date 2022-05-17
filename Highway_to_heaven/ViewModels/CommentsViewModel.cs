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
        private ObservableCollection<Comment> comments;
        private bool canLike = true;
        private string likes;
        private string dislikes;
        private readonly UserService userService;
        private readonly ObservableCollection<User> users;
        private readonly PackageTour packageTour;

        public bool CanLike
        {
            get => canLike;
            set => Set(ref canLike, value);
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
        public ObservableCollection<Comment> Comments
        {
            get => comments;
        }

        public ICommand LikeCommand { get; }

        public CommentsViewModel(UserService userService, PackageTour packageTour)
        {
            comments = new ObservableCollection<Comment>(userService.GetCommentsCollectionByTourId(packageTour.IdTour));
            users = new ObservableCollection<User>(userService.GetUserList());
            this.packageTour = packageTour;
            LikeCommand = new ExternalCommand(onLikeCommand);
            this.userService = userService;
        }

        private void onLikeCommand(object o)
        {
            userService.IncreaseCommentLikes((o as Comment).IdComment);
            comments = new ObservableCollection<Comment>(userService.GetCommentsCollectionByTourId(packageTour.IdTour));
            CanLike = false;
        }
    }
}
