﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Core.Model
{
    public class OnLineUser
    {
        public string ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string GroupName { get; set; }
    }
}
