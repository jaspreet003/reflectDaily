using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace reflectDaily.Model
{
    public class Theme
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("colors")]
        public ThemeColour Colors { get; set; }
        public bool IsSelected { get; set; }


    }

    public class ThemeColorBindingModel
    {
        public string Name { get; set; }
        public string PrimaryColor { get; set; }
    }
}
