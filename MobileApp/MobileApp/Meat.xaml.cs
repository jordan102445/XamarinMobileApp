using connectionDB;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xamarin.CommunityToolkit.Extensions;
using connectionDB;
using MobileApp;

namespace Delicious_app
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Meat : ContentPage
    {
        SqlConnection connection;
        private List<string> meatOptions;
        private string username;
        public Meat(string username)
        {
            InitializeComponent();

            meatOptions = new List<string>
            {
                "Steak",
                "Baconwitheggs",
                "Ribswithbarbecuesauce",
                "Hamwithpineapple",

            };

            UpdateMeatOptions("");
            this.username = username;
            usernamelabel.Text = "Наздравје јадење , " + username;

            MessagingCenter.Subscribe<MenuSposnorsPage>(this, "UserLoggedOut", (sender) =>
            {
                // Clear the DisplayUsernameLabel.Text when the message is received
                usernamelabel.Text = string.Empty;
            });

            Connection dbConnection = new Connection();
            connection = dbConnection.GetDBConnection();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {

            var vnesi = e.NewTextValue;

            UpdateMeatOptions(vnesi);

        }
        

            

        

        private void UpdateMeatOptions(string vnesi)
        {
            var filter = meatOptions.Where(option => option.ToLower().Contains(vnesi.ToLower())).ToList(); 

            MeatOptionFlexLayOut.Children.Clear();

            foreach (var option in filter)
            {
                var frame = (Frame)FindByName(option.Replace(" ",string.Empty));

                if (frame != null)
                {
                    MeatOptionFlexLayOut.Children.Add(frame);
                }
            }

           
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            using (SqlConnection connection = new Connection().GetDBConnection())
            {
                connection.Open();

                int Idporacka = 0;
                using (SqlCommand command = new SqlCommand("SELECT MAX(id) FROM dbo.poracka", connection))
                {
                    var result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        Idporacka = Convert.ToInt32(result);
                    }
                }

                int Id = Idporacka + 1;
                int fetchedLoginId = 0;

                using (SqlCommand command = new SqlCommand("SELECT loginid FROM dbo.login WHERE username = @username", connection))
                {
                    command.Parameters.Add(new SqlParameter("@username", username));

                    object rez = command.ExecuteScalar();

                    if (rez != DBNull.Value && rez != null)
                    {
                        fetchedLoginId = Convert.ToInt32(rez);


                    }

                }

                int cenastek = 340;
                bool vneseni = false;



                using (SqlCommand insertCommand = new SqlCommand("INSERT INTO dbo.poracka(id, loginid, poracka, brporacka,cena) VALUES (@id, @loginid, @poracka, @brporacka,@cena)", connection))
                {
                    insertCommand.Parameters.Add(new SqlParameter("id", Id));
                    insertCommand.Parameters.Add(new SqlParameter("loginid", fetchedLoginId));
                    insertCommand.Parameters.Add(new SqlParameter("poracka", Stek.Text));
                    insertCommand.Parameters.Add(new SqlParameter("brporacka", Broj11steak.Text));
                    insertCommand.Parameters.Add(new SqlParameter("cena", cenastek));


                    insertCommand.ExecuteNonQuery();
                    vneseni = true;



                }

                if (vneseni)
                {

                    await DisplayAlert("Успешно", "Успешна порачка", "Ok");

                    var target = new Popupmenu(username);
                    var navigation = Application.Current.MainPage.Navigation;
                    navigation.ShowPopup(target);


                }
                else
                {
                    await DisplayAlert("Неуспешно", "Неуспешна порачка", "Ok");

                }

            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            using (SqlConnection connection = new Connection().GetDBConnection())
            {
                connection.Open();

                int Idporacka = 0;
                using (SqlCommand command = new SqlCommand("SELECT MAX(id) FROM dbo.poracka", connection))
                {
                    var result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        Idporacka = Convert.ToInt32(result);
                    }
                }

                int Id = Idporacka + 1;
                int fetchedLoginId = 0;

                using (SqlCommand command = new SqlCommand("SELECT loginid FROM dbo.login WHERE username = @username", connection))
                {
                    command.Parameters.Add(new SqlParameter("@username", username));

                    object rez = command.ExecuteScalar();

                    if (rez != DBNull.Value && rez != null)
                    {
                        fetchedLoginId = Convert.ToInt32(rez);


                    }

                }

                int cenabacon = 240;
                bool vneseni = false;



                using (SqlCommand insertCommand = new SqlCommand("INSERT INTO dbo.poracka(id, loginid, poracka, brporacka,cena) VALUES (@id, @loginid, @poracka, @brporacka,@cena)", connection))
                {
                    insertCommand.Parameters.Add(new SqlParameter("id", Id));
                    insertCommand.Parameters.Add(new SqlParameter("loginid", fetchedLoginId));
                    insertCommand.Parameters.Add(new SqlParameter("poracka", Bacon.Text));
                    insertCommand.Parameters.Add(new SqlParameter("brporacka", Broj12bacon.Text));
                    insertCommand.Parameters.Add(new SqlParameter("cena", cenabacon));


                    insertCommand.ExecuteNonQuery();
                    vneseni = true;



                }

                if (vneseni)
                {

                    await DisplayAlert("Успешно", "Успешна порачка", "Ok");

                    var target = new Popupmenu(username);
                    var navigation = Application.Current.MainPage.Navigation;
                    navigation.ShowPopup(target);


                }
                else
                {
                    await DisplayAlert("Неуспешно", "Неуспешна порачка", "Ok");

                }
            }
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            using (SqlConnection connection = new Connection().GetDBConnection())
            {
                connection.Open();

                int Idporacka = 0;
                using (SqlCommand command = new SqlCommand("SELECT MAX(id) FROM dbo.poracka", connection))
                {
                    var result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        Idporacka = Convert.ToInt32(result);
                    }
                }

                int Id = Idporacka + 1;
                int fetchedLoginId = 0;

                using (SqlCommand command = new SqlCommand("SELECT loginid FROM dbo.login WHERE username = @username", connection))
                {
                    command.Parameters.Add(new SqlParameter("@username", username));

                    object rez = command.ExecuteScalar();

                    if (rez != DBNull.Value && rez != null)
                    {
                        fetchedLoginId = Convert.ToInt32(rez);


                    }

                }

                int cenaribs = 360;
                bool vneseni = false;



                using (SqlCommand insertCommand = new SqlCommand("INSERT INTO dbo.poracka(id, loginid, poracka, brporacka,cena) VALUES (@id, @loginid, @poracka, @brporacka,@cena)", connection))
                {
                    insertCommand.Parameters.Add(new SqlParameter("id", Id));
                    insertCommand.Parameters.Add(new SqlParameter("loginid", fetchedLoginId));
                    insertCommand.Parameters.Add(new SqlParameter("poracka", ribs.Text));
                    insertCommand.Parameters.Add(new SqlParameter("brporacka", Broj13ribs.Text));
                    insertCommand.Parameters.Add(new SqlParameter("cena", cenaribs));


                    insertCommand.ExecuteNonQuery();
                    vneseni = true;



                }

                if (vneseni)
                {

                    await DisplayAlert("Успешно", "Успешна порачка", "Ok");

                    var target = new Popupmenu(username);
                    var navigation = Application.Current.MainPage.Navigation;
                    navigation.ShowPopup(target);


                }
                else
                {
                    await DisplayAlert("Неуспешно", "Неуспешна порачка", "Ok");

                }
            }
        }

        private async void Button_Clicked_3(object sender, EventArgs e)
        {
            using (SqlConnection connection = new Connection().GetDBConnection())
            {

                connection.Open();

                int Idporacka = 0;
                using (SqlCommand command = new SqlCommand("SELECT MAX(id) FROM dbo.poracka", connection))
                {
                    var result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        Idporacka = Convert.ToInt32(result);
                    }
                }

                int Id = Idporacka + 1;
                int fetchedLoginId = 0;

                using (SqlCommand command = new SqlCommand("SELECT loginid FROM dbo.login WHERE username = @username", connection))
                {
                    command.Parameters.Add(new SqlParameter("@username", username));

                    object rez = command.ExecuteScalar();

                    if (rez != DBNull.Value && rez != null)
                    {
                        fetchedLoginId = Convert.ToInt32(rez);


                    }

                }

                int cenaham = 270;
                bool vneseni = false;



                using (SqlCommand insertCommand = new SqlCommand("INSERT INTO dbo.poracka(id, loginid, poracka, brporacka,cena) VALUES (@id, @loginid, @poracka, @brporacka,@cena)", connection))
                {
                    insertCommand.Parameters.Add(new SqlParameter("id", Id));
                    insertCommand.Parameters.Add(new SqlParameter("loginid", fetchedLoginId));
                    insertCommand.Parameters.Add(new SqlParameter("poracka", Ham.Text));
                    insertCommand.Parameters.Add(new SqlParameter("brporacka", Broj14ham.Text));
                    insertCommand.Parameters.Add(new SqlParameter("cena", cenaham));


                    insertCommand.ExecuteNonQuery();
                    vneseni = true;



                }

                if (vneseni)
                {

                    await DisplayAlert("Успешно", "Успешна порачка", "Ok");

                    var target = new Popupmenu(username);
                    var navigation = Application.Current.MainPage.Navigation;
                    navigation.ShowPopup(target);


                }
                else
                {
                    await DisplayAlert("Неуспешно", "Неуспешна порачка", "Ok");

                }
            }

        }

        
    }
}