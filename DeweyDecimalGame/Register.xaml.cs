using AuthDLL;
using DBHelperDLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {

        private string connection = ConfigurationManager.ConnectionStrings["DeweyDB"].ConnectionString;
        private DBHelper dBHelper = new DBHelper();
        private Auth authDLL = new Auth();

        public Register()
        {
            InitializeComponent();

            //sets the relative path to access the database
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string relative = @"..\..\";
            string absolute = System.IO.Path.GetFullPath(System.IO.Path.Combine(baseDirectory, relative));
            AppDomain.CurrentDomain.SetData("DataDirectory", absolute);

        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {

            bool userExists = authDLL.checkUser(dBHelper.openConn(connection), tbUsername.Text);

            if(userExists)
            {
                userExistsDialog.IsOpen = true;
            }

            else
            {

                dBHelper.closeConn();

                authDLL.register(dBHelper.openConn(connection), tbUsername.Text, tbPassword.Password);

                dBHelper.closeConn();

                userCreatedDialog.IsOpen = true;

            }

        }

        private void goToLogin_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

            Login login = new Login();

            login.Show();

            this.Close();

        }
    }
}
