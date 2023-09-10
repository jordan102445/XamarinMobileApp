using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowPage : ContentPage
    {
        public ShowPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var target = new Login();
            var navigation = Application.Current.MainPage.Navigation;
            navigation.PushAsync(target);
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            var target = new Register();
            var navigation = Application.Current.MainPage.Navigation;
            navigation.PushAsync(target);


        }
    }
}