using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    [Table("Chats")]
    public class Chat
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ChatUser> ChatUsers { get; set; }

    }
}
