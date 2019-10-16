using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Gmail { get; set; }

        
        public virtual ICollection<ChatUser> ChatUsers { get; set; }

        public virtual ICollection<ChatUser> ChatUsers { get; set; }

        [ForeignKey("WallOf")]
        public int IdWall { get; set; }

        public virtual Wall WallOf { get; set; }

    }
}
