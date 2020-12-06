using DBHelperDLL;
using ScoreDLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for FindingCallNumbersGameArea.xaml
    /// </summary>
    public partial class FindingCallNumbersGameArea : Window
    {

        //variables
        private DeweyDecimalData deweyDecimalData = new DeweyDecimalData();

        private string userQuestion;
        private string roundThreeCallNumber;
        private string optOne, optTwo, optThree, optFour;
        private string round, correctAns;
        private bool roundOver;
        private bool gameOver;

        private string gameType = "Finding Call Numbers";

        private List<string> incorrectAns = new List<string>();
        private List<string> possibleOptions = new List<string>();

        private int correctCount = 0;
        private int incorrectCount = 0;
        private int roundScore = 0;
        private int roundCount = 0;
        private List<string> incorrectAnsList = new List<string>();

        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private Stopwatch stopwatch = new Stopwatch();
        private Score score = new Score();
        private DBHelper dBHelper = new DBHelper();
        private string connection = ConfigurationManager.ConnectionStrings["DeweyDB"].ConnectionString;

        private string currentTime = "";
        private int userID;

        public FindingCallNumbersGameArea(int id)
        {
            InitializeComponent();

            btnPlayAgain.IsEnabled = false;

            //sets the relative path to access the database
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string relative = @"..\..\";
            string absolute = System.IO.Path.GetFullPath(System.IO.Path.Combine(baseDirectory, relative));
            AppDomain.CurrentDomain.SetData("DataDirectory", absolute);

            //retrieves the user's id
            userID = id;

            //gets and sets the high score when the user begins the mode
            if (getHighScore() == "")
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

            //set lists of dewey decimal data to be used by the tree
            deweyDecimalData.setFirstNodeList();
            deweyDecimalData.setSecondNodeList();
            deweyDecimalData.setThirdNodeList();

            //sets the round to the first round by default
            round = "Round One";
            roundOver = false;

            setRoundCount();

            populateGrids();

        }

        //timer event to keep track of how long the user takes
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {

            if (stopwatch.IsRunning)
            {

                TimeSpan ts = stopwatch.Elapsed;

                currentTime = String.Format("{0:00}:{1:00}:{2:00}",
                    ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

                tbModeTimer.Text = currentTime;

            }

        }

        //method to set the round count textblock
        private void setRoundCount()
        {

            switch (round)
            {
                case "Round One":
                    tbRoundCount.Text = "1";
                    roundCount = 1;
                    break;

                case "Round Two":
                    tbRoundCount.Text = "2";
                    roundCount = 2;
                    break;

                case "Round Three":
                    tbRoundCount.Text = "3";
                    roundCount = 3;
                    break;

                default:
                    break;
            }

        }

        //method to generate possible options for user
        private void generateIncorrectOptions()
        {

            //gets incorrect options for the first round from the first tree node
            if (round.Equals("Round One"))
            {

                foreach (var item in deweyDecimalData.getFirstNodeList())
                {

                    if (item != correctAns)
                    {

                        incorrectAns.Add(item);

                    }

                }

            }

            //gets incorrect options for the first round from the second tree node
            else if (round.Equals("Round Two"))
            {

                foreach (var item in deweyDecimalData.getSecondNodeList())
                {

                    if (item != correctAns)
                    {

                        incorrectAns.Add(item);

                    }

                }

            }

            //gets incorrect options for the first round from the third tree node
            else if (round.Equals("Round Three"))
            {

                foreach (var item in deweyDecimalData.getThirdNodeList())
                {

                    if (item != correctAns)
                    {

                        incorrectAns.Add(item);

                    }

                }

            }

        }

        //method to get the answer for each round using the tree data structure
        private string getAnswer(string userAnswer)
        {

            string correctAnswer = "";

            TreeData treeData = new TreeData();

            TreeNode<string> treeRoot = treeData.setData();

            //gets the answer for round one from the tree i.e. the parent's parent
            if (round.Equals("Round One"))
            {

                TreeNode<string> answer = treeRoot.Where(node => node.Data != null && node.Data.Contains(userAnswer)).Select(node => node.Parent.Parent).FirstOrDefault();

                correctAnswer = answer.ToString();

            }

            //gets the answer for round two from the tree i.e. the parent
            else if (round.Equals("Round Two"))
            {

                TreeNode<string> answer = treeRoot.Where(node => node.Data != null && node.Data.Contains(userAnswer)).Select(node => node.Parent).FirstOrDefault();

                correctAnswer = answer.ToString();

            }

            //gets the answer for round three which is already set as the question's dewey decimal description
            else if (round.Equals("Round Three"))
            {

                correctAnswer = userAnswer;

            }

            return correctAnswer;

        }

        private void populateGrids()
        {

            //code to make sure the question stays the same for each round
            //ie three rounds one question to be displayed
            if (roundOver == true || round.Equals("Round One"))
            {

                //sets a random question for the user from the most detailed level of the dewey decimal data (the third level)
                Shuffle(deweyDecimalData.getThirdNodeList());

                userQuestion = deweyDecimalData.getThirdNodeList()[0];

                tbQuest.Text = userQuestion.Remove(0,4);

                //calls method to get the correct answer for each question
                correctAns = getAnswer(userQuestion);

                //at the third round get the call number for the user to match with the question's dewey decimal description
                roundThreeCallNumber = userQuestion.Substring(0,3);

            }

            correctAns = getAnswer(userQuestion);

            //finds the textblock on each grid for the options
            StackPanel gridOptOne = gridOneOpt.Children.OfType<StackPanel>().FirstOrDefault();
            TextBlock gridOptOneText = this.FindName(gridOptOne.Children.OfType<TextBlock>().FirstOrDefault().Name) as TextBlock;

            StackPanel gridOptTwo = gridTwoOpt.Children.OfType<StackPanel>().FirstOrDefault();
            TextBlock gridOptTwoText = this.FindName(gridOptTwo.Children.OfType<TextBlock>().FirstOrDefault().Name) as TextBlock;

            StackPanel gridOptThree = gridThreeOpt.Children.OfType<StackPanel>().FirstOrDefault();
            TextBlock gridOptThreeText = this.FindName(gridOptThree.Children.OfType<TextBlock>().FirstOrDefault().Name) as TextBlock;

            StackPanel gridOptFour = gridFourOpt.Children.OfType<StackPanel>().FirstOrDefault();
            TextBlock gridOptFourText = this.FindName(gridOptFour.Children.OfType<TextBlock>().FirstOrDefault().Name) as TextBlock;

            //finds the textblock on each grid for the answers
            StackPanel gridOneAns = gridAns.Children.OfType<StackPanel>().FirstOrDefault();
            TextBlock gridAnsText = this.FindName(gridOneAns.Children.OfType<TextBlock>().FirstOrDefault().Name) as TextBlock;

            //resets answer grid
            gridAnsText.Text = "Drop answer here";

            generateIncorrectOptions();

            //randomise incorrect options list
            Shuffle(incorrectAns);

            //add 3 incorrect options and the correct option to the possible options list
            optOne = correctAns;
            optTwo = incorrectAns[0];
            optThree = incorrectAns[1];
            optFour = incorrectAns[2];

            possibleOptions.Add(optOne);
            possibleOptions.Add(optTwo);
            possibleOptions.Add(optThree);
            possibleOptions.Add(optFour);

            //randomise the possible options for the user
            Shuffle(possibleOptions);

            if(round.Equals("Round Three"))
            {

                gridOptOneText.Text = possibleOptions[0].Substring(0,3);
                gridOptTwoText.Text = possibleOptions[1].Substring(0,3);
                gridOptThreeText.Text = possibleOptions[2].Substring(0,3);
                gridOptFourText.Text = possibleOptions[3].Substring(0,3);

            }

            else
            {

                gridOptOneText.Text = possibleOptions[0];
                gridOptTwoText.Text = possibleOptions[1];
                gridOptThreeText.Text = possibleOptions[2];
                gridOptFourText.Text = possibleOptions[3];

            }

        }

        //method to check if the user has selected an answer
        private bool validateAnswers()
        {

            bool isValidated = true;

            //finds the textblock on each grid for the answers
            StackPanel gridOneAns = gridAns.Children.OfType<StackPanel>().FirstOrDefault();
            TextBlock gridAnsText = this.FindName(gridOneAns.Children.OfType<TextBlock>().FirstOrDefault().Name) as TextBlock;

            if(gridAnsText.Text.Equals("Drop answer here"))
            {

                isValidated = false;

            }

            return isValidated;

        }

        private void btnSubmitAnswers_Click(object sender, RoutedEventArgs e)
        {

            //validates user's answer
            if (!validateAnswers())
            {

                invalidAnswersDialog.IsOpen = true;

            }

            else
            {

                //switch case to progress to the next round if the user gets an answer correct
                switch (round)
                {
                    case "Round One":
                        calculateAnswers();
                        checkGameOver();
                        round = "Round Two";
                        roundOver = false;
                        setRoundCount();
                        break;

                    case "Round Two":
                        calculateAnswers();
                        checkGameOver();
                        round = "Round Three";
                        roundOver = false;
                        setRoundCount();
                        break;

                    case "Round Three":
                        calculateAnswers();
                        checkGameOver();
                        roundOver = true;
                        correctAns = "";
                        round = "Round One";
                        setRoundCount();
                        break;

                    default:
                        round = "";
                        break;
                }

                //code to end the game and display the round the user is currently on before they begin a new question
                if (gameOver == true)
                {

                    tbRoundScore.Text = roundScore.ToString();

                    if (roundCount == 1)
                    {
                        tbRoundCount.Text = (roundCount + 2).ToString();
                    }

                    else
                    {
                        tbRoundCount.Text = (roundCount - 1).ToString();
                    }

                    //stops the timer when the user is finished
                    if (stopwatch.IsRunning)
                    {
                        stopwatch.Stop();
                    }

                    btnSubmitAnswers.IsEnabled = false;

                    btnPlayAgain.IsEnabled = true;

                }

                //if the game is not over then continue to the next round for the user
                else
                {

                    tbRoundScore.Text = roundScore.ToString();

                    incorrectAns.Clear();
                    possibleOptions.Clear();

                    populateGrids();

                }

            }

        }

        private void checkGameOver()
        {

            //if a user gets a round wrong then end the game and progress to the next question
            if(incorrectCount > 0)
            {

                gameOver = true;

                saveUserScore();

                Debug.WriteLine(getHighScore());

                //updates to new high score at the end of each game for user
                tbHighScore.Text = getHighScore();

                displayResults();
                resultsDialog.IsOpen = true;

            }

            //if a user gets all 3 rounds correct then end the game as well
            else if(correctCount == 3)
            {

                gameOver = true;

                saveUserScore();

                Debug.WriteLine(getHighScore());

                //updates to new high score at the end of each game for user
                tbHighScore.Text = getHighScore();

                displayResults();
                resultsDialog.IsOpen = true;

            }

            foreach (var item in incorrectAnsList)
            {

                Debug.WriteLine(item);

            }


        }

        private void calculateAnswers()
        {

            //finds the textblock on each grid for the answers
            StackPanel gridOneAns = gridAns.Children.OfType<StackPanel>().FirstOrDefault();
            TextBlock gridAnsText = this.FindName(gridOneAns.Children.OfType<TextBlock>().FirstOrDefault().Name) as TextBlock;

            string userAnswer = gridAnsText.Text;

            //checks to see if the answer the user chooses is correct
            if (userAnswer.Equals(correctAns)){ correctCount += 1; roundScore += 10; }

            //handles the user's answer if it is incorrect
            else if (round.Equals("Round Three"))
            {

                if (userAnswer.Equals(roundThreeCallNumber))
                {

                    correctCount += 1; roundScore += 10;

                }

                else { incorrectCount += 1; incorrectAnsList.Add("You got the third round's answer wrong and your game is over"); }

            }

            else if (!userAnswer.Equals(correctAns) && round.Equals("Round One")) { incorrectCount += 1; incorrectAnsList.Add("You got the first round's answer wrong and your game is over"); }

            else if (!userAnswer.Equals(correctAns) && round.Equals("Round Two")) { incorrectCount += 1; incorrectAnsList.Add("You got the second round's answer wrong and your game is over"); }

        }

        private void displayResults()
        {

            //method that sets the controls descriptions for the results dialog
            tbCorrectDialog.Text = "You got " + correctCount.ToString() + " rounds correct :)";

            if (incorrectCount == 0)
            {
                tbIncorrectDialog.Text = "You got every round correct, have a cookie :)";
            }

            else if (incorrectCount > 0)
            {
                tbIncorrectDialog.Text = "You got " + incorrectCount.ToString() + " rounds wrong :(";

                lvWrongAnswers.ItemsSource = incorrectAnsList;
            }

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
            if (currentGrid.Name != draggingGrid.Name)
            {

                currentGrid.Children.Clear();
                draggingGrid.Children.Clear();

                currentGrid.Children.Add(draggingGridStack);
                draggingGrid.Children.Add(currentGridStack);

            }

        }

        private void grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            Grid sendingGrid = (Grid)sender;

            DataObject dataObject = new DataObject(sendingGrid);

            DragDrop.DoDragDrop(sendingGrid, dataObject, DragDropEffects.Move);

        }

        private void btnPlayAgain_Click(object sender, RoutedEventArgs e)
        {

            //resets variables so the second question can be shown
            round = "Round One";
            correctCount = 0;
            incorrectCount = 0;
            incorrectAns.Clear();
            incorrectAnsList.Clear();
            possibleOptions.Clear();
            gameOver = false;
            roundScore = 0;
            lvWrongAnswers.ItemsSource = null;

            tbRoundScore.Text = "0";

            setRoundCount();

            btnSubmitAnswers.IsEnabled = true;
            btnPlayAgain.IsEnabled = false;

            //resets the timer for the user
            stopwatch.Restart();
            tbModeTimer.Text = "00:00:00";

            populateGrids();

        }

        //sets the column names for the data grid view
        private void dgScores_AutoGeneratedColumns(object sender, EventArgs e)
        {
            dgScores.Columns[0].Header = "Score";
            dgScores.Columns[1].Header = "Time you took (minutes:seconds:milliseconds)";
            dgScores.Columns[2].Header = "Date and time Completed";
        }

        private void btnViewScores_Click(object sender, RoutedEventArgs e)
        {

            //code to get the user's high scores from the database
            dgScores.ItemsSource = score.getScores(dBHelper.openConn(connection), userID, gameType).DefaultView;

            if (dgScores.Items.Count == 0)
            {

                stckScoresDialog.Children.Remove(dgScores);

                tbYourScores.Text = "You don't have any scores saved yet, play a game to see some data";

            }

            dBHelper.closeConn();

            scoresDialog.IsOpen = true;

        }

        private void btnHowToPlay_Click(object sender, RoutedEventArgs e)
        {

            howToPlayDialog.IsOpen = true;

        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {

            Login login = new Login();

            login.Show();

            this.Close();

        }

        //method to save the user's score
        private void saveUserScore()
        {
            //current date
            var time = DateTime.Now;

            string highScoreString;

            if(getHighScore() == "")
            {
                highScoreString = "0";
            }

            else
            {
                highScoreString = getHighScore();
            }

            int highScoreNum = roundScore + Convert.ToInt32(highScoreString);

            score.saveUserScore(dBHelper.openConn(connection), userID, highScoreNum.ToString(), tbModeTimer.Text, time.ToString(), gameType);

            dBHelper.closeConn();

        }

        //method to return user's current high score
        private string getHighScore()
        {

            string highScore = "";

            highScore = score.getHighScore(dBHelper.openConn(connection), userID, gameType);

            dBHelper.closeConn();

            return highScore;
        }

        //fisher-yates algorithm for shuffling the list to make each round of the game yield random questions and options
        private void Shuffle<T>(IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

    }
}
