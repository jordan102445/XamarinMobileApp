using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using connectionDB;
using System.Data.SqlClient;

namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Contact : ContentPage
    {
        SqlConnection connection;
        public Contact()
        {
            InitializeComponent();

            //Connection dbConnection = new Connection();
           // connection = dbConnection.GetDBConnection();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            using (SqlConnection connection = new Connection().GetDBConnection())
            {
                connection.Open();

                int Idcontact = 0;
                using (SqlCommand command = new SqlCommand("SELECT MAX(id) FROM dbo.contact", connection))
                {
                    var result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        Idcontact = Convert.ToInt32(result);
                    }
                }







                int Id = Idcontact + 1;

                using (SqlCommand insertconctact = new SqlCommand("INSERT INTO dbo.contact(id,ime,prezime,predmet,poraka) VALUES (@id,@ime,@prezime,@predmet,@poraka)", connection))
                {
                    insertconctact.Parameters.Add(new SqlParameter("id", Id));

                    insertconctact.Parameters.Add(new SqlParameter("ime", NameEntry.Text));

                    insertconctact.Parameters.Add(new SqlParameter("prezime", LastNameEntry.Text));

                    insertconctact.Parameters.Add(new SqlParameter("predmet",SubjectEntry.Text));

                    insertconctact.Parameters.Add(new SqlParameter("poraka",MessageEditor.Text));

                    insertconctact.ExecuteNonQuery();

                    await DisplayAlert("Успешно", "Пораката е пратена", "Ok");


                }


            }
        }
    }
}