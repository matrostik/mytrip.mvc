using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace mtm.Weather.Models
{
    public class WeatherSettingModel
    {
        public bool unlockWeather { get; set; }
        [Required(ErrorMessageResourceType = typeof(WeatherLanguage), ErrorMessageResourceName = "nullvalue")]
        public string nameWeather { get; set; }
    }
    public class ManagerModel
    {
        public int WeatherId { get; set; }
        [Required(ErrorMessageResourceType = typeof(WeatherLanguage), ErrorMessageResourceName = "nullvalue")]
        public string Title { get; set; }
        public bool AllCulture { get; set; }
        [Required(ErrorMessageResourceType = typeof(WeatherLanguage), ErrorMessageResourceName = "nullvalue")]
        [AllowHtml]
        public string UrlXml { get; set; }
        public bool VisibleInformer { get; set; }
    }
}
