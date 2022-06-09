using System.Drawing;

namespace WantApp.ThemesModel
{
    public interface IEnvironment
    {
        void SetStatusBarColor(Color color, bool darkStatusBarTint);
    }
}