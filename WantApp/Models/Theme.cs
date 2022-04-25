using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Xml;

namespace WantApp.Models
{
    class Theme
    {
        public Color BackgroundColor { get; set; }
        public Color TextColor { get; set; }
        public Color UnselectedColor { get; set; }

        public Theme (Color backgroundColor, Color textColor, Color unselectedColor)
        {
            BackgroundColor = backgroundColor;
            TextColor = textColor;
            UnselectedColor = unselectedColor;
        }
    }

    enum Themes
    {
        WhiteTheme =0,
        BlackTheme =1
    }
}
