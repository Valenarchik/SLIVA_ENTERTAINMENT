using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using WantApp.ViewModels;
using Xamarin.Forms;

namespace WantApp.ViewModels
{
    public class FavoritesViewModel : BaseViewModel
    {
        public ObservableCollection<FavoritesElement> FavoritesElements { get; set; } =
            new ObservableCollection<FavoritesElement>();

        public ICommand ElementAddCommand { get; set; }

        public FavoritesViewModel()
        {
            ElementAddCommand = new Command(ElementAdd);
        }

        public void ElementAdd()
        {
            FavoritesElements.Add(new FavoritesElement());
            OnPropertyChanged(nameof(FavoritesElements));
        }
    }
}