using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Highway_to_heaven.Models;

namespace Highway_to_heaven.Services
{
    public class UserService
    {
        private readonly HIGHWAY_TO_HEAVENContext context;

        public UserService(HIGHWAY_TO_HEAVENContext context)
        {
            this.context = context;
        }

        public User GetUserByLogin(string login)
        {
            return this.context.Users.FirstOrDefault(t => t.Login.Equals(login));
        }

        public bool AddNewUser(User user)
        {
            if (GetUserByLogin(user.Login) != null)
            {
                return false;
            }
            context.Users.Add(user);
            context.SaveChanges();
            return true;
        }

        public IEnumerable<Comment> GetCommentsCollectionByTourId(int id)
        {
            return this.context.Comments.Where(t => t.IdTour.Equals(id)).AsEnumerable();
        }

        public IEnumerable<User> GetUserList()
        {
            return context.Users.AsEnumerable();
        }

        public IEnumerable<CommentRating> FindRatingByIdComment(int id)
        {
            return context.CommentRatings.Where(t => t.IdComment == id);
        }

        public Comment FindCommentById(int id)
        {
            return context.Comments.FirstOrDefault(t => t.IdComment == id);
        }

        public bool AddRaiting(Comment comment, User user, bool liked, bool disliked)
        {
            if (context.CommentRatings.Any(t => (t.IdComment == comment.IdComment && t.IdUser == user.Login)))
            {
                return false;
            }

            CommentRating rating = new CommentRating();
            rating.IdComment = comment.IdComment;
            rating.IdUser = user.Login;
            rating.Liked = liked;
            rating.Disliked = disliked;
            context.CommentRatings.Add(rating);
            context.SaveChanges();
            return true;
        }

        public void AddComment (Comment comment)
        {
            comment.IdComment = context.Comments.Count() + 1;
            context.Comments.Add(comment);
            context.SaveChanges();
        }

        public int? GetUserScore(string userId)
        {
            return context.Travels.Where(t => t.IdTraveler.Equals(userId)).Sum(t => t.Score);
        }
    }
}
