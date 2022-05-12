using System.Threading.Tasks;
using MvvmHelpers;
using Xamarin.Forms;

namespace WantApp.ViewModels
{
    public class MyViewModel : BaseViewModel

    {
    public async Task DisplayAlert(string title, string massage, string cancle)
    {
        await Application.Current.MainPage.DisplayAlert(title, massage, cancle);
    }

    public async Task DisplayAlert(string title, string massage, string accept, string cancle)
    {
        await Application.Current.MainPage.DisplayAlert(title, massage, accept, cancle);
    }
    }
}