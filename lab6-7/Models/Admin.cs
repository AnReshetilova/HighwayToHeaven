using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace lab6_7.Models
{
    [Table("ADMIN")]
    public partial class Admin
    {
        [Key]
        [Column("login")]
        [StringLength(30)]
        [Unicode(false)]
        public string Login { get; set; }
        [Column("password")]
        [StringLength(20)]
        [Unicode(false)]
        public string Password { get; set; }
        [Column("name")]
        [StringLength(30)]
        [Unicode(false)]
        public string Name { get; set; }
        [Column("picture")]
        [StringLength(70)]
        [Unicode(false)]
        public string Picture { get; set; }
    }
}
