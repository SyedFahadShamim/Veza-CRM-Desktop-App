using System.Windows;
using Simple.Data;
using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace Veza_Desktop
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        string username;
        string password;



        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-857EU07\SQLEXPRESS;Initial Catalog=VezaDB;Integrated Security=True");


        public LoginWindow()
        {
            InitializeComponent();


        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {

            username = Username.Text;
            password = Password.Password.ToString();


            if (username == "" && password == "")
            {
                MessageBox.Show("User Name and Password Incorrect");
            }

            else if ((username == "" && password != null) || (username != null && password == ""))
            {
                MessageBox.Show("User Name or Password is Missing");
            }



            else
            {
                dynamic DB = Database.OpenNamedConnection("VezaDB");


                try
                {


                    var res = DB.USP_LoginUser(@Username: username, @Password: password);


                    if (res != null)

                    {

                        TalkWindow win2 = new TalkWindow(username, password);

                        
                        MessageBox.Show("Login Successful", "Login", MessageBoxButton.OK, MessageBoxImage.Information);

                        win2.Show();

                        this.Close();

                    }

                    else if (res == -1)
                    {
                        MessageBox.Show("Incorrect User name and password");
                    }

                    else

                    {
                        MessageBox.Show("Email ID or Password Incorrect !");

                    }


                }
                catch (Exception ex)
                {
                }

            }



        }

    }
}
