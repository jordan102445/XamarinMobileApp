using connectionDB;
using Delicious_app;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pizzapage : ContentPage
    {
        private List<string> originalPizzaOptions;

        private SqlConnection connection;
        private string username;

        public class dbelementilogin
        {
            public int loginID { get; set; }
            public string usernamelogin { get; set; }
            public string passwordlogin { get; set; }
        }



        public Pizzapage(string username)
        {
            InitializeComponent();
            this.username = username;
            usernamelabel.Text = "Наздравје јадење , " + username ;


            MessagingCenter.Subscribe<MenuSposnorsPage>(this, "UserLoggedOut", (sender) =>
            {
                // Clear the DisplayUsernameLabel.Text when the message is received
                usernamelabel.Text = string.Empty;
            });

            Connection dbConnection = new Connection();
            connection = dbConnection.GetDBConnection();

           


            originalPizzaOptions = new List<string>
            {
                "CheesePizza",
                "VeggiePizza",
                "PepperoniPizza",
                "MargheritaPizza",
                "BuffaloPizza",
                "BBQChickenPizza"



            };


            UpdatePizzaOptions("");


        }

       

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {

            var keyword = e.NewTextValue;


            UpdatePizzaOptions(keyword);
        }

        private void UpdatePizzaOptions(string keyword)
        {

            var filteredOptions = originalPizzaOptions
                .Where(option => option.ToLower().Contains(keyword.ToLower()))
                .ToList();


            pizzaFlexLayout.Children.Clear();


            foreach (var option in filteredOptions)
            {

                var frame = (Frame)FindByName(option.Replace(" ", string.Empty));


                if (frame != null)
                {
                    pizzaFlexLayout.Children.Add(frame);
                }
            }
        }

        private async  void Button_Clicked(object sender, EventArgs e)
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

                int cenacheese = 300;

                bool vneseni = false;



                using (SqlCommand insertCommand = new SqlCommand("INSERT INTO dbo.poracka(id, loginid, poracka, brporacka,cena) VALUES (@id, @loginid, @poracka, @brporacka,@cena)", connection))
                {
                    insertCommand.Parameters.Add(new SqlParameter("id", Id));
                    insertCommand.Parameters.Add(new SqlParameter("loginid", fetchedLoginId));
                    insertCommand.Parameters.Add(new SqlParameter("poracka", CheesePica.Text));
                    insertCommand.Parameters.Add(new SqlParameter("brporacka", BROJ5Cheese.Text));
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
                    insertCommand.Parameters.Add(new SqlParameter("poracka", VeggiePica.Text));
                    insertCommand.Parameters.Add(new SqlParameter("brporacka", BROJ6Veggie.Text));
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

                int cenapeperoni = 330;
                bool vneseni = false;



                using (SqlCommand insertCommand = new SqlCommand("INSERT INTO dbo.poracka(id, loginid, poracka, brporacka,cena) VALUES (@id, @loginid, @poracka, @brporacka,@cena)", connection))
                {
                    insertCommand.Parameters.Add(new SqlParameter("id", Id));
                    insertCommand.Parameters.Add(new SqlParameter("loginid", fetchedLoginId));
                    insertCommand.Parameters.Add(new SqlParameter("poracka", Peperonipica.Text));
                    insertCommand.Parameters.Add(new SqlParameter("brporacka", BROJ7Peperoni.Text));
                    insertCommand.Parameters.Add(new SqlParameter("cena", cenapeperoni));


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

                int cenamargarita = 340;
                bool vneseni = false;



                using (SqlCommand insertCommand = new SqlCommand("INSERT INTO dbo.poracka(id, loginid, poracka, brporacka,cena) VALUES (@id, @loginid, @poracka, @brporacka,@cena)", connection))
                {
                    insertCommand.Parameters.Add(new SqlParameter("id", Id));
                    insertCommand.Parameters.Add(new SqlParameter("loginid", fetchedLoginId));
                    insertCommand.Parameters.Add(new SqlParameter("poracka", Margatirapica.Text));
                    insertCommand.Parameters.Add(new SqlParameter("brporacka", BROJ8Margatira.Text));
                    insertCommand.Parameters.Add(new SqlParameter("cena", cenamargarita));


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

                int cenabuffalo = 360;
                bool vneseni = false;



                using (SqlCommand insertCommand = new SqlCommand("INSERT INTO dbo.poracka(id, loginid, poracka, brporacka,cena) VALUES (@id, @loginid, @poracka, @brporacka,@cena)", connection))
                {
                    insertCommand.Parameters.Add(new SqlParameter("id", Id));
                    insertCommand.Parameters.Add(new SqlParameter("loginid", fetchedLoginId));
                    insertCommand.Parameters.Add(new SqlParameter("poracka", Buffalopica.Text));
                    insertCommand.Parameters.Add(new SqlParameter("brporacka", BROJ9Buffalo.Text));
                    insertCommand.Parameters.Add(new SqlParameter("cena", cenabuffalo));


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

                int cenabbqchicken = 350;
                bool vneseni = false;



                using (SqlCommand insertCommand = new SqlCommand("INSERT INTO dbo.poracka(id, loginid, poracka, brporacka,cena) VALUES (@id, @loginid, @poracka, @brporacka,@cena)", connection))
                {
                    insertCommand.Parameters.Add(new SqlParameter("id", Id));
                    insertCommand.Parameters.Add(new SqlParameter("loginid", fetchedLoginId));
                    insertCommand.Parameters.Add(new SqlParameter("poracka", bbqlchickenpica.Text));
                    insertCommand.Parameters.Add(new SqlParameter("brporacka", BROJ10bbqchicken.Text));
                    insertCommand.Parameters.Add(new SqlParameter("cena", cenabbqchicken));


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