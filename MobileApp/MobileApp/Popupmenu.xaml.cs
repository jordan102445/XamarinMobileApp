using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using connectionDB;
using Xamarin.CommunityToolkit;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using Xamarin.CommunityToolkit.Extensions;

namespace Delicious_app
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Popupmenu : Popup

    {

        private string username;
        SqlConnection connection;

        public class dbelementiporacka
        {
            public int id { get; set; }
            public int loginid { get; set; }
            public string poracka { get; set; }
            public string brporacka { get; set; }

            public int cena { get; set; }
        }

        public Popupmenu(string username)
        {

            InitializeComponent();
            this.username = username;

            Displaytheuser.Text = "Вашата порачка" + " " + username;

            Connection dbConnection = new Connection();
            connection = dbConnection.GetDBConnection();

            connection.Open();

            using (SqlCommand command = new SqlCommand("SELECT p.loginid, p.poracka, p.brporacka, p.cena " +
                                                       "FROM dbo.poracka AS p " +
                                                       "JOIN dbo.login AS l ON p.loginid = l.loginid " +
                                                       "WHERE l.username = @username", connection))

            {

                command.Parameters.AddWithValue("username", username);


                List<dbelementiporacka> poracka = new List<dbelementiporacka>();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        poracka.Add(new dbelementiporacka()
                        {
                           // id = (int)reader["id"],
                            poracka = reader["poracka"].ToString(),
                            brporacka = reader["brporacka"].ToString(),
                            cena = (int)reader["cena"],

                        });




                    }




                }

                int suma = 0;
                foreach (dbelementiporacka item in poracka)
                {

                    suma = item.cena + suma;
                }

                sumalabel.Text = "Вашата порачка изнесува" + " " + suma;


                connection.Close();

                lista.ItemsSource = poracka;






            }

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var target = new Payment(username);
            var navigation = Application.Current.MainPage.Navigation;
            navigation.ShowPopup(target);

            Displaytheuser.Text = string.Empty;





        }

        private void UpdateSuma(List<dbelementiporacka> porackaList)
        {
            int suma = porackaList.Sum(item => item.cena);
            sumalabel.Text = "Вашата порачка изнесува " + suma;
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {

            if(sender is Button button && button.CommandParameter is dbelementiporacka obrokKupuvac)
            {

                var porackaList = (List<dbelementiporacka>)lista.ItemsSource;
                porackaList.Remove(obrokKupuvac);

                using (SqlConnection connection2 = new Connection().GetDBConnection())
                {
                    connection2.Open();

                    using (SqlCommand command2 = new SqlCommand("DELETE FROM dbo.poracka " +
                            "WHERE loginid = (SELECT loginid FROM dbo.login WHERE username = @username)" +
                            "AND brporacka = @brporacka", connection2))
                    {
                        command2.Parameters.AddWithValue("username", username);
                        command2.Parameters.AddWithValue("brporacka",obrokKupuvac.brporacka);
                        //command2.Parameters.AddWithValue("id", obrokKupuvac.id);
                        command2.ExecuteNonQuery(); // Execute the DELETE statement
                    }



                }

                

                lista.ItemsSource = null;
                lista.ItemsSource = porackaList;

                UpdateSuma(porackaList);
            }

            
        }
    }
}