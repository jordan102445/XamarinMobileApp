using Xamarin.Forms;
using System.Data.SqlClient;

using System.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Data.Common;

namespace MobileApp
{
    public partial class Login : ContentPage
    {
        private SqlConnection connection;

        // Add a variable to store the user token
        private const string UserTokenKey = "user_token";





        public class dbelementi
        {
            public int loginid { get; set; }
            public string username { get; set; }
            public string password { get; set; }
        }

        public class dbelementilogin
        {
            public int loginID { get; set; }
            public string usernamelogin { get; set; }
            public string passwordlogin { get; set; }
        }

        public Login()
        {
            InitializeComponent();
            //Connection dbConnection = new Connection();
           // connection = dbConnection.GetDBConnection();

            // CheckAndHandleUserSession();
        }

       

        private async void Button_Clicked_1(object sender, EventArgs e)
        {


           try

            {
                

                string sqlconnection = "Data Source=192.168.1.101;Initial Catalog=DeliciousAppdb;User Id='Jordan';Password='jocejoce2';";


                
                    using (SqlConnection connection = new SqlConnection(sqlconnection))
                    {


                        connection.Open();



                        List<dbelementi> mytable = new List<dbelementi>();

                        SqlCommand querypost = new SqlCommand("SELECT username, password FROM dbo.register", connection);
                        SqlDataReader reader = querypost.ExecuteReader();
                        while (reader.Read())
                        {
                            mytable.Add(new dbelementi
                            {
                                password = reader["password"].ToString(),
                                username = reader["username"].ToString(),
                            });
                        }
                        reader.Close();



                        List<dbelementilogin> logintable = new List<dbelementilogin>();
                        SqlCommand querypostlogin = new SqlCommand("SELECT loginid,username, password FROM dbo.login", connection);
                        SqlDataReader readerlogin = querypostlogin.ExecuteReader();
                        while (readerlogin.Read())
                        {
                            logintable.Add(new dbelementilogin
                            {
                                loginID = (int)readerlogin["loginid"],
                                passwordlogin = readerlogin["password"].ToString(),
                                usernamelogin = readerlogin["username"].ToString(),
                            });
                        }
                        readerlogin.Close();

                        bool isUserRegistered = false;

                        foreach (dbelementi item in mytable)
                        {
                            if (item.username.Equals(Username.Text))
                            {


                                isUserRegistered = true;

                                break;

                            }
                        }

                        if (!isUserRegistered)
                        {
                            await DisplayAlert("Грешка", "Невалидно корисничко име (корисникот не е регистриран)", "Ok");
                            return;
                        }

                        bool loginran = false;
                        foreach (dbelementilogin elementi in logintable)
                        {
                            if (elementi.passwordlogin.Equals(Password.Text) && elementi.usernamelogin.Equals(Username.Text))
                            {
                                loginran = true;
                                break;
                            }
                        }

                        if (loginran)
                        {
                            await DisplayAlert("Успешна најава", "Вие сте логирани продолжете со нарачката.", "Ok");



                            await SecureStorage.SetAsync(UserTokenKey, "user_logged_in");


                            string token = await SecureStorage.GetAsync(UserTokenKey);



                            var target = new MenuSposnorsPage(Username.Text, token);
                            var navigation = Application.Current.MainPage.Navigation;
                            await navigation.PushAsync(target);



                        }
                        else if (isUserRegistered && !loginran)
                        {
                            using (SqlCommand queryinsert = new SqlCommand("INSERT INTO dbo.login(loginid, username, password) VALUES (@loginid, @username, @password)", connection))
                            {

                                int loginid = logintable.Count + 1;

                                queryinsert.Parameters.Add(new SqlParameter("loginid", loginid));
                                queryinsert.Parameters.Add(new SqlParameter("username", Username.Text));
                                queryinsert.Parameters.Add(new SqlParameter("password", Password.Text));
                                queryinsert.ExecuteNonQuery();

                                await DisplayAlert("Успешна најава", "Вие сте логирани продолжете со нарачката.", "Ok");

                                await SecureStorage.SetAsync(UserTokenKey, "user_logged_in");
                                string token = await SecureStorage.GetAsync(UserTokenKey);

                                var target = new MenuSposnorsPage(Username.Text, token);
                                var navigation = Application.Current.MainPage.Navigation;
                                await navigation.PushAsync(target);
                            }
                        }
                        else
                        {
                            await DisplayAlert("Грешка", "Невалидно корисничко име или лозинка", "Ok");

                        }





                    }
                

            }

            catch (Exception ex)
            {
                await DisplayAlert("Alert", ex.Message, "Ok");
            }
        }


        
       
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            Username.Text = string.Empty;
            Password.Text = string.Empty;
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            var target = new Register();
            var navigation = Application.Current.MainPage.Navigation;
            navigation.PushAsync(target);

        }

        //private async void CheckAndHandleUserSession()
        //{
        // Check if the flag exists in SecureStorage
        // if (await SecureStorage.GetAsync(UserTokenKey) == "user_logged_in")
        // {
        // User is logged in, navigate to the next page
        //var target = new Basket(Username.Text);
        //var navigation = Application.Current.MainPage.Navigation;
        // await navigation.PushAsync(target);
        // }
        //}
    }
}
