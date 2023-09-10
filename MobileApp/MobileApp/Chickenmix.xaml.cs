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
    public partial class Chickenmix : ContentPage
    {

        private string username;
        SqlConnection connection;

        private List<string> chickenOptions;
        public Chickenmix(string username)
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


            chickenOptions = new List<string>
            {
                "Chickenwings",
                "Chickenbreasts",
                "Chickendrumsticks",
                "Chickenribs",
                "Chickenthighs"




            };

            UpdateChickedOptions("");
        }



        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var vnesi = e.NewTextValue;

            UpdateChickedOptions(vnesi);

        }

        private void UpdateChickedOptions(string vnesi)
        {

            var filter = chickenOptions.Where(option => option.ToLower().Contains(vnesi.ToLower())).ToList();

            ChickenFlexLayout.Children.Clear();

            foreach (var option in filter)
            {

                var frame = (Frame)FindByName(option.Replace(" ",string.Empty));


                if(frame != null) { 
                

                    ChickenFlexLayout.Children.Add(frame);
                
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

                int cenawings = 300;
                bool vnesi = false;


                using (SqlCommand insertCommand = new SqlCommand("INSERT INTO dbo.poracka(id, loginid, poracka, brporacka,cena) VALUES (@id, @loginid, @poracka, @brporacka,@cena)", connection))
                {
                    insertCommand.Parameters.Add(new SqlParameter("id", Id));
                    insertCommand.Parameters.Add(new SqlParameter("loginid", fetchedLoginId));

                    if (crispi.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", crispi.Content));

                    }
                    else if (medlukchicken.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", medlukchicken.Content));

                    }
                    else if (ljutipileskikrilca.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", ljutipileskikrilca.Content));
                    }

                    insertCommand.Parameters.Add(new SqlParameter("brporacka", BROJ31chickenwings.Text));
                    insertCommand.Parameters.Add(new SqlParameter("cena", cenawings));


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

                int cenabreasts = 280;

                bool vneseni = false;



                using (SqlCommand insertCommand = new SqlCommand("INSERT INTO dbo.poracka(id, loginid, poracka, brporacka,cena) VALUES (@id, @loginid, @poracka, @brporacka,@cena)", connection))
                {
                    insertCommand.Parameters.Add(new SqlParameter("id", Id));
                    insertCommand.Parameters.Add(new SqlParameter("loginid", fetchedLoginId));
                    insertCommand.Parameters.Add(new SqlParameter("poracka", chickenbreastss.Text));
                    insertCommand.Parameters.Add(new SqlParameter("brporacka", BROJ32chickenbreasts.Text));
                    insertCommand.Parameters.Add(new SqlParameter("cena", cenabreasts));


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

                int cenadrumsticks = 230;
                bool vnesi = false;


                using (SqlCommand insertCommand = new SqlCommand("INSERT INTO dbo.poracka(id, loginid, poracka, brporacka,cena) VALUES (@id, @loginid, @poracka, @brporacka,@cena)", connection))
                {
                    insertCommand.Parameters.Add(new SqlParameter("id", Id));
                    insertCommand.Parameters.Add(new SqlParameter("loginid", fetchedLoginId));

                    if (sokokoska.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", sokokoska.Content));

                    }
                    else if (somisirka.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", somisirka.Content));

                    }
                    else if (sopatka.IsChecked)
                    {
                        insertCommand.Parameters.Add(new SqlParameter("poracka", sopatka.Content));
                    }

                    insertCommand.Parameters.Add(new SqlParameter("brporacka", BROJ33chickendrumsticks.Text));
                    insertCommand.Parameters.Add(new SqlParameter("cena", cenadrumsticks));


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

                int cenaribs = 300;

                bool vneseni = false;



                using (SqlCommand insertCommand = new SqlCommand("INSERT INTO dbo.poracka(id, loginid, poracka, brporacka,cena) VALUES (@id, @loginid, @poracka, @brporacka,@cena)", connection))
                {
                    insertCommand.Parameters.Add(new SqlParameter("id", Id));
                    insertCommand.Parameters.Add(new SqlParameter("loginid", fetchedLoginId));
                    insertCommand.Parameters.Add(new SqlParameter("poracka", chickenribss.Text));
                    insertCommand.Parameters.Add(new SqlParameter("brporacka", BROJ34chickenribs.Text));
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

                int cenathighs = 320;

                bool vneseni = false;



                using (SqlCommand insertCommand = new SqlCommand("INSERT INTO dbo.poracka(id, loginid, poracka, brporacka,cena) VALUES (@id, @loginid, @poracka, @brporacka,@cena)", connection))
                {
                    insertCommand.Parameters.Add(new SqlParameter("id", Id));
                    insertCommand.Parameters.Add(new SqlParameter("loginid", fetchedLoginId));
                    insertCommand.Parameters.Add(new SqlParameter("poracka", chickenthighss.Text));
                    insertCommand.Parameters.Add(new SqlParameter("brporacka", BROJ35chickenthighs.Text));
                    insertCommand.Parameters.Add(new SqlParameter("cena", cenathighs));


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