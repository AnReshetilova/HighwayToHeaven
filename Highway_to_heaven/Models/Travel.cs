using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Highway_to_heaven.Models
{
    [Table("TRAVEL")]
    public partial class Travel
    {
        [Key]
        [Column("id_travel")]
        public int IdTravel { get; set; }
        [Column("id_tour")]
        public int? IdTour { get; set; }
        [Column("id_traveler")]
        [StringLength(30)]
        [Unicode(false)]
        public string IdTraveler { get; set; }
        [Column("departure_date", TypeName = "date")]
        public DateTime? DepartureDate { get; set; }
        [Column("return_date", TypeName = "date")]
        public DateTime? ReturnDate { get; set; }
        [Column("picture")]
        [StringLength(70)]
        [Unicode(false)]
        public string Picture { get; set; }

        [ForeignKey("IdTour")]
        [InverseProperty("Travels")]
        public virtual PackageTour IdTourNavigation { get; set; }
        [ForeignKey("IdTraveler")]
        [InverseProperty("Travels")]
        public virtual User IdTravelerNavigation { get; set; }
    }
}
