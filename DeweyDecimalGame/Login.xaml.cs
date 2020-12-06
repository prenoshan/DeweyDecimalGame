using AuthDLL;
using DBHelperDLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DeweyDecimalGame
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        private string connection = ConfigurationManager.ConnectionStrings["DeweyDB"].ConnectionString;
        private DBHelper dBHelper = new DBHelper();
        private Auth authDLL = new Auth();

        public Login()
        {
            InitializeComponent();

            //sets the relative path to access the database
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string relative = @"..\..\";
            string absolute = System.IO.Path.GetFullPath(System.IO.Path.Combine(baseDirectory, relative));
            AppDomain.CurrentDomain.SetData("DataDirectory", absolute);

        }

        //method to return user's id
        private int getUserID()
        {

            int id = 0;

            id = authDLL.getUserID(dBHelper.openConn(connection), tbUsername.Text);

            dBHelper.closeConn();

            return id;

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            bool userExists = authDLL.login(dBHelper.openConn(connection), tbUsername.Text, tbPassword.Password);

            if (userExists)
            {
                dBHelper.closeConn();

                MainWindow mainWindow = new MainWindow(getUserID());

                mainWindow.Show();

                this.Close();

            }

            else
            {

                dBHelper.closeConn();

                invalidDetailsDialog.IsOpen = true;

            }

        }

        private void goToRegister_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

            Register register = new Register();

            register.Show();

            this.Close();

        }
    }
}
