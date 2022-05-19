﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.EventSource;

namespace SpaceAdventures.Application.Common.Models.APIConsume.NASA
{
    public class Data
    {
        public string[] album;
        public string center { get; set; }
        public string title { get; set; }
        public string nasa_id { get; set; }
        public string media_Type { get; set; }
        public DateTime date_created { get; set; }
        string description { get; set; }

    }
}