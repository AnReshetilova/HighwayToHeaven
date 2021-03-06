using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Highway_to_heaven.Models
{
    [Table("PLANET")]
    public partial class Planet
    {
        public Planet()
        {
            PackageTours = new HashSet<PackageTour>();
        }

        [Key]
        [Column("name")]
        [StringLength(30)]
        [Unicode(false)]
        public string Name { get; set; }
        [Column("planetary_system")]
        [StringLength(30)]
        [Unicode(false)]
        public string PlanetarySystem { get; set; }
        [Column("surface_area")]
        [StringLength(30)]
        [Unicode(false)]
        public string SurfaceArea { get; set; }
        [Column("sidereal_rotation_period")]
        [StringLength(30)]
        [Unicode(false)]
        public string SiderealRotationPeriod { get; set; }
        [Column("min_temp")]
        [StringLength(10)]
        [Unicode(false)]
        public string MinTemp { get; set; }
        [Column("max_temp")]
        [StringLength(10)]
        [Unicode(false)]
        public string MaxTemp { get; set; }
        [Column("description")]
        [StringLength(1000)]
        [Unicode(false)]
        public string Description { get; set; }
        [Column("picture")]
        [StringLength(1000)]
        [Unicode(false)]
        public string Picture { get; set; }

        [InverseProperty("PlanetNameNavigation")]
        public virtual ICollection<PackageTour> PackageTours { get; set; }
    }
}
