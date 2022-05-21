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