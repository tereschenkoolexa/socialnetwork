using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Models
{
    class MessageModel
    {

        public int Id { get; set; }

        public string Context { get; set; }
        public int IdUser { get; set; }
        public int IdChat { get; set; }

    }
}
