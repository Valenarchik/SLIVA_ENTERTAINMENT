﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WantApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WantApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : ContentPage
    {
        public Menu()
        {
            InitializeComponent();
            BindingContext = Interface.Model;
        }
    }
}