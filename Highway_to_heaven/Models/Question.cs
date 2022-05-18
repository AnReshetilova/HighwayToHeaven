using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Highway_to_heaven.Models
{
    [Table("QUESTION")]
    public partial class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
        }

        [Key]
        [Column("id_question")]
        public int IdQuestion { get; set; }
        [Column("id_tour")]
        public int? IdTour { get; set; }
        [Column("question")]
        [StringLength(200)]
        [Unicode(false)]
        public string Question1 { get; set; }

        [ForeignKey("IdTour")]
        [InverseProperty("Questions")]
        public virtual PackageTour IdTourNavigation { get; set; }
        [InverseProperty("IdQuestionNavigation")]
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
