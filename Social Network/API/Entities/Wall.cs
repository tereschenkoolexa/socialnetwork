using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    [Table("Walls")]
    public class Wall
    {
        [Key]
        public int Id { get; set; }

        public string ImageAvatar { get; set; }

        public int Age { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Status { get; set; }

        [ForeignKey("UserOf")]
        public int IdUser { get; set; }

        public User UserOf { get; set; }

    }
}
