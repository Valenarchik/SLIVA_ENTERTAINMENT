using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using WantApp.Models;

namespace WantApp.ViewModels
{
    class MainPageViewModel :BaseViewModel
    {
        private List<Theme> themes;
        public Theme CurrentTheme { get; private set; }

        public MainPageViewModel()
        {
            themes = new List<Theme>
            {
                new Theme(Color.Azure, Color.Black, Color.Black),
                new Theme(Color.Black, Color.Azure, Color.Azure)
            };
            CurrentTheme = themes[0];
        }
        public void SetTheme(Themes theme)
        {
            CurrentTheme = themes[(int)theme];
            OnPropertyChanged(nameof(CurrentTheme));
        }
    }
}
