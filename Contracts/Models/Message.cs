﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models
{
    [Serializable]
    public class Message
    {
        public string Type { get; set; }
        public string Text { get; set; }
    }
}
