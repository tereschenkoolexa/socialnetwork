using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    [Table("ChatUsers")]
    public class ChatUser
    {

        [Key,ForeignKey("UserOf")]
        public int IdUser { get; set; }
        public virtual User UserOf { get; set; }

        [Key,ForeignKey("ChatOf")]
        public int IdChat { get; set; }
        public virtual Chat ChatOf { get; set; }

    }
}
