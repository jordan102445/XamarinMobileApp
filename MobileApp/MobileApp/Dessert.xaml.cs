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
    public partial class Dessert : ContentPage
    {
        private string username;
        private List<string> DessertOption;
        private SqlConnection connection;




        public Dessert(string username)
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


            DessertOption = new List<string>
            {
                "Applepie",
                "Cheesecake",
                "Baklava",
                "Cake",
                "Icecream",
                "Cookies"





            };

            UpdateDessert("");
        }

        private void SearchBar_TextChanged(object sender,TextChangedEventArgs e)
        {
            var vnesi = e.NewTextValue;
            UpdateDessert(vnesi);

        }


        private void UpdateDessert(string vnesi)
        {

            var filter = DessertOption.Where(option => option.ToLower().Contains(vnesi.ToLower())).ToList();

            DessertFlexLayout.Children.Clear();

            foreach (var option in filter)
            {
                var frame =(Frame)FindByName(option.Replace(" ",string.Empty));

                if(frame != null)
                {
                    DessertFlexLayout.Children.Add(frame);
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

                int cenaapplepie = 140;
                bool vnesi = false;


                using (SqlCommand insertCommand = new SqlCommand("INSERT INTO dbo.poracka(id, loginid, poracka, brporacka,cena) VALUES (@id, @loginid, @poracka, @brporacka,@cena)", connection))
                {
                    insertCommand.Parameters.Add(new SqlParameter("id", Id));
                    insertCommand.Parameters.Add(new SqlParameter("loginid", fetchedLoginId));

                    if (Чоколадна.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", Чоколадна.Content));

                    }
                    else if (Овошна.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", Овошна.Content));

                    }
                    else if (Солена.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", Солена.Content));
                    }

                    insertCommand.Parameters.Add(new SqlParameter("brporacka", BROJ15applepie.Text));
                    insertCommand.Parameters.Add(new SqlParameter("cena", cenaapplepie));


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

                int cenaacheese = 120;
                bool vnesi = false;

                using (SqlCommand insertCommand = new SqlCommand("INSERT INTO dbo.poracka(id, loginid, poracka, brporacka,cena) VALUES (@id, @loginid, @poracka, @brporacka,@cena)", connection))
                {
                    insertCommand.Parameters.Add(new SqlParameter("id", Id));
                    insertCommand.Parameters.Add(new SqlParameter("loginid", fetchedLoginId));

                    if (iris.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", iris.Content));

                    }
                    else if (Cokoladnaa.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", Cokoladnaa.Content));

                    }
                    else if (Vegeterjanska.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", Vegeterjanska.Content));
                    }

                    insertCommand.Parameters.Add(new SqlParameter("brporacka", BROJ16cheese.Text));
                    insertCommand.Parameters.Add(new SqlParameter("cena", cenaacheese));


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

                int cenaabaklava = 90;
                bool vnesi = false;

                using (SqlCommand insertCommand = new SqlCommand("INSERT INTO dbo.poracka(id, loginid, poracka, brporacka,cena) VALUES (@id, @loginid, @poracka, @brporacka,@cena)", connection))
                {
                    insertCommand.Parameters.Add(new SqlParameter("id", Id));
                    insertCommand.Parameters.Add(new SqlParameter("loginid", fetchedLoginId));

                    if (cokoladnabaklava.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", cokoladnabaklava.Content));

                    }
                    else if (fstci.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", fstci.Content));

                    }
                    else if (solena.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", solena.Content));
                    }

                    insertCommand.Parameters.Add(new SqlParameter("brporacka", BROJ17baklava.Text));
                    insertCommand.Parameters.Add(new SqlParameter("cena", cenaabaklava));


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

                int cenatorta = 140;
                bool vnesi = false;

                using (SqlCommand insertCommand = new SqlCommand("INSERT INTO dbo.poracka(id, loginid, poracka, brporacka,cena) VALUES (@id, @loginid, @poracka, @brporacka,@cena)", connection))
                {
                    insertCommand.Parameters.Add(new SqlParameter("id", Id));
                    insertCommand.Parameters.Add(new SqlParameter("loginid", fetchedLoginId));

                    if (cokoladnatorta.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", cokoladnatorta.Content));

                    }
                    else if (ovosna.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", ovosna.Content));

                    }
                    else if (oreotorta.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", oreotorta.Content));
                    }

                    insertCommand.Parameters.Add(new SqlParameter("brporacka", BROJ18cake.Text));
                    insertCommand.Parameters.Add(new SqlParameter("cena", cenatorta));


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

                int cenasladolen = 100;
                bool vnesi = false;

                using (SqlCommand insertCommand = new SqlCommand("INSERT INTO dbo.poracka(id, loginid, poracka, brporacka,cena) VALUES (@id, @loginid, @poracka, @brporacka,@cena)", connection))
                {
                    insertCommand.Parameters.Add(new SqlParameter("id", Id));
                    insertCommand.Parameters.Add(new SqlParameter("loginid", fetchedLoginId));

                    if (cokoladensladolen.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", cokoladensladolen.Content));

                    }
                    else if (ovosensladolen.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", ovosensladolen.Content));

                    }
                    else if (vanilasladolen.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", vanilasladolen.Content));
                    }

                    insertCommand.Parameters.Add(new SqlParameter("brporacka", BROJ19icecream.Text));
                    insertCommand.Parameters.Add(new SqlParameter("cena", cenasladolen));


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

                int cenacookies = 60;
                bool vnesi = false;

                using (SqlCommand insertCommand = new SqlCommand("INSERT INTO dbo.poracka(id, loginid, poracka, brporacka,cena) VALUES (@id, @loginid, @poracka, @brporacka,@cena)", connection))
                {
                    insertCommand.Parameters.Add(new SqlParameter("id", Id));
                    insertCommand.Parameters.Add(new SqlParameter("loginid", fetchedLoginId));

                    if (cokoladnicookies.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", cokoladnicookies.Content));

                    }
                    else if (visnacookies.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", visnacookies.Content));

                    }
                    else if (oreocookies.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", oreocookies.Content));
                    }

                    insertCommand.Parameters.Add(new SqlParameter("brporacka", BROJ20cookies.Text));
                    insertCommand.Parameters.Add(new SqlParameter("cena", cenacookies));


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
