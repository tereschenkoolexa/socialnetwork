using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    [Table("Messages")]
    public class Message
    {

        public int Id { get; set; }

        public string Context { get; set; }

        [ForeignKey("UserOf")]
        public int IdUser { get; set; }

        public User UserOf { get; set; }

    }
}
