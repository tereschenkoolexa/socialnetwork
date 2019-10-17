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

        [Key,ForeignKey("UserOf"), Column(Order = 1)]
        public int IdUser { get; set; }
        public virtual User UserOf { get; set; }

        [Key,ForeignKey("ChatOf"), Column(Order = 2)]
        public int IdChat { get; set; }
        public virtual Chat ChatOf { get; set; }

    }
}
