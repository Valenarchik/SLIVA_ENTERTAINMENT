using System.Threading.Tasks;
using MvvmHelpers;
using Xamarin.Forms;

namespace WantApp.ViewModels
{
    public class DisplayAlertViewModel : BaseViewModel
    {
        public async Task DisplayAlert(string title, string massage, string cancle)
        {
            await Application.Current.MainPage.DisplayAlert(title, massage, cancle);
        }

        public async Task DisplayAlert(string title, string massage, string accept, string cancle)
        {
            await Application.Current.MainPage.DisplayAlert(title, massage, accept, cancle);
        }
        public async Task<string> DisplayPromptAsync(string title, string massage)
        {
            return await Application.Current.MainPage.DisplayPromptAsync(title, massage);
        }
    }
}