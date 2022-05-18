using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Highway_to_heaven.Models
{
    [Table("PACKAGE_TOUR")]
    public partial class PackageTour
    {
        public PackageTour()
        {
            Comments = new HashSet<Comment>();
            Questions = new HashSet<Question>();
            Travels = new HashSet<Travel>();
        }

        [Key]
        [Column("id_tour")]
        public int IdTour { get; set; }
        [Column("planet_name")]
        [StringLength(30)]
        [Unicode(false)]
        public string PlanetName { get; set; }
        [Column("number_of_days")]
        public int? NumberOfDays { get; set; }
        [Column("rating")]
        public float? Rating { get; set; }
        [Column("description")]
        [StringLength(1000)]
        [Unicode(false)]
        public string Description { get; set; }
        [Column("picture")]
        [StringLength(1000)]
        [Unicode(false)]
        public string Picture { get; set; }
        [Column("tour_name")]
        [StringLength(50)]
        [Unicode(false)]
        public string TourName { get; set; }
        [Column("second_picture")]
        [StringLength(1000)]
        [Unicode(false)]
        public string SecondPicture { get; set; }

        [ForeignKey("PlanetName")]
        [InverseProperty("PackageTours")]
        public virtual Planet PlanetNameNavigation { get; set; }
        [InverseProperty("IdTourNavigation")]
        public virtual ICollection<Comment> Comments { get; set; }
        [InverseProperty("IdTourNavigation")]
        public virtual ICollection<Question> Questions { get; set; }
        [InverseProperty("IdTourNavigation")]
        public virtual ICollection<Travel> Travels { get; set; }
    }
}
