﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WantApp.ViewModels;
using Xamarin.Forms;


namespace WantApp.Views
{
    public partial class Favorites : ContentPage
    {
        public Favorites()
        {
            InitializeComponent();
            BindingContext = Interface.Model;
        }
    }
}
