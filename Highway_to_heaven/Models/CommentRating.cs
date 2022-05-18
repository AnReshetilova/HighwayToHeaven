using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Highway_to_heaven.Models
{
    [Table("COMMENT_RATING")]
    public partial class CommentRating
    {
        [Key]
        [Column("rating_id")]
        public int RatingId { get; set; }
        [Column("id_user")]
        [StringLength(30)]
        [Unicode(false)]
        public string IdUser { get; set; }
        [Column("id_comment")]
        public int? IdComment { get; set; }
        [Column("liked")]
        public bool? Liked { get; set; }
        [Column("disliked")]
        public bool? Disliked { get; set; }

        [ForeignKey("IdComment")]
        [InverseProperty("CommentRatings")]
        public virtual Comment IdCommentNavigation { get; set; }
        [ForeignKey("IdUser")]
        [InverseProperty("CommentRatings")]
        public virtual User IdUserNavigation { get; set; }
    }
}
