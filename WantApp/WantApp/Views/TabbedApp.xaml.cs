using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WantApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedApp: TabbedPage
    {
        public TabbedApp()
        {
            InitializeComponent();
            CreateDefault(new MapPage());
        }

        protected sealed override Page CreateDefault(object item)
        {
            return base.CreateDefault(item);
        }
    }
}