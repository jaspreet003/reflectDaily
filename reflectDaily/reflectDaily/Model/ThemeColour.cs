using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace reflectDaily.Model
{
    public class MyColour
    {
        public string colourName { get; set; }
        public string colourValue { get; set; }
    }

    public class ThemeColour
    {
        public static readonly Dictionary<string, string> ColorDictionary = new Dictionary<string, string>
    {
        { "Red", "#FF0000" },
        { "Orange", "#FFA500" },
        { "Yellow", "#FFFF00" },
        { "Green", "#008000" },
        { "Cyan", "#00FFFF" },
        { "Blue", "#0000FF" },
        { "Purple", "#800080" },
        { "Pink", "#FFC0CB" },
        { "LightBlue", "#5FAEAA" },
        { "LightGray", "#D3D3D3" }
    };
    
        public static List<MyColour> getColorList() {

            var list = new List<MyColour>();

           foreach (var colour in ColorDictionary)
            {
                var colourObject = new MyColour();
                colourObject.colourName = colour.Key;
                colourObject.colourValue = colour.Value;
                list.Add(colourObject);
            }

           return list;

        }

        
    }

}
