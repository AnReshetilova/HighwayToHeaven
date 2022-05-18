using Highway_to_heaven.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Highway_to_heaven.Models
{
    class CommentTemplate
    {
        private readonly User user;
        private readonly Comment comment;
        private readonly ObservableCollection<CommentRating> commentRating;

        public string Username { get; set; }
        public DateTime? Date { get; set; }
        public string Discription {get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public string Picture { get; set; }
        public int Id { get; set; }

        public CommentTemplate(Comment comment, UserService userService)
        {
            user = comment.IdTravelerNavigation;
            commentRating = new ObservableCollection<CommentRating>(userService.FindRatingByIdComment(comment.IdComment));

            Id = comment.IdComment;
            Username = user.Name;
            Picture = user.Picture;
            Date = comment.DateComment;
            Discription = comment.TextComment;
            Likes = commentRating.Count(t => t.Liked == true);
            Dislikes = commentRating.Count(t => t.Disliked == true);
        }
    }
}
