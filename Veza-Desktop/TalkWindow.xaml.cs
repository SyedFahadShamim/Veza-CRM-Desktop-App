using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Win32;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Windows.Threading;
using Simple.Data;

namespace Veza_Desktop
{

    public partial class TalkWindow : Window
    {


        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-857EU07\SQLEXPRESS;Initial Catalog=VezaDB;Integrated Security=True");
        SqlDataReader reader1, reader2;
        string strName, imageName;
        string constr = @"Data Source=DESKTOP-857EU07\SQLEXPRESS;Initial Catalog=tbl_Image;Integrated Security=True";

        static string output1 = "";

        SqlConnection tblConn = new SqlConnection(@"Data Source = DESKTOP - 857EU07\SQLEXPRESS; Initial Catalog = tbl_Image; Integrated Security = True");

        MsgWindow msgWin = new MsgWindow();

        public string getData = "";


        string username;
        string password;



        public TalkWindow()
        {
            Loaded += TalkWindow_Loaded;
            InitializeComponent();
            TripleBarBtn1.Visibility = Visibility.Hidden;
            CustomerCareGrid.Visibility = Visibility.Hidden;
            NotificationGrid.Visibility = Visibility.Hidden;
            InboxGrid.Visibility = Visibility.Hidden;
            PauseGrid.Visibility = Visibility.Hidden;
            UserProfileGrid.Visibility = Visibility.Hidden;
            ComposeGrid.Visibility = Visibility.Hidden;

            DBBtn.IsChecked = true;


        }


        public TalkWindow(string username, string password)
        {
            this.username = username;
            this.password = password;

            Loaded += TalkWindow_Loaded;
            InitializeComponent();
            TripleBarBtn1.Visibility = Visibility.Hidden;
            CustomerCareGrid.Visibility = Visibility.Hidden;
            NotificationGrid.Visibility = Visibility.Hidden;
            InboxGrid.Visibility = Visibility.Hidden;
            PauseGrid.Visibility = Visibility.Hidden;
            UserProfileGrid.Visibility = Visibility.Hidden;
            ComposeGrid.Visibility = Visibility.Hidden;
            DBBtn.IsChecked = true;
            Usernamelbl.Content = username.ToString();
            AgentName.Content = username;




        }

        static TcpClient client;
        static Thread thread;

        private void TalkWindow_Loaded(object sender, RoutedEventArgs e)
        {

            talkToAdmin.Visibility = Visibility.Hidden;


            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = 5000;

            client = new TcpClient();
            
            
            client.Connect(ip, port);

            NetworkStream ns = client.GetStream();
            thread = new Thread(o => ReceiveData((TcpClient)o));

            thread.Start(client);

            
            conn.Open();

            using (SqlCommand cmd1 = new SqlCommand("select u.Id from [User] u where u.Username = @username", conn))
            {

                cmd1.CommandType = CommandType.Text;
                cmd1.Parameters.AddWithValue("@username", username);
                reader1 = cmd1.ExecuteReader();

                while (reader1.Read())
                {

                    output1 = output1 + reader1.GetValue(0).ToString();

                }
                reader1.Close();
            }

            conn.Close();



            dynamic DB = Database.OpenNamedConnection("VezaDB");

            var res = DB.USP_GetDashboard(@Id: Convert.ToInt32(output1));

            foreach (var index in res)
            {
                totalCalls.Content = index.TotalCalls;
                acceptedCalls.Content = index.AcceptedCalls;
                rejectedCalls.Content = index.RejectedCalls;
                dailyCalls.Content = index.DailyCalls;
            }
        }

        static NetworkStream ns;

        static void ReceiveData(TcpClient client)
        {
            try
            {

                ns = client.GetStream();
                byte[] receivedBytes = new byte[1024];
                int byte_count;

                while ((byte_count = ns.Read(receivedBytes, 0, receivedBytes.Length)) > 0)
                {
                    Console.Write(Encoding.ASCII.GetString(receivedBytes, 0, byte_count));
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void sendbutton1_Click(object sender, RoutedEventArgs e)
        {
            getData = textBox2.Text.ToString();
            string s;

            while (!string.IsNullOrEmpty((s = getData.ToString())))
            {
                byte[] buffer = Encoding.ASCII.GetBytes(s);
                ns.Write(buffer, 0, buffer.Length);
                break;

            }


            textBox1.Text += "Agent : " + getData + "\r\n";
            textBox2.Text = "";

        }

        private void talkWithCustomer_Click(object sender, RoutedEventArgs e)
        {
            msgWin.Show();

        }


        #region menubutonlar
        int CheckedButton = 1;


        private void Dashboard_Click_Checked(object sender, RoutedEventArgs e)
        {
            UserProfileGrid.Visibility = Visibility.Hidden;
            DashboardGrid.Visibility = Visibility.Visible;
            InboxGrid.Visibility = Visibility.Hidden;
            CustomerCareGrid.Visibility = Visibility.Hidden;
            NotificationGrid.Visibility = Visibility.Hidden;
            PauseGrid.Visibility = Visibility.Hidden;
            ComposeGrid.Visibility = Visibility.Hidden;
            var signalChange = signal_lbl as Ellipse;
            signalChange.Fill = Brushes.Green;
            CheckedButton = 5;
            CheckedStatus();
            Dashboard.Visibility = Visibility.Visible;

            talkToAdmin.Visibility = Visibility.Hidden;

        }
        void CheckedStatus()
        {

            if (CheckedButton == 1)
            {
                LogoutBtn.IsChecked = true;
            }
            else
            {
                LogoutBtn.IsChecked = false;
            }
            if (CheckedButton == 2)
            {
                PauseBtn.IsChecked = true;
            }
            else
            {
                PauseBtn.IsChecked = false;
            }
            if (CheckedButton == 3)
            {
                ACBtn.IsChecked = true;
            }
            else
            {
                ACBtn.IsChecked = false;
            }
            if (CheckedButton == 4)
            {
                CCBtn.IsChecked = true;
            }
            else
            {
                CCBtn.IsChecked = false;
            }
            if (CheckedButton == 5)
            {
                DBBtn.IsChecked = true;
            }
            else
            {
                DBBtn.IsChecked = false;
            }

        }

        private void CC_Chat_Click_Checked(object sender, RoutedEventArgs e)
        {
            UserProfileGrid.Visibility = Visibility.Hidden;
            CustomerCareGrid.Visibility = Visibility.Visible;
            NotificationGrid.Visibility = Visibility.Visible;
            DashboardGrid.Visibility = Visibility.Hidden;
            InboxGrid.Visibility = Visibility.Hidden;
            PauseGrid.Visibility = Visibility.Hidden;
            ComposeGrid.Visibility = Visibility.Hidden;
            var signalChange = signal_lbl as Ellipse;
            signalChange.Fill = Brushes.Green;
            CheckedButton = 4;
            CheckedStatus();
            Dashboard.Visibility = Visibility.Hidden;


        }

        private void TalkWithAdmin_Checked(object sender, RoutedEventArgs e)
        {
            UserProfileGrid.Visibility = Visibility.Hidden;
            CustomerCareGrid.Visibility = Visibility.Hidden;
            NotificationGrid.Visibility = Visibility.Hidden;
            DashboardGrid.Visibility = Visibility.Hidden;
            InboxGrid.Visibility = Visibility.Visible;
            PauseGrid.Visibility = Visibility.Hidden;
            ComposeGrid.Visibility = Visibility.Hidden;
            var signalChange = signal_lbl as Ellipse;
            signalChange.Fill = Brushes.Green;
            CheckedButton = 3;
            CheckedStatus();

            talkToAdmin.Visibility = Visibility.Visible;


        }

        private void Pause_Click_Checked(object sender, RoutedEventArgs e)
        {
            CustomerCareGrid.Visibility = Visibility.Hidden;
            NotificationGrid.Visibility = Visibility.Hidden;
            DashboardGrid.Visibility = Visibility.Hidden;
            InboxGrid.Visibility = Visibility.Hidden;
            PauseGrid.Visibility = Visibility.Visible;
            ComposeGrid.Visibility = Visibility.Hidden;
            UserProfileGrid.Visibility = Visibility.Hidden;
            active_lbl.Content = "PAUSE";
            var signalChange = signal_lbl as Ellipse;
            signalChange.Fill = Brushes.Yellow;
            CheckedButton = 2;
            CheckedStatus();
            Dashboard.Visibility = Visibility.Hidden;

            dynamic DB = Database.OpenNamedConnection("VezaDB");

            DB.USP_PauseAgent(@UserId: output1);

        }

        private void LogoutBtn_Click_Checked(object sender, RoutedEventArgs e)
        {


            CheckedButton = 1;
            CheckedStatus();
            active_lbl.Content = "NON-ACTIVE";
            var signalChange = signal_lbl as Ellipse;
            signalChange.Fill = Brushes.Red;
            Dashboard.Visibility = Visibility.Hidden;

            conn.Open();

            using (SqlCommand cmd2 = new SqlCommand("USP_LogoutUser", conn))
            {

                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@UserId", SqlDbType.Int).Value = Convert.ToInt32(output1);

                var isLogout = cmd2.ExecuteNonQuery();

                if (isLogout == 2)
                {
                    MessageBox.Show("You have been Logout", "Logout", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                    this.Close();
                    LoginWindow loginWin = new LoginWindow();
                    loginWin.Show();
                }




            }

            conn.Close();


        }
        #endregion



        #region identical
        private void TripleBarBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            InboxGrid.Width = 1131;
            PauseGrid.Width = 1131;
            ComposeGrid.Width = 1131;
            UserProfileGrid.Width = 1131;
            CustomerCareGrid.Width = 1131;
            LeftStack.Width = 80;
            //topDesign.Width = 1131;
            MainTextGrid.Width = 1131;
            DashboardGrid.Width = 1131;


            Thickness GridMargin = MainTextGrid.Margin;
            GridMargin.Left = 80;
            MainTextGrid.Margin = GridMargin;

            Thickness DashboardGridMargin = DashboardGrid.Margin;
            DashboardGridMargin.Left = 80;
            DashboardGrid.Margin = DashboardGridMargin;

            Thickness InboxGridMargin = InboxGrid.Margin;
            InboxGridMargin.Left = 80;
            InboxGrid.Margin = InboxGridMargin;

            Thickness ComposeGridMargin = ComposeGrid.Margin;
            ComposeGridMargin.Left = 80;
            ComposeGrid.Margin = ComposeGridMargin;

            Thickness UserProfileGridMargin = UserProfileGrid.Margin;
            UserProfileGridMargin.Left = 80;
            UserProfileGrid.Margin = UserProfileGridMargin;

            Thickness PauseGridMargin = PauseGrid.Margin;
            PauseGridMargin.Left = 80;
            PauseGrid.Margin = PauseGridMargin;

            //Thickness margin = topDesign.Margin;
            //margin.Left = 79;
            //topDesign.Margin = margin;
            UserImg.Width = 70;
            UserImg.Height = 57;
            UserProfile.Height = 77;

            Thickness marginImg = UserProfile.Margin;
            marginImg.Top = 5;
            marginImg.Left = -5;
            UserProfile.Margin = marginImg;

            Thickness DashboardImg1 = DashboardImg.Margin;
            DashboardImg1.Left = 18;
            DashboardImg.Margin = DashboardImg1;

            Thickness CCImg1 = CCImg.Margin;
            CCImg1.Left = 18;
            CCImg.Margin = CCImg1;

            Thickness ATImg1 = ATImg.Margin;
            ATImg1.Left = 18;
            ATImg.Margin = ATImg1;

            Thickness PauseImg1 = PauseImg.Margin;
            PauseImg1.Left = 18;
            PauseImg.Margin = PauseImg1;

            Thickness Copyright = Copyrightlbl.Margin;
            Copyright.Top = 225;
            Copyrightlbl.Margin = Copyright;


            Thickness LogoutImg1 = LogoutImg.Margin;
            LogoutImg1.Left = 18;
            LogoutImg.Margin = LogoutImg1;

            Thickness ThreeMargin = TripleBarBtn.Margin;
            ThreeMargin.Left = -10;
            TripleBarBtn.Margin = ThreeMargin;

            Logolbl.Width = 80;
            Dashboardlbl.Visibility = Visibility.Hidden;
            Chatlbl.Visibility = Visibility.Hidden;
            logoutlbl.Visibility = Visibility.Hidden;
            Pauselbl.Visibility = Visibility.Hidden;
            Usernamelbl.Visibility = Visibility.Hidden;
            Positionlbl.Visibility = Visibility.Hidden;
            Talklbl.Visibility = Visibility.Hidden;
            UserProfile.Width = 70;
            TripleBarBtn.Visibility = Visibility.Hidden;
            TripleBarBtn1.Visibility = Visibility.Visible;


        }
        #endregion

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

            client.Client.Shutdown(SocketShutdown.Send);
            thread.Join();
            ns.Close();
            client.Close();
        }

        private void TripleBarBtn1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            InboxGrid.Width = 931;
            PauseGrid.Width = 931;
            ComposeGrid.Width = 931;
            UserProfileGrid.Width = 931;
            LeftStack.Width = 280;
            //topDesign.Width = 931;
            MainTextGrid.Width = 931;
            CustomerCareGrid.Width = 931;

            Thickness GridMargin = MainTextGrid.Margin;
            GridMargin.Left = 280;
            MainTextGrid.Margin = GridMargin;

            Thickness DashboardGridMargin = DashboardGrid.Margin;
            DashboardGridMargin.Left = 280;
            DashboardGrid.Margin = DashboardGridMargin;

            Thickness InboxGridMargin = InboxGrid.Margin;
            InboxGridMargin.Left = 280;
            InboxGrid.Margin = InboxGridMargin;

            Thickness ComposeGridMargin = ComposeGrid.Margin;
            ComposeGridMargin.Left = 280;
            ComposeGrid.Margin = ComposeGridMargin;

            Thickness UserProfileGridMargin = UserProfileGrid.Margin;
            UserProfileGridMargin.Left = 280;
            UserProfileGrid.Margin = UserProfileGridMargin;

            Thickness PauseGridMargin = PauseGrid.Margin;
            PauseGridMargin.Left = 280;
            PauseGrid.Margin = PauseGridMargin;


            //Thickness margin = topDesign.Margin;
            //margin.Left = 280;
            //topDesign.Margin = margin;

            UserImg.Width = 59;
            UserImg.Height = 60;
            UserProfile.Height = 77;
            UserProfile.Width = 280;


            Thickness marginImg = UserProfile.Margin;
            marginImg.Top = 0;
            UserProfile.Margin = marginImg;

            Thickness DashboardImg1 = DashboardImg.Margin;
            DashboardImg1.Left = 0;
            DashboardImg.Margin = DashboardImg1;

            Thickness CCImg1 = CCImg.Margin;
            CCImg1.Left = 0;
            CCImg.Margin = CCImg1;

            Thickness ATImg1 = ATImg.Margin;
            ATImg1.Left = 0;
            ATImg.Margin = ATImg1;

            Thickness PauseImg1 = PauseImg.Margin;
            PauseImg1.Left = 0;
            PauseImg.Margin = PauseImg1;

            Thickness LogoutImg1 = LogoutImg.Margin;
            LogoutImg1.Left = 0;
            LogoutImg.Margin = LogoutImg1;

            Thickness ThreeMargin = TripleBarBtn.Margin;
            ThreeMargin.Left = -30;
            TripleBarBtn.Margin = ThreeMargin;

            Logolbl.Width = 329;
            //Thickness LogoMargin = Logolbl.Margin;
            //LogoMargin.Left = 0;
            //Logolbl.Margin = LogoMargin;

            Dashboardlbl.Visibility = Visibility.Visible;
            Chatlbl.Visibility = Visibility.Visible;
            logoutlbl.Visibility = Visibility.Visible;
            Pauselbl.Visibility = Visibility.Visible;
            Usernamelbl.Visibility = Visibility.Visible;
            Positionlbl.Visibility = Visibility.Visible;
            Talklbl.Visibility = Visibility.Visible;
            TripleBarBtn.Visibility = Visibility.Visible;
            TripleBarBtn1.Visibility = Visibility.Hidden;

        }

        private void ComposeBtn_Click(object sender, RoutedEventArgs e)
        {
            CustomerCareGrid.Visibility = Visibility.Hidden;
            NotificationGrid.Visibility = Visibility.Hidden;
            DashboardGrid.Visibility = Visibility.Hidden;
            InboxGrid.Visibility = Visibility.Hidden;
            PauseGrid.Visibility = Visibility.Hidden;
            ComposeGrid.Visibility = Visibility.Visible;
            UserProfileGrid.Visibility = Visibility.Hidden;
            var signalChange = signal_lbl as Ellipse;
            signalChange.Fill = Brushes.Green;

            CheckedButton = 0;
            CheckedStatus();

        }

        private void UserProfile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Dashboard.Visibility = Visibility.Hidden;
            CustomerCareGrid.Visibility = Visibility.Hidden;
            NotificationGrid.Visibility = Visibility.Hidden;
            DashboardGrid.Visibility = Visibility.Hidden;
            InboxGrid.Visibility = Visibility.Hidden;
            PauseGrid.Visibility = Visibility.Hidden;
            ComposeGrid.Visibility = Visibility.Hidden;
            UserProfileGrid.Visibility = Visibility.Visible;
            var signalChange = signal_lbl as Ellipse;
            signalChange.Fill = Brushes.Green;
            CheckedButton = 0;
            CheckedStatus();


        }

        private void FilterBtn_Click(object sender, RoutedEventArgs e)
        {

        }



        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {

        }



        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FileDialog fldlg = new OpenFileDialog();
                fldlg.InitialDirectory = Environment.SpecialFolder.MyPictures.ToString();
                fldlg.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
                fldlg.ShowDialog();
                {
                    strName = fldlg.SafeFileName;
                    imageName = fldlg.FileName;
                    ImageSourceConverter isc = new ImageSourceConverter();
                    ProfilePic.SetValue(Image.SourceProperty, isc.ConvertFromString(imageName));

                }
                fldlg = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void button1Copy_Click(object sender, RoutedEventArgs e)
        {

            insertImageData();
        }

        private void insertImageData()
        {
            try
            {
                if (imageName != "")
                {
                    //Initialize a file stream to read the image file
                    FileStream fs = new FileStream(imageName, FileMode.Open, FileAccess.Read);

                    //Initialize a byte array with size of stream
                    byte[] imgByteArr = new byte[fs.Length];

                    //Read data from the file stream and put into the byte array
                    fs.Read(imgByteArr, 0, Convert.ToInt32(fs.Length));

                    //Close a file stream
                    fs.Close();

                    using (SqlConnection conn = new SqlConnection(constr))
                    {
                        conn.Open();


                        string sql = "insert into tbl_Image(id,img) values('" + strName + "',@img)";

                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            //Pass byte array into database
                            cmd.Parameters.Add(new SqlParameter("img", imgByteArr));
                            int result = cmd.ExecuteNonQuery();
                            if (result == 1)
                            {
                                MessageBox.Show("Image added successfully.");

                            }
                        }



                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
