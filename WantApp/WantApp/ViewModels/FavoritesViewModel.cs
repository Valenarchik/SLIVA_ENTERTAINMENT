using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WantApp.Models;
using WantApp.Services;
using WantApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace WantApp.ViewModels
{
    public class FavoritesViewModel: DisplayAlertViewModel
    {
        private bool favoritesElementsIsVisible = false;

        public bool FavoritesElementsIsVisible
        {
            get => favoritesElementsIsVisible;
            set
            {
                favoritesElementsIsVisible = value;
                OnPropertyChanged(nameof(FavoritesElementsIsVisible));
            } 
        }

        public ObservableCollection<FavoriteElementViewModel> FavoritesElements { get; set; } =
            new ObservableCollection<FavoriteElementViewModel>();
        
        private bool addedElementsIsVisible = true;
        public bool AddedElementsIsVisible
        {
            get => addedElementsIsVisible;
            set
            {
                addedElementsIsVisible = value;
                OnPropertyChanged(nameof(AddedElementsIsVisible));
            } 
        }
        public ObservableCollection<FavoriteElementViewModel> AddedElements { get; set; } =
            new ObservableCollection<FavoriteElementViewModel>();

        public ICommand FavoritesButtonClickCommand { get; set; }
        public ICommand AddedButtonClickCommand { get; set; }
        public ICommand AddFavoritesCommand { get; set; }
        public ICommand RemoveFavoritesCommand { get; set; }
        public ICommand ElementAddCommand { get; private set; }
        
        public FavoritesViewModel()
        {
            ElementAddCommand = new Command(ElementAdd);
            RemoveFavoritesCommand = new Command(RemoveFavorites);
            AddFavoritesCommand = new Command(AddFavorites);
            FavoritesButtonClickCommand = new Command(AddedButtonClick);
            AddedButtonClickCommand = new Command(FavoritesButtonClick);
        }

        private void AddedButtonClick()
        {
            FavoritesElementsIsVisible = true;
            AddedElementsIsVisible = false;
        }

        private void FavoritesButtonClick()
        {
            FavoritesElementsIsVisible = false;
            AddedElementsIsVisible = true;
        }
        
        private void AddFavorites(object obj)
        {
            var element = obj as FavoriteElementViewModel;
            AddedElements.Remove(element);
            FavoritesElements.Add(element);
        }
        
        private void RemoveFavorites(object obj)
        {
            var element = obj as FavoriteElementViewModel;
            FavoritesElements.Remove(element);
            AddedElements.Add(element);
        }
        
        private void ElementAdd()
        {
            AddedElements.Add(new FavoriteElementViewModel());
            OnPropertyChanged(nameof(AddedElements));
        }
    }
}