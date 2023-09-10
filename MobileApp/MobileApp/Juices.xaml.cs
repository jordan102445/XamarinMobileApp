using connectionDB;
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
using MobileApp;

namespace Delicious_app
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Juices : ContentPage
    {

        private List<string> juices;
        private string username;
        SqlConnection connection;
        public Juices(string username)
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

            juices = new List<string>
            {
                "Carbonatedjuices",
                "Milkshake",
                "FruitJuices",
                "Coffe"



            };

            UpdateJuices("");


        }


        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {

            var vnesi = e.NewTextValue;

            UpdateJuices(vnesi);


        }

        private void UpdateJuices(string vnesi)
        {

            var filter = juices.Where(option => option.ToLower().Contains(vnesi.ToLower())).ToList();

            DrinksFlexLayout.Children.Clear();

            foreach (var option in filter)
            {

                var frame = (Frame)FindByName(option.Replace(" ", string.Empty));

                if (frame != null)
                {

                    DrinksFlexLayout.Children.Add(frame);
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

                int cenacola = 70;
                bool vnesi = false;


                using (SqlCommand insertCommand = new SqlCommand("INSERT INTO dbo.poracka(id, loginid, poracka, brporacka,cena) VALUES (@id, @loginid, @poracka, @brporacka,@cena)", connection))
                {
                    insertCommand.Parameters.Add(new SqlParameter("id", Id));
                    insertCommand.Parameters.Add(new SqlParameter("loginid", fetchedLoginId));

                    if (cocacola.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", cocacola.Content));

                    }
                    else if (fanta.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", fanta.Content));

                    }
                    else if (pepsi.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", pepsi.Content));
                    }
                    else if (sprite.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", sprite.Content));
                    }

                    insertCommand.Parameters.Add(new SqlParameter("brporacka", BROJ27carbonated.Text));
                    insertCommand.Parameters.Add(new SqlParameter("cena", cenacola));


                    insertCommand.ExecuteNonQuery();
                    vnesi = true;

                    await DisplayAlert("Успешно", "Успешна порачка", "Ok");

                }

                if (vnesi)
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

                int cenamilkshake = 120;
                bool vnesi = false;


                using (SqlCommand insertCommand = new SqlCommand("INSERT INTO dbo.poracka(id, loginid, poracka, brporacka,cena) VALUES (@id, @loginid, @poracka, @brporacka,@cena)", connection))
                {
                    insertCommand.Parameters.Add(new SqlParameter("id", Id));
                    insertCommand.Parameters.Add(new SqlParameter("loginid", fetchedLoginId));

                    if (coko.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", coko.Content));

                    }
                    else if (vanila.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", vanila.Content));

                    }
                    else if (banana.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", banana.Content));
                    }

                    insertCommand.Parameters.Add(new SqlParameter("brporacka", BROJ28milk.Text));
                    insertCommand.Parameters.Add(new SqlParameter("cena", cenamilkshake));


                    insertCommand.ExecuteNonQuery();
                    vnesi = true;

                    await DisplayAlert("Успешно", "Успешна порачка", "Ok");

                }

                if (vnesi)
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

                int cenafruit = 90;
                bool vnesi = false;


                using (SqlCommand insertCommand = new SqlCommand("INSERT INTO dbo.poracka(id, loginid, poracka, brporacka,cena) VALUES (@id, @loginid, @poracka, @brporacka,@cena)", connection))
                {
                    insertCommand.Parameters.Add(new SqlParameter("id", Id));
                    insertCommand.Parameters.Add(new SqlParameter("loginid", fetchedLoginId));

                    if (portokal.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", portokal.Content));

                    }
                    else if (jabuka.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", jabuka.Content));

                    }
                    else if (limon.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", limon.Content));
                    }
                    else if (lobenica.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", lobenica.Content));
                    }

                    insertCommand.Parameters.Add(new SqlParameter("brporacka", BROJ29fruit.Text));
                    insertCommand.Parameters.Add(new SqlParameter("cena", cenafruit));


                    insertCommand.ExecuteNonQuery();
                    vnesi = true;

                    await DisplayAlert("Успешно", "Успешна порачка", "Ok");

                }

                if (vnesi)
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

                int cenacoffe = 80;
                bool vnesi = false;


                using (SqlCommand insertCommand = new SqlCommand("INSERT INTO dbo.poracka(id, loginid, poracka, brporacka,cena) VALUES (@id, @loginid, @poracka, @brporacka,@cena)", connection))
                {
                    insertCommand.Parameters.Add(new SqlParameter("id", Id));
                    insertCommand.Parameters.Add(new SqlParameter("loginid", fetchedLoginId));

                    if (esspreso.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", esspreso.Content));

                    }
                    else if (maciajato.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", maciajato.Content));

                    }
                    else if (kapucino.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", kapucino.Content));
                    }
                    else if (amerikano.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", amerikano.Content));
                    }

                    insertCommand.Parameters.Add(new SqlParameter("brporacka", BROJ30coffe.Text));
                    insertCommand.Parameters.Add(new SqlParameter("cena", cenacoffe));


                    insertCommand.ExecuteNonQuery();
                    vnesi = true;

                    await DisplayAlert("Успешно", "Успешна порачка", "Ok");

                }

                if (vnesi)
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