using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDLL
{
    public class Auth
    {

        //method to get the user's username
        public string getUsername(SqlConnection conn, int id)
        {

            string username = "";

            try
            {

                string q_GetUsername = "select usernames from userinfo where id = @id";

                SqlCommand cmd = new SqlCommand(q_GetUsername, conn);

                cmd.Parameters.AddWithValue("@id", id);

                username = Convert.ToString(cmd.ExecuteScalar());

            }
            catch (Exception e)
            {

                Debug.WriteLine(e);
            }

            return username;

        }

        //method to return the user id
        public int getUserID(SqlConnection conn, string username)
        {

            int id = 0;

            try
            {

                string q_GetUserID = "select id from userinfo where usernames = @username";

                SqlCommand cmd = new SqlCommand(q_GetUserID, conn);

                cmd.Parameters.AddWithValue("@username", username);

                id = Convert.ToInt32(cmd.ExecuteScalar());

            }

            catch (Exception e)
            {

                Debug.WriteLine(e);
            }

            return id;

        }

        //method to avoid duplicate users
        public bool checkUser(SqlConnection conn, string username)
        {

            bool isExists = false;

            try
            {

                string q_CheckUser = "select usernames from userinfo where usernames = @username";

                SqlCommand cmd = new SqlCommand(q_CheckUser, conn);

                cmd.Parameters.AddWithValue("@username", username);

                if(cmd.ExecuteScalar() != null)
                {
                    isExists = true;
                }

            }

            catch (Exception e)
            {

                Debug.WriteLine(e);
            }

            return isExists;

        }

        //method to register users to the database
        public void register(SqlConnection conn, string username, string password)
        {

            //query to insert a new user into the database
            try
            {

                string q_Register = "insert into userinfo(usernames,passwords) values(@username,@password)";

                SqlCommand cmd = new SqlCommand(q_Register, conn);

                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                cmd.ExecuteScalar();


            }

            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

        }

        //method to login users
        public bool login(SqlConnection conn, string username, string password)
        {

            bool userExists = false;

            try
            {

                string q_Login = "select id from userinfo where usernames = @username and passwords = @password";

                SqlCommand cmd = new SqlCommand(q_Login, conn);

                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                if(cmd.ExecuteScalar() != null)
                {
                    userExists = true;
                }

            }

            catch (Exception e)
            {

                Debug.WriteLine(e);

            }

            return userExists;
        }

    }
}
