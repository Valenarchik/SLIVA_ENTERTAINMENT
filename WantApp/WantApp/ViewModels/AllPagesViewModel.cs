﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WantApp.Models;
using Xamarin.Forms;
using Color = System.Drawing.Color;

namespace WantApp.ViewModels
{
    class AllPagesViewModel :BaseViewModel
    {
        private List<Theme> themes;
        public Theme CurrentTheme { get; private set; }

        public ICommand SetThemeCommand;
        public AllPagesViewModel(Themes theme)
        {
            themes = new List<Theme>
            {
                new Theme(Color.Azure, Color.Black, Color.Black,Color.Black),
                new Theme(Color.Black, Color.Azure, Color.Azure,Color.Azure)
            };
            CurrentTheme = themes[(int)theme];
            SetThemeCommand = new Command(SetTheme);
        }
        public void SetTheme(object theme)
        {
            CurrentTheme = themes[(int)theme];
            OnPropertyChanged(nameof(CurrentTheme));
        }
    }
}
