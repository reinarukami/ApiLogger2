﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLogger.Models.JSON
{
    public class LogJSON
    {
        public string deviceID { get; set; }
        public string values { get; set; }
        public int datetime { get; set; }
    }
}
