using DBHelperDLL;
using ScoreDLL;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Threading;

namespace DeweyDecimalGame
{
    /// <summary>
    /// Interaction logic for ReplacingBooksGameArea.xaml
    /// </summary>
    public partial class ReplacingBooksGameArea : Window
    {

        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private Stopwatch stopwatch = new Stopwatch();
        private Score score = new Score();
        private DBHelper dBHelper = new DBHelper();
        private string connection = ConfigurationManager.ConnectionStrings["DeweyDB"].ConnectionString;
        private CallNumbers callNumbers = new CallNumbers();
        private List<string> callNumbersList = new List<string>();
        private string ansOne, ansTwo, ansThree, ansFour, ansFive, ansSix, ansSeven, ansEight, ansNine, ansTen;
        private int correctCount = 0;
        private int incorrectCount = 0;
        private List<string> incorrectAnswersList = new List<string>();
        private string categoryRange;
        private int min, max;
        private int userID;
        private string currentTime = "";
        private string gameType = "ReplaceBooks";

        //method to save the user's score
        private void saveUserScore()
        {
            //current date
            var time = DateTime.Now;

            score.saveUserScore(dBHelper.openConn(connection), userID, correctCount.ToString(), tbTimer.Text, time.ToString(), gameType);

            dBHelper.closeConn();

        }

        //overloaded constructor to get range of category
        public ReplacingBooksGameArea(string range, int id)
        {

            InitializeComponent();

            //sets the relative path to access the database
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string relative = @"..\..\";
            string absolute = System.IO.Path.GetFullPath(System.IO.Path.Combine(baseDirectory, relative));
            AppDomain.CurrentDomain.SetData("DataDirectory", absolute);

            //gets the category from the previous window
            categoryRange = range.Substring(0,3);

            //retrieves the user's id
            userID = id;

            getRange();

            callNumbersList = callNumbers.generateNumbers(min, max);

            setCallNumbers();

            btnPlayAgain.IsEnabled = false;

            if(getHighScore() == "")
            {
                tbHighScore.Text = "0";
            }

            else
            {
                tbHighScore.Text = getHighScore();
            }

            //sets the timer to 1 second per tick
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            dispatcherTimer.Start();
            stopwatch.Start();

        }

        //timer event to keep track of how long the user takes
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {

            if (stopwatch.IsRunning)
            {

                TimeSpan ts = stopwatch.Elapsed;
                
                currentTime = String.Format("{0:00}:{1:00}:{2:00}",
                    ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

                tbTimer.Text = currentTime;

            }

        }

        //method to set the range of call numbers based on the category the user selected
        private void getRange()
        {

            switch (categoryRange)
            {

                case "000": 
                    min = 1;
                    max = 100;
                    break;

                case "100":
                    min = 100;
                    max = 200;
                    break;

                case "200":
                    min = 200;
                    max = 300;
                    break;

                case "300":
                    min = 300;
                    max = 400;
                    break;

                case "400":
                    min = 400;
                    max = 500;
                    break;

                case "500":
                    min = 500;
                    max = 600;
                    break;

                case "600":
                    min = 600;
                    max = 700;
                    break;

                case "700":
                    min = 700;
                    max = 800;
                    break;

                case "800":
                    min = 800;
                    max = 900;
                    break;

                case "900":
                    min = 900;
                    max = 1000;
                    break;

                default: min = 0; max = 0;
                    break;
            }

        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {

            Login login = new Login();

            login.Show();

            this.Close();

        }

        //method to show user a grid of their scores
        private void btnViewScores_Click(object sender, RoutedEventArgs e)
        {

            dgScores.ItemsSource = score.getScores(dBHelper.openConn(connection), userID, gameType).DefaultView;

            if (dgScores.Items.Count == 0)
            {

                stckScoresDialog.Children.Remove(dgScores);

                tbYourScores.Text = "You don't have any scores saved yet, play a game to see some data";

            }

            dBHelper.closeConn();

            scoresDialog.IsOpen = true;

        }

        //sets the column names for the data grid view
        private void dgScores_AutoGeneratedColumns(object sender, EventArgs e)
        {
            dgScores.Columns[0].Header = "Score";
            dgScores.Columns[1].Header = "Time you took (minutes:seconds:milliseconds)";
            dgScores.Columns[2].Header = "Date and time Completed";
        }

        //method to display the call numbers for the user
        public void setCallNumbers()
        {

            tbOne.Text = callNumbersList[0];
            tbTwo.Text = callNumbersList[1];
            tbThree.Text = callNumbersList[2];
            tbFour.Text = callNumbersList[3];
            tbFive.Text = callNumbersList[4];
            tbSix.Text = callNumbersList[5];
            tbSeven.Text = callNumbersList[6];
            tbEight.Text = callNumbersList[7];
            tbNine.Text = callNumbersList[8];
            tbTen.Text = callNumbersList[9];

        }

        private void grid_Drop(object sender, DragEventArgs e)
        {

            //current grid
            Grid currentGrid = (Grid)sender;

            //dragging grid
            Grid draggingGrid = (Grid)e.Data.GetData(typeof(Grid));

            //stackpanel of the current grid
            var currentGridStack = currentGrid.Children.OfType<StackPanel>().FirstOrDefault();

            //stackpanel of the dragging grid
            var draggingGridStack = draggingGrid.Children.OfType<StackPanel>().FirstOrDefault();

            //replaces grids content with new content that is dragged over
            if(currentGrid.Name != draggingGrid.Name)
            {

                currentGrid.Children.Clear();
                draggingGrid.Children.Clear();

                currentGrid.Children.Add(draggingGridStack);
                draggingGrid.Children.Add(currentGridStack);

            }

        }

        //method handles the grid to be dragged over to its new position
        private void grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            Grid sendingGrid = (Grid)sender;

            DataObject dataObject = new DataObject(sendingGrid);

            DragDrop.DoDragDrop(sendingGrid, dataObject, DragDropEffects.Move);

        }

        //method gets the user's answers based on the stack panels they drag around
        private void getAnswers()
        {

            StackPanel gridOneStack = gridOne.Children.OfType<StackPanel>().FirstOrDefault();
            TextBlock gridOneAns = this.FindName(gridOneStack.Children.OfType<TextBlock>().FirstOrDefault().Name) as TextBlock;

            StackPanel gridTwoStack = gridTwo.Children.OfType<StackPanel>().FirstOrDefault();
            TextBlock gridTwoAns = this.FindName(gridTwoStack.Children.OfType<TextBlock>().FirstOrDefault().Name) as TextBlock;

            StackPanel gridThreeStack = gridThree.Children.OfType<StackPanel>().FirstOrDefault();
            TextBlock gridThreeAns = this.FindName(gridThreeStack.Children.OfType<TextBlock>().FirstOrDefault().Name) as TextBlock;

            StackPanel gridFourStack = gridFour.Children.OfType<StackPanel>().FirstOrDefault();
            TextBlock gridFourAns = this.FindName(gridFourStack.Children.OfType<TextBlock>().FirstOrDefault().Name) as TextBlock;

            StackPanel gridFiveStack = gridFive.Children.OfType<StackPanel>().FirstOrDefault();
            TextBlock gridFiveAns = this.FindName(gridFiveStack.Children.OfType<TextBlock>().FirstOrDefault().Name) as TextBlock;

            StackPanel gridSixStack = gridSix.Children.OfType<StackPanel>().FirstOrDefault();
            TextBlock gridSixAns = this.FindName(gridSixStack.Children.OfType<TextBlock>().FirstOrDefault().Name) as TextBlock;

            StackPanel gridSevenStack = gridSeven.Children.OfType<StackPanel>().FirstOrDefault();
            TextBlock gridSevenAns = this.FindName(gridSevenStack.Children.OfType<TextBlock>().FirstOrDefault().Name) as TextBlock;

            StackPanel gridEightStack = gridEight.Children.OfType<StackPanel>().FirstOrDefault();
            TextBlock gridEightAns = this.FindName(gridEightStack.Children.OfType<TextBlock>().FirstOrDefault().Name) as TextBlock;

            StackPanel gridNineStack = gridNine.Children.OfType<StackPanel>().FirstOrDefault();
            TextBlock gridNineAns = this.FindName(gridNineStack.Children.OfType<TextBlock>().FirstOrDefault().Name) as TextBlock;

            StackPanel gridTenStack = gridTen.Children.OfType<StackPanel>().FirstOrDefault();
            TextBlock gridTenAns = this.FindName(gridTenStack.Children.OfType<TextBlock>().FirstOrDefault().Name) as TextBlock;

            ansOne = gridOneAns.Text;
            ansTwo = gridTwoAns.Text;
            ansThree = gridThreeAns.Text;
            ansFour = gridFourAns.Text;
            ansFive = gridFiveAns.Text;
            ansSix = gridSixAns.Text;
            ansSeven = gridSevenAns.Text;
            ansEight = gridEightAns.Text;
            ansNine = gridNineAns.Text;
            ansTen = gridTenAns.Text;

        }

        //method to sort the list
        private void sortCallNumbers()
        {

            callNumbersList.Sort();

        }

        //method that checks a user's answer and compares it to the sorted list to check if it was correct
        private void checkAnswers()
        {

            sortCallNumbers();

            if(ansOne != callNumbersList[0]){ incorrectAnswersList.Add("Position 1 was incorrect"); incorrectCount += 1; }
            else { correctCount += 1; }

            if (ansTwo != callNumbersList[1]) { incorrectAnswersList.Add("Position 2 was incorrect"); incorrectCount += 1; }
            else { correctCount += 1; }

            if (ansThree != callNumbersList[2]) { incorrectAnswersList.Add("Position 3 was incorrect"); incorrectCount += 1; }
            else { correctCount += 1; }

            if (ansFour != callNumbersList[3]) { incorrectAnswersList.Add("Position 4 was incorrect"); incorrectCount += 1; }
            else { correctCount += 1; }

            if (ansFive != callNumbersList[4]) { incorrectAnswersList.Add("Position 5 was incorrect"); incorrectCount += 1; }
            else { correctCount += 1; }

            if (ansSix != callNumbersList[5]) { incorrectAnswersList.Add("Position 6 was incorrect"); incorrectCount += 1; }
            else { correctCount += 1; }

            if (ansSeven != callNumbersList[6]) { incorrectAnswersList.Add("Position 7 was incorrect"); incorrectCount += 1; }
            else { correctCount += 1; }

            if (ansEight != callNumbersList[7]) { incorrectAnswersList.Add("Position 8 was incorrect"); incorrectCount += 1; }
            else { correctCount += 1; }

            if (ansNine != callNumbersList[8]) { incorrectAnswersList.Add("Position 9 was incorrect"); incorrectCount += 1; }
            else { correctCount += 1; }

            if (ansTen != callNumbersList[9]) { incorrectAnswersList.Add("Position 10 was incorrect"); incorrectCount += 1; }
            else { correctCount += 1; }

        }

        //method to display user's answers
        private void displayAnswers()
        {

            tbCorrectDialog.Text = "You got " + correctCount.ToString() + " positions correct :)";
            
            if(incorrectCount == 0)
            {
                tbIncorrectDialog.Text = "You got every answer correct, have a cookie :)";
            }

            else if(incorrectCount > 0)
            {
                tbIncorrectDialog.Text = "You got " + incorrectCount.ToString() + " positions wrong :(";

                lvWrongAnswers.ItemsSource = incorrectAnswersList;
            }

        }

        //method calculates users answers
        private void btnSubmitAnswers_Click(object sender, RoutedEventArgs e)
        {

            //stops the timer when the user is finished
            if (stopwatch.IsRunning)
            {
                stopwatch.Stop();
            }

            getAnswers();

            checkAnswers();

            saveUserScore();

            //updates to new high score at the end of each game for user
            tbHighScore.Text = getHighScore();

            displayAnswers();

            resultsDialog.IsOpen = true;

            btnSubmitAnswers.IsEnabled = false;

            btnPlayAgain.IsEnabled = true;

        }

        //method clears lists and reassigns call numbers if the user wishes to play again
        private void btnPlayAgain_Click(object sender, RoutedEventArgs e)
        {

            //resets the timer for the user
            stopwatch.Restart();
            tbTimer.Text = "00:00:00";

            correctCount = 0;
            incorrectCount = 0;
            incorrectAnswersList.Clear();
            callNumbersList.Clear();

            callNumbersList = callNumbers.generateNumbers(min, max);

            setCallNumbers();

            btnSubmitAnswers.IsEnabled = true;

            btnPlayAgain.IsEnabled = false;

        }

        //method to return user's current high score
        private string getHighScore()
        {

            string highScore = "";

            highScore = score.getHighScore(dBHelper.openConn(connection), userID, gameType);

            dBHelper.closeConn();

            return highScore;
        }
    }
}
