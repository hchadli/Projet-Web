﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Models.APIConsume.NASA
{
    public class Item
    {
        public string href { get; set; }
        public Data[] data { get; set; }
        
        public LinkItem[] links { get; set; }
    }
}