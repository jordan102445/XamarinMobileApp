using connectionDB;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        public static SqlConnection connection;

        public class dbelementi
        {
            public int registerid {get; set;}
            public string username { get; set;}
            public string email { get; set;}    
            public string password { get; set;}
            public string confirimpassword { get; set; }

        }
        public Register()
        {
            InitializeComponent();

            Connection dbConnection = new Connection();
            connection = dbConnection.GetDBConnection();
        }


       



            public static bool IsValidEmail(string email)
        {
           
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

           
            return Regex.IsMatch(email, pattern);
        }

        


        
        


        private async void POST_Clicked(object sender, EventArgs e)
        {
            using (SqlConnection connection = new Connection().GetDBConnection())
            {


                connection.Open();
                int Id = 0;
                using (SqlCommand command = new SqlCommand("SELECT MAX(registerid) FROM dbo.register", connection))
                {
                    var result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        Id = Convert.ToInt32(result) + 1;
                    }
                }


                int registerid = Id + 1;
                try
                {


                    using (SqlCommand commanda = new SqlCommand("INSERT INTO dbo.register (registerid,username, email, password, confirmpassword) VALUES (@registerid,@username,@email,@password,@confirmpassword)", connection))
                    {
                        commanda.Parameters.Add(new SqlParameter("registerid", registerid));

                        commanda.Parameters.Add(new SqlParameter("username", Username.Text));



                        if (IsValidEmail(Email.Text))
                        {
                            commanda.Parameters.Add(new SqlParameter("email", Email.Text));
                        }
                        else
                        {

                            await App.Current.MainPage.DisplayAlert("Неуспешна регистрација", "Внесете правилен емаил", "Ok");
                            return;
                        }


                        commanda.Parameters.Add(new SqlParameter("password", Password.Text));
                        if (Password.Text.Equals(ConfirmPassword.Text))
                        {

                            commanda.Parameters.Add(new SqlParameter("confirmpassword", ConfirmPassword.Text));

                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Неуспешна регистрација", "Лозинките не се совпаѓаат", "Ok");
                        }


                        commanda.ExecuteNonQuery();

                    }

                    await App.Current.MainPage.DisplayAlert("Успешно се регистриравте", "Вашите податоци се внесени", "Ok");

                }
                catch
                (Exception ex)
                {
                    connection.Close();
                    await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "Ok");

                }
            }



        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            Email.Text = string.Empty;
            Username.Text = string.Empty;
            Password.Text = string.Empty;
            ConfirmPassword.Text = string.Empty;

        }
    }
}




