using Delicious_app;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Reflection;
using Xamarin.CommunityToolkit.Extensions;

namespace MobileApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
        private string username;
        private string token;


        public ImageButton dunkindonatsCLicked;
        public ImageButton wendyCLicked;
        public ImageButton tacobellCLicked;
        public ImageButton burgerkingCLicked;
        public ImageButton kfcCLicked;
        public ImageButton mcdonaldCLicked;
        public ImageButton starbucksCLicked;
        public ImageButton papajhonsCLicked;
        public ImageButton pizzahutCLicked;
        public ImageButton dairyqueenCLicked;
        public ImageButton sonicCLicked;
        public ImageButton dominosCLicked;





        public HomePage (string username,string token)

		{

			InitializeComponent ();

            this.username = username;
            DisplayUsernameLabel.Text = "Логирани како :"+" "+ username;



            // appering and dissapering buttons for food 

            MessagingCenter.Subscribe<MenuSposnorsPage>(this, "DunkinDonatsClicked", (sender) => {
                pizzbutton.IsVisible = false;
                meat.IsVisible = false;
                burger.IsVisible = false;
                chicken.IsVisible = false;
            });


            MessagingCenter.Subscribe<MenuSposnorsPage>(this, "WendysClicked", (sender) => {
                pizzbutton.IsVisible = false;
                meat.IsVisible = false;
                chicken.IsVisible = false;
            });

            MessagingCenter.Subscribe<MenuSposnorsPage>(this, "TacoBellClicked", (sender) => {
                pizzbutton.IsVisible = false;
                meat.IsVisible = false;
                dessert.IsVisible = false;
            });
            

            MessagingCenter.Subscribe<MenuSposnorsPage>(this, "BurgerKingClicked", (sender) => {
                pizzbutton.IsVisible = false;
                meat.IsVisible = false;
                
            });

            MessagingCenter.Subscribe<MenuSposnorsPage>(this, "KfcClicked", (sender) => {
                pizzbutton.IsVisible = false;
                meat.IsVisible = false;

            });

            MessagingCenter.Subscribe<MenuSposnorsPage>(this, "McDonaldClicked", (sender) => {
                pizzbutton.IsVisible = false;
                meat.IsVisible = false;

            });
            MessagingCenter.Subscribe<MenuSposnorsPage>(this, "StarbucksClicked", (sender) => {
                pizzbutton.IsVisible = false;
                meat.IsVisible = false;
                burger.IsVisible = false;
                chicken.IsVisible = false;
            });

            MessagingCenter.Subscribe<MenuSposnorsPage>(this, "PapaJhonsClicked", (sender) => {
                dessert.IsVisible= false;
                meat.IsVisible = false;
                burger.IsVisible = false;
                chicken.IsVisible = false;
            });

            MessagingCenter.Subscribe<MenuSposnorsPage>(this, "PizzHutClicked", (sender) => {
                dessert.IsVisible = false;
                meat.IsVisible = false;
                burger.IsVisible = false;
                chicken.IsVisible = false;
            });

            //MessagingCenter.Subscribe<MenuSposnorsPage>(this, "DairyQueenClicked", (sender) => {
            //dessert.IsVisible = false;
            // meat.IsVisible = false;
            //burger.IsVisible = false;
            //chicken.IsVisible = false;
            //});

            MessagingCenter.Subscribe<MenuSposnorsPage>(this, "SonicClicked", (sender) => {
                pizzbutton.IsVisible = false;
                meat.IsVisible = false;
            });

            MessagingCenter.Subscribe<MenuSposnorsPage>(this, "DominosClicked", (sender) => {
                meat.IsVisible = false;
                burger.IsVisible = false;
                chicken.IsVisible = false;
            });




        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<MenuSposnorsPage>(this, "DunkinDonatsClicked");
            MessagingCenter.Unsubscribe<MenuSposnorsPage>(this, "WendysClicked");
            MessagingCenter.Unsubscribe<MenuSposnorsPage>(this, "TacoBellClicked");
            MessagingCenter.Unsubscribe<MenuSposnorsPage>(this, "BurgerKingClicked");
            MessagingCenter.Unsubscribe<MenuSposnorsPage>(this, "KfcClicked");
            MessagingCenter.Unsubscribe<MenuSposnorsPage>(this, "McDonaldClicked");
            MessagingCenter.Unsubscribe<MenuSposnorsPage>(this, "StarbucksClicked");
            MessagingCenter.Unsubscribe<MenuSposnorsPage>(this, "PapaJhonsClicked");
            MessagingCenter.Unsubscribe<MenuSposnorsPage>(this, "PizzHutClicked");
            MessagingCenter.Unsubscribe<MenuSposnorsPage>(this, "SonicClicked");
            MessagingCenter.Unsubscribe<MenuSposnorsPage>(this, "DominosClicked");

        }

       
            
        

        private void Button_Clicked(object sender, EventArgs e)
        {

            var target = new Pizzapage(username);
            var navigation = Application.Current.MainPage.Navigation;
            navigation.PushAsync(target);

        }

        
       

        private void Button_Clicked_1(object sender, EventArgs e)
        {

            var target = new Meat(username);
            var navigation = Application.Current.MainPage.Navigation;
            navigation.PushAsync(target);

        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            var target = new Dessert(username);
            var navigation = Application.Current.MainPage.Navigation;
            navigation.PushAsync(target);

        }

        private void Button_Clicked_3(object sender, EventArgs e)
        {
            var target = new Burger(username);
            var navigation = Application.Current.MainPage.Navigation;
            navigation.PushAsync(target);

        }

        private void Button_Clicked_4(object sender, EventArgs e)
        {

            var target = new Juices(username);
            var navigation = Application.Current.MainPage.Navigation;
            navigation.PushAsync(target);

        }

        private void Button_Clicked_5(object sender, EventArgs e)
        {
            var target = new Chickenmix(username);
            var navigation = Application.Current.MainPage.Navigation;
            navigation.PushAsync(target);

        }

       


        private void LOGOUT(object sender, EventArgs e)
        {
           
            Preferences.Remove(username);

            Preferences.Remove(token);
            
            DisplayUsernameLabel.Text = string.Empty;
            
            
            var target = new Login();
            var navigation = Application.Current.MainPage.Navigation;
            navigation.PushAsync(target);
        }


    }
}