using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Highway_to_heaven.Models
{
    [Table("ANSWER")]
    public partial class Answer
    {
        [Key]
        [Column("id_answer")]
        public int IdAnswer { get; set; }
        [Column("id_question")]
        public int? IdQuestion { get; set; }
        [Column("answer")]
        [StringLength(200)]
        [Unicode(false)]
        public string Answer1 { get; set; }
        [Column("is_correct")]
        public bool? IsCorrect { get; set; }

        [ForeignKey("IdQuestion")]
        [InverseProperty("Answers")]
        public virtual Question IdQuestionNavigation { get; set; }
    }
}
