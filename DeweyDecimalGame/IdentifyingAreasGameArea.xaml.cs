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
    /// Interaction logic for IdentifyingAreasGameArea.xaml
    /// </summary>
    public partial class IdentifyingAreasGameArea : Window
    {
        //global variables
        private Dictionary<string, string> ddAreas = new Dictionary<string, string>();
        private List<string> ddCodes = new List<string>();
        private List<string> ddDesc = new List<string>();

        private List<string> correctOpts = new List<string>();
        private List<string> incorrectOpts = new List<string>();
        private List<string> generatedOpts = new List<string>();

        private string ansOne, ansTwo, ansThree, ansFour;
        private int correctCount = 0;
        private int incorrectCount = 0;
        private List<string> incorrectAnsList = new List<string>();

        private bool switchRound = false;
        private Random random = new Random();

        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private Stopwatch stopwatch = new Stopwatch();
        private Score score = new Score();
        private DBHelper dBHelper = new DBHelper();
        private string connection = ConfigurationManager.ConnectionStrings["DeweyDB"].ConnectionString;

        private string currentTime = "";
        private string gameType = "Identify Areas";
        private int userID;

        //method to populate the dictionary with dewey decimal categories
        private void populateDictionary()
        {

            ddAreas.Add("000", "General Knowledge");
            ddAreas.Add("100", "Philosophy and Psychology");
            ddAreas.Add("200", "Religion");
            ddAreas.Add("300", "Social Sciences");
            ddAreas.Add("400", "Languages");
            ddAreas.Add("500", "Science");
            ddAreas.Add("600", "Technology");
            ddAreas.Add("700", "Arts and Recreation");
            ddAreas.Add("800", "Literature");
            ddAreas.Add("900", "History and Geography");

        }

        //method to populate the lists for the codes and descriptions to be used
        private void populateQandALists()
        {

            foreach (var code in ddAreas.Keys)
            {
                ddCodes.Add(code);
            }

            foreach (var desc in ddAreas.Values)
            {
                ddDesc.Add(desc);
            }

        }

        public IdentifyingAreasGameArea(int id)
        {
            InitializeComponent();

            //sets the relative path to access the database
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string relative = @"..\..\";
            string absolute = System.IO.Path.GetFullPath(System.IO.Path.Combine(baseDirectory, relative));
            AppDomain.CurrentDomain.SetData("DataDirectory", absolute);

            //retrieves the user's id
            userID = id;

            btnPlayModeAgain.IsEnabled = false;

            populateDictionary();

            populateQandALists();

            populateGrids();

            if (getHighScore() == "")
            {
                tbModeHighScore.Text = "0";
            }

            else
            {
                tbModeHighScore.Text = getHighScore();
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

                tbModeTimer.Text = currentTime;

            }

        }

        //method to populate the list that holds the generated options
        private void populatePossibleOptionsList()
        {

            //stores all the correct possible options
            foreach (var correctOpt in correctOpts)
            {

                generatedOpts.Add(correctOpt);

            }

            //when questions are dewey decimal codes and answers are descriptions
            if (switchRound == false)
            {
                //stores all the incorrect options
                foreach (var incorrectOpt in ddAreas.Values)
                {

                    if (incorrectOpt != correctOpts[0] && incorrectOpt != correctOpts[1] && incorrectOpt != correctOpts[2] && incorrectOpt != correctOpts[3])
                    {

                        incorrectOpts.Add(incorrectOpt);

                    }

                }
            }

            //when questions are values(descriptions) and answers are keys(codes)
            else if (switchRound)
            {
                //stores all the incorrect options
                foreach (var incorrectOpt in ddAreas.Keys)
                {

                    if (incorrectOpt != correctOpts[0] && incorrectOpt != correctOpts[1] && incorrectOpt != correctOpts[2] && incorrectOpt != correctOpts[3])
                    {

                        incorrectOpts.Add(incorrectOpt);

                    }

                }
            }

            //shuffles the incorrect options list for randomness
            Shuffle(incorrectOpts);

            //stores 2 random incorrect answers 
            for (int i = 0; i < 3; i++)
            {
                generatedOpts.Add(incorrectOpts[i]);
            }
        }

        //method to populate the grids that hold the questions and possible options
        private void populateGrids()
        {

            //finds the textblock on each grid for the options
            StackPanel gridOptOne = gridOneOpt.Children.OfType<StackPanel>().FirstOrDefault();
            TextBlock gridOptOneText = this.FindName(gridOptOne.Children.OfType<TextBlock>().FirstOrDefault().Name) as TextBlock;

            StackPanel gridOptTwo = gridTwoOpt.Children.OfType<StackPanel>().FirstOrDefault();
            TextBlock gridOptTwoText = this.FindName(gridOptTwo.Children.OfType<TextBlock>().FirstOrDefault().Name) as TextBlock;

            StackPanel gridOptThree = gridThreeOpt.Children.OfType<StackPanel>().FirstOrDefault();
            TextBlock gridOptThreeText = this.FindName(gridOptThree.Children.OfType<TextBlock>().FirstOrDefault().Name) as TextBlock;

            StackPanel gridOptFour = gridFourOpt.Children.OfType<StackPanel>().FirstOrDefault();
            TextBlock gridOptFourText = this.FindName(gridOptFour.Children.OfType<TextBlock>().FirstOrDefault().Name) as TextBlock;

            StackPanel gridOptFive = gridFiveOpt.Children.OfType<StackPanel>().FirstOrDefault();
            TextBlock gridOptFiveText = this.FindName(gridOptFive.Children.OfType<TextBlock>().FirstOrDefault().Name) as TextBlock;

            StackPanel gridOptSix = gridSixOpt.Children.OfType<StackPanel>().FirstOrDefault();
            TextBlock gridOptSixText = this.FindName(gridOptSix.Children.OfType<TextBlock>().FirstOrDefault().Name) as TextBlock;

            StackPanel gridOptSeven = gridSevenOpt.Children.OfType<StackPanel>().FirstOrDefault();
            TextBlock gridOptSevenText = this.FindName(gridOptSeven.Children.OfType<TextBlock>().FirstOrDefault().Name) as TextBlock;

            //finds the textblock on each grid for the answers
            StackPanel gridAnsOne = gridOneAns.Children.OfType<StackPanel>().FirstOrDefault();
            TextBlock gridAnsOneText = this.FindName(gridAnsOne.Children.OfType<TextBlock>().FirstOrDefault().Name) as TextBlock;

            StackPanel gridAnsTwo = gridTwoAns.Children.OfType<StackPanel>().FirstOrDefault();
            TextBlock gridAnsTwoText = this.FindName(gridAnsTwo.Children.OfType<TextBlock>().FirstOrDefault().Name) as TextBlock;

            StackPanel gridAnsThree = gridThreeAns.Children.OfType<StackPanel>().FirstOrDefault();
            TextBlock gridAnsThreeText = this.FindName(gridAnsThree.Children.OfType<TextBlock>().FirstOrDefault().Name) as TextBlock;

            StackPanel gridAnsFour = gridFourAns.Children.OfType<StackPanel>().FirstOrDefault();
            TextBlock gridAnsFourText = this.FindName(gridAnsFour.Children.OfType<TextBlock>().FirstOrDefault().Name) as TextBlock;

            //calling the fisher-yates algorithm to shuffle the lists for unique questions and options each round
            Shuffle(ddCodes);
            Shuffle(ddDesc);

            //code to switch between setting codes as questions and descriptions as answers and vice versa
            if (switchRound == false)
            {

                //sets the section heading for the questions
                tbSectionOne.Text = "Section One - These are your codes";

                //sets the section heading for the answers
                tbSectionTwo.Text = "Section Two - Match the dewey decimal codes that are directly above each block with their correct descriptions from the options below. Use the numbers as a guide";

                //sets the text of the grids to the dewey decimal codes
                tbOneQuest.Text = ddCodes[0];
                tbTwoQuest.Text = ddCodes[1];
                tbThreeQuest.Text = ddCodes[2];
                tbFourQuest.Text = ddCodes[3];

                //stores the correct description for each dewey decimal code
                correctOpts.Add(ddAreas[tbOneQuest.Text]);
                correctOpts.Add(ddAreas[tbTwoQuest.Text]);
                correctOpts.Add(ddAreas[tbThreeQuest.Text]);
                correctOpts.Add(ddAreas[tbFourQuest.Text]);

                //calls the method to populate the generated options list for the user
                populatePossibleOptionsList();

                //using the fisher-yates algorithm to shuffle the list to ensure the possible options are displayed randomly
                Shuffle(generatedOpts);

                //resets the answers
                gridAnsOneText.Text = "Drop answer one here";
                gridAnsTwoText.Text = "Drop answer two here";
                gridAnsThreeText.Text = "Drop answer three here";
                gridAnsFourText.Text = "Drop answer four here";

                //sets the possible options
                gridOptOneText.Text = generatedOpts[0];
                gridOptTwoText.Text = generatedOpts[1];
                gridOptThreeText.Text = generatedOpts[2];
                gridOptFourText.Text = generatedOpts[3];
                gridOptFiveText.Text = generatedOpts[4];
                gridOptSixText.Text = generatedOpts[5];
                gridOptSevenText.Text = generatedOpts[6];

            }

            else if (switchRound == true)
            {

                //sets the section heading for the questions
                tbSectionOne.Text = "Section One - These are your descriptions";

                //sets the section heading for the answers
                tbSectionTwo.Text = "Section Two - Match the descriptions that are directly above each block with their correct dewey decimal code from the options below. Use the numbers as a guide";

                //sets the text of the grids to the dewey decimal descriptions
                tbOneQuest.Text = ddDesc[0];
                tbTwoQuest.Text = ddDesc[1];
                tbThreeQuest.Text = ddDesc[2];
                tbFourQuest.Text = ddDesc[3];

                //getting codes for each description
                string tbOneQuestVal = ddAreas.FirstOrDefault(a => a.Value == tbOneQuest.Text).Key.ToString();
                string tbTwoQuestVal = ddAreas.FirstOrDefault(a => a.Value == tbTwoQuest.Text).Key.ToString();
                string tbThreeQuestVal = ddAreas.FirstOrDefault(a => a.Value == tbThreeQuest.Text).Key.ToString();
                string tbFourQuestVal = ddAreas.FirstOrDefault(a => a.Value == tbFourQuest.Text).Key.ToString();

                //stores the correct description for each dewey decimal code
                correctOpts.Add(tbOneQuestVal);
                correctOpts.Add(tbTwoQuestVal);
                correctOpts.Add(tbThreeQuestVal);
                correctOpts.Add(tbFourQuestVal);

                //calls the method to populate the generated options list for the user
                populatePossibleOptionsList();

                //using the fisher-yates algorithm to shuffle the list to ensure the possible options are displayed randomly
                Shuffle(generatedOpts);

                //resets the answers
                gridAnsOneText.Text = "Drop answer one here";
                gridAnsTwoText.Text = "Drop answer two here";
                gridAnsThreeText.Text = "Drop answer three here";
                gridAnsFourText.Text = "Drop answer four here";

                //sets the possible options
                gridOptOneText.Text = generatedOpts[0];
                gridOptTwoText.Text = generatedOpts[1];
                gridOptThreeText.Text = generatedOpts[2];
                gridOptFourText.Text = generatedOpts[3];
                gridOptFiveText.Text = generatedOpts[4];
                gridOptSixText.Text = generatedOpts[5];
                gridOptSevenText.Text = generatedOpts[6];

            }

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

        private void displayAnswers()
        {
            tbCorrectDialog.Text = "You got " + correctCount.ToString() + " answers correct :)";

            if (incorrectCount == 0)
            {
                tbIncorrectDialog.Text = "You got every answer correct, have a cookie :)";
            }

            else if (incorrectCount > 0)
            {
                tbIncorrectDialog.Text = "You got " + incorrectCount.ToString() + " answers wrong :(";

                lvWrongAnswers.ItemsSource = incorrectAnsList;
            }
        }

        //method to save the user's score
        private void saveUserScore()
        {
            //current date
            var time = DateTime.Now;

            score.saveUserScore(dBHelper.openConn(connection), userID, correctCount.ToString(), tbModeTimer.Text, time.ToString(), gameType);

            dBHelper.closeConn();

        }

        private void btnSubmitOptions_Click(object sender, RoutedEventArgs e)
        {

            if (!validateAnswers())
            {
                invalidAnswersDialog.IsOpen = true;
            }

            else
            {
                //stops the timer when the user is finished
                if (stopwatch.IsRunning)
                {
                    stopwatch.Stop();
                }

                checkAnswers();

                saveUserScore();

                //updates to new high score at the end of each game for user
                tbModeHighScore.Text = getHighScore();

                displayAnswers();

                resultsDialog.IsOpen = true;

                btnSubmitOptions.IsEnabled = false;

                btnPlayModeAgain.IsEnabled = true;
            }

        }

        //method to get the value of each of the three answers the user has given
        private void getAnswers()
        {
            StackPanel gridOne = gridOneAns.Children.OfType<StackPanel>().FirstOrDefault();
            TextBlock gridOneAnsText = this.FindName(gridOne.Children.OfType<TextBlock>().FirstOrDefault().Name) as TextBlock;

            StackPanel gridTwo = gridTwoAns.Children.OfType<StackPanel>().FirstOrDefault();
            TextBlock gridTwoAnsText = this.FindName(gridTwo.Children.OfType<TextBlock>().FirstOrDefault().Name) as TextBlock;

            StackPanel gridThree = gridThreeAns.Children.OfType<StackPanel>().FirstOrDefault();
            TextBlock gridThreeAnsText = this.FindName(gridThree.Children.OfType<TextBlock>().FirstOrDefault().Name) as TextBlock;

            StackPanel gridFour = gridFourAns.Children.OfType<StackPanel>().FirstOrDefault();
            TextBlock gridFourAnsText = this.FindName(gridFour.Children.OfType<TextBlock>().FirstOrDefault().Name) as TextBlock;

            ansOne = gridOneAnsText.Text;
            ansTwo = gridTwoAnsText.Text;
            ansThree = gridThreeAnsText.Text;
            ansFour = gridFourAnsText.Text;

        }

        //method to check the correct and incorrect answers
        private void checkAnswers()
        {

            //calls method to get the user's answers
            getAnswers();

            //when questions are dewey decimal codes and answers are descriptions
            if (switchRound == false)
            {

                //get the keys(codes) of each value(description) from the dictionary
                string answerOneKey = ddAreas.FirstOrDefault(a => a.Value == ansOne).Key.ToString();
                string answerTwoKey = ddAreas.FirstOrDefault(a => a.Value == ansTwo).Key.ToString();
                string answerThreeKey = ddAreas.FirstOrDefault(a => a.Value == ansThree).Key.ToString();
                string answerFourKey = ddAreas.FirstOrDefault(a => a.Value == ansFour).Key.ToString();

                if (tbOneQuest.Text != answerOneKey) { incorrectAnsList.Add("Your first answer was wrong"); incorrectCount += 1; }
                else { correctCount += 1; }

                if (tbTwoQuest.Text != answerTwoKey) { incorrectAnsList.Add("Your second answer was wrong"); incorrectCount += 1; }
                else { correctCount += 1; }

                if (tbThreeQuest.Text != answerThreeKey) { incorrectAnsList.Add("Your third answer was wrong"); incorrectCount += 1; }
                else { correctCount += 1; }

                if (tbFourQuest.Text != answerFourKey) { incorrectAnsList.Add("Your fourth answer was wrong"); incorrectCount += 1; }
                else { correctCount += 1; }

            }

            //when questions are values(descriptions) and answers are keys(codes)
            else if (switchRound)
            {

                Debug.WriteLine(ansOne);

                //get the values(descriptions) of each key(code) from the dictionary
                string answerOneVal = ddAreas.FirstOrDefault(a => a.Key == ansOne).Value.ToString();
                string answerTwoVal = ddAreas.FirstOrDefault(a => a.Key == ansTwo).Value.ToString();
                string answerThreeVal = ddAreas.FirstOrDefault(a => a.Key == ansThree).Value.ToString();
                string answerFourVal = ddAreas.FirstOrDefault(a => a.Key == ansFour).Value.ToString();

                if (tbOneQuest.Text != answerOneVal) { incorrectAnsList.Add("Your first answer was wrong"); incorrectCount += 1; }
                else { correctCount += 1; }

                if (tbTwoQuest.Text != answerTwoVal) { incorrectAnsList.Add("Your second answer was wrong"); incorrectCount += 1; }
                else { correctCount += 1; }

                if (tbThreeQuest.Text != answerThreeVal) { incorrectAnsList.Add("Your third answer was wrong"); incorrectCount += 1; }
                else { correctCount += 1; }

                if (tbFourQuest.Text != answerFourVal) { incorrectAnsList.Add("Your fourth answer was wrong"); incorrectCount += 1; }
                else { correctCount += 1; }

            }

        }

        //method to make sure a user has provided an answer
        private bool validateAnswers()
        {

            bool isValidated = true;

            StackPanel gridOne = gridOneAns.Children.OfType<StackPanel>().FirstOrDefault();
            TextBlock gridOneAnsText = this.FindName(gridOne.Children.OfType<TextBlock>().FirstOrDefault().Name) as TextBlock;

            StackPanel gridTwo = gridTwoAns.Children.OfType<StackPanel>().FirstOrDefault();
            TextBlock gridTwoAnsText = this.FindName(gridTwo.Children.OfType<TextBlock>().FirstOrDefault().Name) as TextBlock;

            StackPanel gridThree = gridThreeAns.Children.OfType<StackPanel>().FirstOrDefault();
            TextBlock gridThreeAnsText = this.FindName(gridThree.Children.OfType<TextBlock>().FirstOrDefault().Name) as TextBlock;

            StackPanel gridFour = gridFourAns.Children.OfType<StackPanel>().FirstOrDefault();
            TextBlock gridFourAnsText = this.FindName(gridFour.Children.OfType<TextBlock>().FirstOrDefault().Name) as TextBlock;

            if (gridOneAnsText.Text == "Drop answer one here" || gridTwoAnsText.Text == "Drop answer two here" || gridThreeAnsText.Text == "Drop answer three here" || gridFourAnsText.Text == "Drop answer four here")
            {

                isValidated = false;

            }

            return isValidated;

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

        //sets the column names for the data grid view
        private void dgScores_AutoGeneratedColumns(object sender, EventArgs e)
        {
            dgScores.Columns[0].Header = "Score";
            dgScores.Columns[1].Header = "Time you took (minutes:seconds:milliseconds)";
            dgScores.Columns[2].Header = "Date and time Completed";
        }

        private void btnViewModeScore_Click(object sender, RoutedEventArgs e)
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

        private void btnLogOff_Click(object sender, RoutedEventArgs e)
        {

            Login login = new Login();

            login.Show();

            this.Close();

        }

        private void btnPlayModeAgain_Click(object sender, RoutedEventArgs e)
        {

            //code to switch between dewey decimal codes and descriptions as questions and answers and vice versa
            switch (switchRound)
            {
                case false:
                    switchRound = true;
                    break;

                case true:
                    switchRound = false;
                    break;

                default:
                    switchRound = false;
                    break;
            }

            correctCount = 0;
            incorrectCount = 0;
            incorrectOpts.Clear();
            incorrectAnsList.Clear();
            correctOpts.Clear();
            generatedOpts.Clear();

            btnSubmitOptions.IsEnabled = true;
            btnPlayModeAgain.IsEnabled = false;

            //resets the timer for the user
            stopwatch.Restart();
            tbModeTimer.Text = "00:00:00";

            populateGrids();

        }

        //method handles the grid to be dragged over to its new position
        private void grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Grid sendingGrid = (Grid)sender;

            DataObject dataObject = new DataObject(sendingGrid);

            DragDrop.DoDragDrop(sendingGrid, dataObject, DragDropEffects.Move);
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
