using System.Collections.ObjectModel;
using System.Windows.Input;
using MvvmHelpers;
using MvvmHelpers.Commands;

namespace WantApp.ViewModels
{
    public class FavoritesViewModel: BaseViewModel
    {
        public ObservableCollection<FavoritesElement> FavoritesElements { get; set; } =
            new ObservableCollection<FavoritesElement>();
        public ObservableCollection<FavoritesElement> AddedElements { get; set; } =
            new ObservableCollection<FavoritesElement>();
        
        public ICommand FavoriteButtonCommand { get; set; }
        public ICommand AddedButtonCommand { get; set; }
        public ICommand FindOrganizationCommand { get; set; }
        public ICommand ElementAddCommand { get; set; }
        

        public FavoritesViewModel()
        {
            ElementAddCommand = new Command(ElementAdd);
            FindOrganizationCommand = new Command(FindOrganization);
        }


        private void FindOrganization(object obj)
        {
            
        }
        private void ElementAdd()
        {
            FavoritesElements.Add(new FavoritesElement());
            OnPropertyChanged(nameof(FavoritesElements));
        }
    }
}