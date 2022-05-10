using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace lab6_7.Models
{
    [Table("COMMENT")]
    public partial class Comment
    {
        [Key]
        [Column("id_comment")]
        public int IdComment { get; set; }
        [Column("id_traveler")]
        [StringLength(30)]
        [Unicode(false)]
        public string IdTraveler { get; set; }
        [Column("id_tour")]
        public int? IdTour { get; set; }
        [Column("date_comment", TypeName = "datetime")]
        public DateTime? DateComment { get; set; }
        [Column("text_comment")]
        [StringLength(500)]
        [Unicode(false)]
        public string TextComment { get; set; }
        [Column("likes")]
        public int? Likes { get; set; }
        [Column("dislikes")]
        public int? Dislikes { get; set; }

        [ForeignKey("IdTour")]
        [InverseProperty("Comments")]
        public virtual PackageTour IdTourNavigation { get; set; }
        [ForeignKey("IdTraveler")]
        [InverseProperty("Comments")]
        public virtual User IdTravelerNavigation { get; set; }
    }
}
