﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthConsult.Classes
{
    internal class User
    {
        public string login { get; set; }
        public string password { get; set; }
        public List<string> wallets { get; set; }

        
    }
}
