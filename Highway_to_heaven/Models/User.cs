using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Highway_to_heaven.Models
{
    [Table("USER")]
    public partial class User
    {
        public User()
        {
            CommentRatings = new HashSet<CommentRating>();
            Comments = new HashSet<Comment>();
            Travels = new HashSet<Travel>();
        }

        [Key]
        [Column("login")]
        [StringLength(30)]
        [Unicode(false)]
        public string Login { get; set; }
        [Column("password")]
        [StringLength(100)]
        [Unicode(false)]
        public string Password { get; set; }
        [Column("name")]
        [StringLength(30)]
        [Unicode(false)]
        public string Name { get; set; }
        [Column("age")]
        [StringLength(5)]
        [Unicode(false)]
        public string Age { get; set; }
        [Column("address")]
        [StringLength(30)]
        [Unicode(false)]
        public string Address { get; set; }
        [Column("is_admin")]
        public bool? IsAdmin { get; set; }
        [Column("picture")]
        [StringLength(1000)]
        [Unicode(false)]
        public string Picture { get; set; }

        [InverseProperty("IdUserNavigation")]
        public virtual ICollection<CommentRating> CommentRatings { get; set; }
        [InverseProperty("IdTravelerNavigation")]
        public virtual ICollection<Comment> Comments { get; set; }
        [InverseProperty("IdTravelerNavigation")]
        public virtual ICollection<Travel> Travels { get; set; }
    }
}
