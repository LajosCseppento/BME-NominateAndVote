using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebExample.Models
{
    public class User
    {
        public int ID { get; set; }
        public String Token { get; set; }
        public Boolean isBanned { get; set; }
    }
}