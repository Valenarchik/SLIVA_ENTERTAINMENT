using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Xamarin.Forms;
using Color = System.Drawing.Color;

namespace WantApp.Models
{
    public class Theme
    {
        public Color BackgroundColor { get; set; }
        public Color TextColor { get; set; }
        public Color UnselectedColor { get; set; }
        public Color TitleColor { get; set; }

        public Color BorderColor { get; set; }

        public Theme(Color backgroundColor, Color textColor, Color unselectedColor)
        {
            BackgroundColor = backgroundColor;
            TextColor = textColor;
            UnselectedColor = unselectedColor;
            TitleColor = TextColor;
            BorderColor = TextColor;
        }
    }

    enum Themes
    {
        WhiteTheme = 0,
        BlackTheme = 1
    }
}