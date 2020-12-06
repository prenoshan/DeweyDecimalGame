using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreDLL
{
    public class Score
    {

        //method to get the user's scores
        public DataTable getScores(SqlConnection conn, int id, string gameType)
        {

            DataTable dt = new DataTable();

            try
            {
                string q_GetScores = "select score,timeTaken,dateCompleted from userscores where userid = @id and gametype = @gametype";

                SqlCommand cmd = new SqlCommand(q_GetScores, conn);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@gametype", gameType);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                sda.Fill(dt);
            }
            catch (Exception e)
            {

                Debug.WriteLine(e);
            }

            return dt;

        }

        //method to get current high score for user
        public string getHighScore(SqlConnection conn, int id, string gameType)
        {

            string highScore = "";

            try
            {

                string q_GetHighScore = "select top 1 score from userscores where userid = @id and gametype = @gametype order by score desc";

                SqlCommand cmd = new SqlCommand(q_GetHighScore, conn);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@gametype", gameType);

                highScore = Convert.ToString(cmd.ExecuteScalar());

            }
            catch (Exception e)
            {

                Debug.WriteLine(e);
            }

            return highScore;

        }

        //method to save the user's score into the database
        public void saveUserScore(SqlConnection conn, int id, string score, string timeTaken, string dateCompleted, string gameType)
        {

            try
            {
                string q_SaveScore = "insert into userscores(userid,score,timetaken,datecompleted,gametype) values(@id,@score,@timeTaken,@dateCompleted,@gametype)";

                SqlCommand cmd = new SqlCommand(q_SaveScore, conn);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@score", score);
                cmd.Parameters.AddWithValue("@timeTaken", timeTaken);
                cmd.Parameters.AddWithValue("@dateCompleted", dateCompleted);
                cmd.Parameters.AddWithValue("@gametype", gameType);

                cmd.ExecuteScalar();

            }
            catch (Exception e)
            {

                Debug.WriteLine(e);
            }

        }

    }
}
