using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Newtonsoft.Json;
using reflectDaily.Main.journal;
using System.IO;
using System.Reflection;

namespace reflectDaily.Model
{ 
    public class ThemeColour
    {
        [JsonProperty("primary")]
        public string Primary { get; set; }

        [JsonProperty("secondary")]
        public string Secondary { get; set; }

        [JsonProperty("accent")]
        public string Accent { get; set; }

        [JsonProperty("neutral")]
        public string Neutral { get; set; }

        [JsonProperty("base")]
        public string Base { get; set; }

        [JsonProperty("info")]
        public string Info { get; set; }

        [JsonProperty("success")]
        public string Success { get; set; }

        [JsonProperty("warning")]
        public string Warning { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        public static List<Theme> LoadThemes()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(ThemeColour)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("reflectDaily.theme.json");

            if (stream == null)
            {
                throw new FileNotFoundException("Theme JSON file not found.");
            }

            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                var themes = JsonConvert.DeserializeObject<List<Theme>>(json);
                return themes;
            }
        }

        public static void ApplyTheme(Theme selectedTheme)
        {
            if (selectedTheme != null)
            {
                Application.Current.Resources["primary"] = Color.FromHex(selectedTheme.Colors.Primary);
                Application.Current.Resources["secondary"] = Color.FromHex(selectedTheme.Colors.Secondary);
                Application.Current.Resources["accent"] = Color.FromHex(selectedTheme.Colors.Accent);
                Application.Current.Resources["neutral"] = Color.FromHex(selectedTheme.Colors.Neutral);
                Application.Current.Resources["base"] = Color.FromHex(selectedTheme.Colors.Base);
                Application.Current.Resources["info"] = Color.FromHex(selectedTheme.Colors.Info);
                Application.Current.Resources["success"] = Color.FromHex(selectedTheme.Colors.Success);
                Application.Current.Resources["warning"] = Color.FromHex(selectedTheme.Colors.Warning);
                Application.Current.Resources["error"] = Color.FromHex(selectedTheme.Colors.Error);
            }
        }

    }

}
