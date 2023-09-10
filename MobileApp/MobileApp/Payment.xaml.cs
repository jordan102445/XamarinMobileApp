using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using connectionDB;
using Xamarin.CommunityToolkit;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using MobileApp;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

namespace Delicious_app
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Payment : Popup
    {

        private string username;
        SqlConnection connection;
        public Payment(string username)
        {
            InitializeComponent();

            this.username = username;
            usernamelabel.Text = "Дел за плаќање на : " + username;

            Connection dbConnection = new Connection();
            connection = dbConnection.GetDBConnection();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            using (SqlConnection connection = new Connection().GetDBConnection())
            {
                connection.Open();

                int id = 0;
                using (SqlCommand command = new SqlCommand("SELECT MAX(id) FROM dbo.plakanja", connection))
                {
                    var result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        id = Convert.ToInt32(result) + 1;
                    }
                }


                int porackaid = id + 1;
                bool vnesi = false;
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

                using (SqlCommand command1 = new SqlCommand("INSERT INTO dbo.plakanja (id,ime,prezime,brkarticka,cvc,date,address,telbroj,loginid) VALUES (@id,@ime,@prezime,@brkarticka,@cvc,@date,@address,@telbroj,@loginid)", connection))
                {

                    command1.Parameters.Add(new SqlParameter("id", porackaid));

                    command1.Parameters.Add(new SqlParameter("ime", name.Text));

                    command1.Parameters.Add(new SqlParameter("prezime", lastname.Text));


                    if (cardnumber.Text.Length == 16)
                    {
                        command1.Parameters.Add(new SqlParameter("brkarticka",cardnumber.Text));
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Внесете го правилно бројот на картичка", "Ok");
                    }
                    //int.TryParse(cvc.Text, out int cvcnumber)
                    if (cvc.Text.Length == 3)
                    {
                        command1.Parameters.Add(new SqlParameter("cvc", cvc.Text));
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Внесете го правилно вашиот пин", "Ok");
                    }

                    command1.Parameters.Add(new SqlParameter("address", address.Text));
                    command1.Parameters.Add(new SqlParameter("telbroj", telbroj.Text));
                    command1.Parameters.Add(new SqlParameter("loginid", fetchedLoginId));

                    DateTime selectedDate = dmy.Date;
                    
                     string selectedDateText = selectedDate.ToString(dmy.Format);
                    
                    //command1.Parameters.Add(new SqlParameter("date", System.Data.SqlDbType).Value = selectedDate;
                    command1.Parameters.Add(new SqlParameter("date",selectedDate));
                    command1.ExecuteNonQuery();
                    vnesi = true;




                }

                if (vnesi) {

                    await App.Current.MainPage.DisplayAlert("Успешно плаќање", "Наздравје јадење", "Ok");

                    name.Text = string.Empty;
                    lastname.Text = string.Empty;
                    cardnumber.Text = string.Empty;
                    cvc.Text = string.Empty;
                    DateTime selectedDate = dmy.Date;
                    string selectedDateText = selectedDate.ToString(dmy.Format);
                    selectedDateText = string.Empty;

                   


                   // using (SqlCommand command = new SqlCommand("DELETE FROM dbo.poracka WHERE loginid IN (SELECT loginid FROM dbo.login WHERE username = @username)", connection))
                    //{
                       // command.Parameters.Add(new SqlParameter("@username", username));
                        //command.ExecuteNonQuery();
                    //}


                   //using (SqlCommand command1 = new SqlCommand("DELETE FROM dbo.plakanja", connection))
                    //{
                       //command1.ExecuteNonQuery();
                  // }





                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Неуспешно плаќање", "Обидете се повторно", "Ok");
                }


                
                

            }



            

        }

       


    }
}