using AuthDLL;
using DBHelperDLL;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DeweyDecimalGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private int userID;
        private DBHelper dBHelper = new DBHelper();
        private Auth auth = new Auth();
        private string connection = ConfigurationManager.ConnectionStrings["DeweyDB"].ConnectionString;

        private void setWelcomeUsername()
        {

            tbWelcome.Text = "Welcome " + auth.getUsername(dBHelper.openConn(connection), userID);

        }

        public MainWindow(int id)
        {
            InitializeComponent();

            //sets the relative path to access the database
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string relative = @"..\..\";
            string absolute = System.IO.Path.GetFullPath(System.IO.Path.Combine(baseDirectory, relative));
            AppDomain.CurrentDomain.SetData("DataDirectory", absolute);

            userID = id;

            setWelcomeUsername();
        }

        private void stckReplaceBooks_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ReplacingBooksCategories replacingBooksCategories = new ReplacingBooksCategories(userID);

            replacingBooksCategories.Show();

            this.Close();
        }

        private void stckFindCallNumbers_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FindingCallNumbersGameArea findingCallNumbersGameArea = new FindingCallNumbersGameArea(userID);

            findingCallNumbersGameArea.Show();

            this.Close();
        }

        private void stckIdentifyAreas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            IdentifyingAreasGameArea identifyingAreasGameArea = new IdentifyingAreasGameArea(userID);

            identifyingAreasGameArea.Show();

            this.Close();
        }
    }
}
