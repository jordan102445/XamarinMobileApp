using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using connectionDB;
using System.Data.SqlClient;
using Xamarin.CommunityToolkit.Extensions;
using System.Data.Common;
using System.Data;
using MobileApp;

namespace Delicious_app
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Burger : ContentPage

    {
        SqlConnection connection;
        private string username;

        private List<string> BurgerOptions;

        
            public Burger(string username)
        {
            InitializeComponent();

            this.username = username;
            usernamelabel.Text = "Наздравје јадење , " + username;

            MessagingCenter.Subscribe<MenuSposnorsPage>(this, "UserLoggedOut", (sender) =>
            {
                // Clear the DisplayUsernameLabel.Text when the message is received
                usernamelabel.Text = string.Empty;
            });

            Connection dbConnection = new Connection();
            connection = dbConnection.GetDBConnection();

            BurgerOptions = new List<string>
            {

                "Hamburger",
                "VeggieBurger",
                "Cheeseburger",
                "Chickenburger",
                "Beanburger",
                "Riceburger"




            };

           


            UpdateBugerOptions("");
        }

        


        private void SeacrhBar_TextChanged(object sender, TextChangedEventArgs e)

        {

            var vnesi = e.NewTextValue;
            UpdateBugerOptions(vnesi);

        }

        private void UpdateBugerOptions(string vnesi)
        {
            var filter = BurgerOptions.Where(option => option.ToLower().Contains(vnesi.ToLower())).ToList();

            BugrgerFlexOut.Children.Clear();

            foreach (var option in filter)
            {
                var frame = (Frame)FindByName(option.Replace(" ",string.Empty));

                if (frame != null) { 
                
                    BugrgerFlexOut.Children.Add(frame);
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

                int cenaham = 350;

                bool vneseni = false;



                using (SqlCommand insertCommand = new SqlCommand("INSERT INTO dbo.poracka(id, loginid, poracka, brporacka,cena) VALUES (@id, @loginid, @poracka, @brporacka,@cena)", connection))
                {
                    insertCommand.Parameters.Add(new SqlParameter("id", Id));
                    insertCommand.Parameters.Add(new SqlParameter("loginid", fetchedLoginId));
                    insertCommand.Parameters.Add(new SqlParameter("poracka", Hamburgeer.Text));
                    insertCommand.Parameters.Add(new SqlParameter("brporacka", Broj21hamburger.Text));
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

                int cenaveggie = 280;

                bool vneseni = false;



                using (SqlCommand insertCommand = new SqlCommand("INSERT INTO dbo.poracka(id, loginid, poracka, brporacka,cena) VALUES (@id, @loginid, @poracka, @brporacka,@cena)", connection))
                {
                    insertCommand.Parameters.Add(new SqlParameter("id", Id));
                    insertCommand.Parameters.Add(new SqlParameter("loginid", fetchedLoginId));
                    insertCommand.Parameters.Add(new SqlParameter("poracka", Veggieburger.Text));
                    insertCommand.Parameters.Add(new SqlParameter("brporacka", Broj22veggieburger.Text));
                    insertCommand.Parameters.Add(new SqlParameter("cena", cenaveggie));


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

                int cenacheese = 380;

                bool vneseni = false;



                using (SqlCommand insertCommand = new SqlCommand("INSERT INTO dbo.poracka(id, loginid, poracka, brporacka,cena) VALUES (@id, @loginid, @poracka, @brporacka,@cena)", connection))
                {
                    insertCommand.Parameters.Add(new SqlParameter("id", Id));
                    insertCommand.Parameters.Add(new SqlParameter("loginid", fetchedLoginId));
                    insertCommand.Parameters.Add(new SqlParameter("poracka", Cheeseburgerr.Text));
                    insertCommand.Parameters.Add(new SqlParameter("brporacka", Broj23cheeseburger.Text));
                    insertCommand.Parameters.Add(new SqlParameter("cena", cenacheese));


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

                int cenachicken = 340;

                bool vneseni = false;



                using (SqlCommand insertCommand = new SqlCommand("INSERT INTO dbo.poracka(id, loginid, poracka, brporacka,cena) VALUES (@id, @loginid, @poracka, @brporacka,@cena)", connection))
                {
                    insertCommand.Parameters.Add(new SqlParameter("id", Id));
                    insertCommand.Parameters.Add(new SqlParameter("loginid", fetchedLoginId));
                    insertCommand.Parameters.Add(new SqlParameter("poracka", Chickenburgerr.Text));
                    insertCommand.Parameters.Add(new SqlParameter("brporacka", Broj24chickenburger.Text));
                    insertCommand.Parameters.Add(new SqlParameter("cena", cenachicken));


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

        private async void Button_Clicked_4(object sender, EventArgs e)
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

                int cenabean = 400;

                bool vneseni = false;



                using (SqlCommand insertCommand = new SqlCommand("INSERT INTO dbo.poracka(id, loginid, poracka, brporacka,cena) VALUES (@id, @loginid, @poracka, @brporacka,@cena)", connection))
                {
                    insertCommand.Parameters.Add(new SqlParameter("id", Id));
                    insertCommand.Parameters.Add(new SqlParameter("loginid", fetchedLoginId));
                    insertCommand.Parameters.Add(new SqlParameter("poracka", Beanburgerr.Text));
                    insertCommand.Parameters.Add(new SqlParameter("brporacka", Broj25beanburger.Text));
                    insertCommand.Parameters.Add(new SqlParameter("cena", cenabean));


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

        private async void Button_Clicked_5(object sender, EventArgs e)
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

                int cenarice = 350;

                bool vneseni = false;



                using (SqlCommand insertCommand = new SqlCommand("INSERT INTO dbo.poracka(id, loginid, poracka, brporacka,cena) VALUES (@id, @loginid, @poracka, @brporacka,@cena)", connection))
                {
                    insertCommand.Parameters.Add(new SqlParameter("id", Id));
                    insertCommand.Parameters.Add(new SqlParameter("loginid", fetchedLoginId));
                    insertCommand.Parameters.Add(new SqlParameter("poracka", riceburgerr.Text));
                    insertCommand.Parameters.Add(new SqlParameter("brporacka", Broj26riceburger.Text));
                    insertCommand.Parameters.Add(new SqlParameter("cena", cenarice));


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