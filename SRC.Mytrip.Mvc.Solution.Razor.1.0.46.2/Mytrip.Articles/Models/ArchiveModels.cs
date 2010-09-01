﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Mytrip.Articles.Models
{
        [MetadataType(typeof(ArchiveIndexModel))]
        public class ArchiveIndexModel
        {
            public string PageTitle { get; set; }
            public int Count { get; set; }
            public string Path { get; set; }
            public string Culture { get; set; }
        }
}
