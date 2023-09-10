using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MenuSposnorsPage : ContentPage
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

        public MenuSposnorsPage (string username,string token)
		{
			InitializeComponent ();
            this.token = token; 
            this.username = username;
            DisplayUsernameLabel.Text = "Логирани како :" + " " + username;


            this.dunkindonatsCLicked = dunkindonuts;
            this.wendyCLicked = wendy;
            this.tacobellCLicked = tacobell;
            this.burgerkingCLicked = burgerking;
            this.kfcCLicked = kfc;
            this.mcdonaldCLicked = mcdonal;
            this.starbucksCLicked = starbucks;
            this.papajhonsCLicked = papajhons;
            this.pizzahutCLicked = pizzahut;
            this.dairyqueenCLicked = dairyqueen;
            this.sonicCLicked = sonic;
            this.dominosCLicked = dominos;
            

            

        }

        
        private void DunkinDonatsClicked(object sender, EventArgs e)
        {
            var target = new HomePage(username,token);
            var navigation = Application.Current.MainPage.Navigation;
            navigation.PushAsync(target);

            MessagingCenter.Send(this, "DunkinDonatsClicked");

        }

        private void WendysClicked(object sender, EventArgs e)
        {
            var target = new HomePage(username,token);
            var navigation = Application.Current.MainPage.Navigation;
            navigation.PushAsync(target);

            MessagingCenter.Send(this, "WendysClicked");

        }

        private void TacoBellClicked(object sender, EventArgs e)
        {
            var target = new HomePage(username,token);
            var navigation = Application.Current.MainPage.Navigation;
            navigation.PushAsync(target);

            MessagingCenter.Send(this, "TacoBellClicked");

        }

        private void BurgerKingClicked(object sender, EventArgs e)
        {
            var target = new HomePage(username,token);
            var navigation = Application.Current.MainPage.Navigation;
            navigation.PushAsync(target);

            MessagingCenter.Send(this, "BurgerKingClicked");
        }

        private void KfcClicked(object sender, EventArgs e)
        {
            var target = new HomePage(username,token);
            var navigation = Application.Current.MainPage.Navigation;
            navigation.PushAsync(target);

            MessagingCenter.Send(this, "KfcClicked");

        }

        private void McDonaldClicked(object sender, EventArgs e)
        {
            var target = new HomePage(username,token);
            var navigation = Application.Current.MainPage.Navigation;
            navigation.PushAsync(target);

            MessagingCenter.Send(this, "McDonaldClicked");

        }

       

        private void Logout(object sender, EventArgs e)
        {
            Preferences.Remove(username);

            Preferences.Remove(token);

            DisplayUsernameLabel.Text = string.Empty;

           


            var target = new Login();
            var navigation = Application.Current.MainPage.Navigation;
            navigation.PushAsync(target);

            MessagingCenter.Send(this, "UserLoggedOut");

        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {

            var target = new HomePage(username, token);
            var navigation = Application.Current.MainPage.Navigation;
            navigation.PushAsync(target);

            

        }

        private void StarbucksClicked(object sender, EventArgs e)
        {
            var target = new HomePage(username, token);
            var navigation = Application.Current.MainPage.Navigation;
            navigation.PushAsync(target);

            MessagingCenter.Send(this, "StarbucksClicked");

            

        }

        private void PapaJhonsClicked(object sender, EventArgs e)
        {
            var target = new HomePage(username, token);
            var navigation = Application.Current.MainPage.Navigation;
            navigation.PushAsync(target);

            MessagingCenter.Send(this, "PapaJhonsClicked");

            

        }

        private void PizzHut(object sender, EventArgs e)
        {
            var target = new HomePage(username, token);
            var navigation = Application.Current.MainPage.Navigation;
            navigation.PushAsync(target);

            MessagingCenter.Send(this, "PizzHutClicked");

            

        }

        private void DairyQueenClicked(object sender, EventArgs e)
        {
            var target = new HomePage(username, token );
            var navigation = Application.Current.MainPage.Navigation;
            navigation.PushAsync(target);

            //MessagingCenter.Send(this, "DairyQueenClicked");
           
        }

        private void SonicClicked(object sender, EventArgs e)
        {
            var target = new HomePage(username, token);
            var navigation = Application.Current.MainPage.Navigation;
            navigation.PushAsync(target);

            MessagingCenter.Send(this, "SonicClicked");

        }

        private void DominosClicked(object sender, EventArgs e)
        {
            var target = new HomePage(username, token);
            var navigation = Application.Current.MainPage.Navigation;
            navigation.PushAsync(target);

            MessagingCenter.Send(this, "DominosClicked");
        }
    }
}