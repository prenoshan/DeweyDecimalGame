using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for ReplacingBooks.xaml
    /// </summary>
    public partial class ReplacingBooksCategories : Window
    {

        private int userID;

        public ReplacingBooksCategories(int id)
        {
            InitializeComponent();

            userID = id;
        }

        //method sets the range the user has selected to be used on the game area
        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            StackPanel clickedPanel = (StackPanel)sender;

            TextBlock tbCategory = clickedPanel.Children.OfType<TextBlock>().FirstOrDefault();

            string tbCategoryName = tbCategory.Text;

            ReplacingBooksGameArea replacingBooksGameArea = new ReplacingBooksGameArea(tbCategoryName, userID);

            replacingBooksGameArea.Show();

            this.Close();

        }

    }
}
