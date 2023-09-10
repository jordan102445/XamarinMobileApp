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
    public partial class Main : FlyoutPage
    {
        public Main()
        {
            InitializeComponent();
            pov.listview.ItemSelected += OnSelectedItem;
        }

        private void OnSelectedItem(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as ItemPage;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.Target));
                pov.listview.SelectedItem = item;
                IsPresented = false;
            }


        }
    }
}