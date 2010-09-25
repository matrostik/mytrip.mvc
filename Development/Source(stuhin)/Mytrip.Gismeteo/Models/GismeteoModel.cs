﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Mytrip.Gismeteo.Models
{
    public class GismeteoSettingModel
    {
        public bool unlockGismeteo { get; set; }
        [Required(ErrorMessageResourceType = typeof(GismeteoLanguage), ErrorMessageResourceName = "nullvalue")]
        public string nameGismeteo { get; set; }
    }
    [MetadataType(typeof(ManagerModel))]
    public class ManagerModel
    {
        public int GismeteoId { get; set; }
        [Required(ErrorMessageResourceType = typeof(GismeteoLanguage), ErrorMessageResourceName = "nullvalue")]
        public string Title { get; set; }
        public bool AllCulture { get; set; }
        [Required(ErrorMessageResourceType = typeof(GismeteoLanguage), ErrorMessageResourceName = "nullvalue")]
        public string UrlXml { get; set; }
        public bool VisibleInformer { get; set; }
    }
}