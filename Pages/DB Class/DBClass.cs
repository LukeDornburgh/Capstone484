using System.Data.SqlClient;
using Lab1.Pages.DB;
using Lab1.Pages.DataClasses;
using System.Data.SqlTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System;

namespace Lab1.Pages.DB_Class
{
    public class DBClass
    {


        //connection string
        private static readonly string Lab1ConStr = @"Server=Localhost;Database=Lab1;Trusted_Connection=True";
        private static readonly string AuthConStr = @"Server=Localhost;Database=AUTH;Trusted_Connection=True";

        public static SqlDataReader TableReader(string email)
        {
            //Sql command
            //set it properites
            //open a connection
            //issue the query
            //capture those results and return them
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = "SELECT * FROM Users WHERE Users.Email = '" + email + "'";
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();

            return tempReader;

        }

        public static SqlDataReader UserReader(string email)
        {

            int temp = GetUserIDSession(email);
            //Sql command
            //set it properites
            //open a connection
            //issue the query
            //capture those results and return them
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = "SELECT * FROM Users WHERE NOT Users.UserID = " + temp + ";";
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();

            return tempReader;

        }

        public static SqlDataReader UserReader()
        {

            //Sql command
            //set it properites
            //open a connection
            //issue the query
            //capture those results and return them
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = "SELECT * FROM Users;";
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();

            return tempReader;

        }

        public static SqlDataReader ProjectsTableReader()
        {
            //Sql command
            //set it properites
            //open a connection
            //issue the query
            //capture those results and return them



            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = "SELECT * FROM Projects";
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();



            return tempReader;
        }

        public static SqlDataReader MyProjectsTableReader(string email)
        {
            //Sql command
            //set it properites
            //open a connection
            //issue the query
            //capture those results and return them



            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = "Select * from projects where Projects.UserID in (select Users.UserID from Users WHERE Users.Email = '" + email + "')";
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();



            return tempReader;
        }

        public static SqlDataReader SkillsTableReader()
        {
            //Sql command
            //set it properites
            //open a connection
            //issue the query
            //capture those results and return them

            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = "SELECT * FROM Skills";
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();

            return tempReader;

        }

        public static SqlDataReader TeamsTableReader()
        {
            //Sql command
            //set it properites
            //open a connection
            //issue the query
            //capture those results and return them



            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = "SELECT * FROM Teams";
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();



            return tempReader;
        }

        public static SqlDataReader MyTeamsTableReader(string email)
        {
            //Sql command
            //set it properites
            //open a connection
            //issue the query
            //capture those results and return them



            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = "Select * from Teams where teams.TeamID in (Select TeamMembers.TeamId from TeamMembers where TeamMembers.UserID  in (select Users.UserID from Users WHERE Users.Email = '" + email + "'));";
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();



            return tempReader;
        }

        public static SqlDataReader TeamMeetingsTableReader(string email)
        {
            //Sql command
            //set it properites
            //open a connection
            //issue the query
            //capture those results and return them

            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = "Select * from TeamMeetings where TeamMeetings.TeamID in(Select teams.TeamID from Teams where teams.TeamID in (Select TeamMembers.TeamId from TeamMembers where TeamMembers.UserID  in (select Users.UserID from Users WHERE Users.Email = '" + email + "')));";
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();

            return tempReader;
        }



        public static void InsertSkill(Skills s)
        {
            string sqlQuery = "INSERT INTO Skills (SkillName) VALUES (";
            sqlQuery += "'" + s.SkillName + "')";
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = sqlQuery;
            cmdProductRead.Connection.Open();
            cmdProductRead.ExecuteNonQuery();
        }

        public static void InsertProject(Projects p)
        {
            string sqlQuery = "INSERT INTO Projects (ProjectName, ProjectDescription, ProjectBeginDate, ProjectMission, ProjectType, UserID ) VALUES (";
            sqlQuery += "'" + p.ProjectName + "',";
            sqlQuery += "'" + p.ProjectDescription + "',";
            sqlQuery += "'" + p.ProjectBeginDate + "',";
            sqlQuery += "'" + p.ProjectMission + "',";
            sqlQuery += "'" + p.ProjectType + "',";
            sqlQuery += "'" + p.UserID + "')";
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = sqlQuery;
            cmdProductRead.Connection.Open();
            cmdProductRead.ExecuteNonQuery();

            //get the project ID of the new project
            int pID = GetProjectID(p.ProjectName);

            //now we need to create a team related to this new project
            string sqlQuery1 = "INSERT INTO Teams (TeamName, ProjectID ) VALUES (";
            sqlQuery1 += "'" + p.ProjectName + "',";
            sqlQuery1 += pID + ")";
            SqlCommand sqlProductRead2 = new SqlCommand();
            sqlProductRead2.Connection = new SqlConnection();
            sqlProductRead2.Connection.ConnectionString = Lab1ConStr;
            sqlProductRead2.CommandText = sqlQuery1;
            sqlProductRead2.Connection.Open();
            sqlProductRead2.ExecuteNonQuery();

            //get the teamID of the new team
            int temp2 = GetTeamID(p.ProjectName);

            //now insert the project owner as a team member 
            string sqlQuery2 = "INSERT INTO TeamMembers (UserID, TeamID, Role) VALUES (";
            sqlQuery2 += p.UserID + ", ";
            sqlQuery2 += temp2 + ", ";
            sqlQuery2 += "'Project Owner');";
            SqlCommand sqlProductRead3 = new SqlCommand();
            sqlProductRead3.Connection = new SqlConnection();
            sqlProductRead3.Connection.ConnectionString = Lab1ConStr;
            sqlProductRead3.CommandText = sqlQuery2;
            sqlProductRead3.Connection.Open();
            sqlProductRead3.ExecuteNonQuery();


        }

        public static int GetTeamID(string teamName)
        {
            string sqlQuery = "Select Teams.TeamID from Teams where Teams.TeamName = '" + teamName + "';";

            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = sqlQuery;
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();

            int result = 0;

            while (tempReader.Read())
            {
                result += (int)tempReader["TeamID"];
            }

            return result;

        }

        public static int GetProjectID(string projectName)
        {
            string sqlQuery = "Select Projects.ProjectID from Projects where Projects.ProjectName = '" + projectName + "';";

            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = sqlQuery;
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();

            int result = 0;

            while (tempReader.Read())
            {
                result += (int)tempReader["ProjectID"];
            }

            return result;

        }

        public static void InsertTeam(Teams t, string email)
        {
            string sqlQuery = "INSERT INTO Teams (TeamName, TeamEmailAddress, TeamDescription, ProjectID ) VALUES (";
            sqlQuery += "'" + t.TeamName + "',";
            sqlQuery += "'" + t.TeamEmailAddress + "',";
            sqlQuery += "'" + t.TeamDescription + "',";
            sqlQuery += "'" + t.ProjectID + "')";
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = sqlQuery;
            cmdProductRead.Connection.Open();
            cmdProductRead.ExecuteNonQuery();

            int user = GetUserIDSession(email);
            int teamID = GetTeamID(t.TeamName);


            string sqlQuery1 = "INSERT INTO TeamMembers (UserID, TeamID, Role) VALUES (";
            sqlQuery1 += user;
            sqlQuery1 += ", " + teamID + ", ";
            sqlQuery1 += "'Project Leader');";
            SqlCommand cmdProductRead1 = new SqlCommand();
            cmdProductRead1.Connection = new SqlConnection();
            cmdProductRead1.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead1.CommandText = sqlQuery1;
            cmdProductRead1.Connection.Open();
            cmdProductRead1.ExecuteNonQuery();
        }

        public static SqlDataReader GetSpecificSkillObject(int num)
        {
            string sqlQuery = "Select * from Skills where Skills.SkillID = " + num + ";";
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = sqlQuery;
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();
            return tempReader;
        }

        public static void InsertTeamMeeting(TeamMeetings t)
        {
            string sqlQuery = "INSERT INTO TeamMeetings (Date, Time, Notes, Location, TeamID ) VALUES (";
            sqlQuery += "'" + t.Date + "',";
            sqlQuery += "'" + t.Time + "',";
            sqlQuery += "'" + t.Notes + "',";
            sqlQuery += "'" + t.Location + "',";
            sqlQuery += "'" + t.TeamID + "')";
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = sqlQuery;
            cmdProductRead.Connection.Open();
            cmdProductRead.ExecuteNonQuery();
        }

        public static SqlDataReader SingleUserReader(int UserID)
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = "SELECT * from Users WHERE UserID = " + UserID;
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();

            return tempReader;
        }

        public static void UpdateUser(Users p)
        {
            string sqlQuery = "Update users SET ";
            sqlQuery += "FirstName='" + p.FirstName + "',";
            sqlQuery += "LastName='" + p.LastName + "',";
            sqlQuery += "Email='" + p.Email + "',";
            sqlQuery += "ProfessionalEmail='" + p.ProfessionalEmail + "',";
            sqlQuery += "PersonalInterests='" + p.PersonalInterests + "',";
            sqlQuery += "ProfessionalInterests='" + p.ProfessionalInterests + "',";
            sqlQuery += "Bio='" + p.Bio + "',";
            sqlQuery += "College='" + p.College + "',";
            if (p.ProfilePicturePath != null)
            {
                sqlQuery += "ProfilePicturePath=@ProfilePicturePath,";
            }
            sqlQuery += "Position='" + p.Position + "'" + "WHERE UserID=" + p.UserID;
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = sqlQuery;
            if(p.ProfilePicturePath != null)
            {
                cmdProductRead.Parameters.AddWithValue("@ProfilePicturePath", p.ProfilePicturePath);
            }
            cmdProductRead.Connection.Open();
            cmdProductRead.ExecuteNonQuery();
        }

        public static SqlDataReader SingleSkillReader(int SkillID)
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = "SELECT * from Skills WHERE SkillID = " + SkillID;
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();

            return tempReader;
        }

        public static void UpdateSkill(Skills s)
        {
            string sqlQuery = "Update Skills SET ";
            sqlQuery += "SkillName='" + s.SkillName + "'" + "WHERE SkillID=" + s.SkillID;
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = sqlQuery;
            cmdProductRead.Connection.Open();
            cmdProductRead.ExecuteNonQuery();
        }

        public static SqlDataReader SingleProjectReader(int ProjectID)
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = "Select * from Projects WHERE ProjectID = " + ProjectID;
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();
            return tempReader;
        }



        public static void UpdateProject(Projects p)
        {
            string sqlQuery = "UPDATE Projects SET ";
            sqlQuery += "ProjectName='" + p.ProjectName + "',";
            sqlQuery += "ProjectDescription='" + p.ProjectDescription + "',";
            sqlQuery += "ProjectBeginDate='" + p.ProjectBeginDate + "',";
            sqlQuery += "ProjectMission='" + p.ProjectMission + "',";
            sqlQuery += "ProjectType='" + p.ProjectType + "'" + "WHERE ProjectID=" + p.ProjectID;
            Console.WriteLine(sqlQuery);

            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = sqlQuery;
            cmdProductRead.Connection.Open();
            cmdProductRead.ExecuteNonQuery();
        }

        public static SqlDataReader SingleTeamReader(int TeamID)
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = "Select * from Teams WHERE TeamID = " + TeamID;
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();



            return tempReader;
        }



        public static void UpdateTeam(Teams p)
        {
            string sqlQuery = "UPDATE Teams SET ";
            sqlQuery += "TeamName='" + p.TeamName + "',";
            sqlQuery += "TeamEmailAddress='" + p.TeamEmailAddress + "',";
            sqlQuery += "TeamDescription='" + p.TeamDescription + "'" + "WHERE TeamID=" + p.TeamID;




            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = sqlQuery;
            cmdProductRead.Connection.Open();
            cmdProductRead.ExecuteNonQuery();
        }



        public static SqlDataReader SingleTeamMeetingReader(int TeamMeetingID)
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = "Select * from TeamMeetings WHERE TeamID = " + TeamMeetingID;
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();



            return tempReader;
        }



        public static void UpdateTeamMeeting(TeamMeetings p)
        {
            string sqlQuery = "UPDATE TeamMeetings SET ";
            sqlQuery += "Date='" + p.Date + "',";
            sqlQuery += "Time='" + p.Time + "',";
            sqlQuery += "Notes='" + p.Notes + "',";
            sqlQuery += "Location='" + p.Location + "'" + "WHERE TeamMeetingID=" + p.TeamMeetingID;




            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = sqlQuery;
            cmdProductRead.Connection.Open();
            cmdProductRead.ExecuteNonQuery();
        }

        public static void PopulateSkillBridge(Users u, int s)
        {
            string sqlQuery = "INSERT INTO SkillsAssociation (SkillID, UserID) VALUES (";
            sqlQuery += "'" + s + "',";
            sqlQuery += "'" + u.UserID + "')";

            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = sqlQuery;
            cmdProductRead.Connection.Open();
            cmdProductRead.ExecuteNonQuery();
        }

        public static void RemoveSkillFromUser(Users u, int s)
        {
            string sqlQuery = "DELETE from SkillsAssociation WHERE SkillsAssociation.SkillID = " + s + " AND SkillsAssociation.UserID = " + u.UserID + ";";
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = sqlQuery;
            cmdProductRead.Connection.Open();
            cmdProductRead.ExecuteNonQuery();
        }

        //General reader query for bypassing the model page and calling a query from the view page via razor code

        public static SqlDataReader GeneralReaderQuery(string sqlQuery)
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = sqlQuery;
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();

            return tempReader;
        }

        public static void PopulateTeamMembersBridge(Users u, int s)
        {
            string sqlQuery = "INSERT INTO TeamMembers (UserID, TeamID) VALUES (";
            sqlQuery += "'" + u.UserID + "',";
            sqlQuery += "'" + s + "')";

            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = sqlQuery;
            cmdProductRead.Connection.Open();
            cmdProductRead.ExecuteNonQuery();
        }

        // New Methods for DBClass.cs
        // Clear-Text Login
        // Parameterized Login
        // Creating a User and Login with Password Hashing

        public static int SecureLogin(string Username, string Password)
        {
            string loginQuery =
            "SELECT COUNT(*) FROM Users where Email = @Username and Passwd = @Password";
            SqlCommand cmdLogin = new SqlCommand();
            cmdLogin.Connection = new SqlConnection();
            cmdLogin.Connection.ConnectionString = Lab1ConStr;
            cmdLogin.CommandText = loginQuery;
            cmdLogin.Parameters.AddWithValue("@Username", Username);
            cmdLogin.Parameters.AddWithValue("@Password", Password);
            cmdLogin.Connection.Open();
            // ExecuteScalar() returns back data type Object
            // Use a typecast to convert this to an int.
            // Method returns first column of first row.
            int rowCount = (int)cmdLogin.ExecuteScalar();
            return rowCount;
        }

        public static bool HashedParameterLogin(string Username, string Password)
        {
            string loginQuery =
                "SELECT Password FROM HashedCredentials WHERE Username = @Username";

            SqlCommand cmdLogin = new SqlCommand();
            cmdLogin.Connection = new SqlConnection();
            cmdLogin.Connection.ConnectionString = AuthConStr;

            cmdLogin.CommandText = loginQuery;
            cmdLogin.Parameters.AddWithValue("@Username", Username);

            cmdLogin.Connection.Open();

            // ExecuteScalar() returns back data type Object
            // Use a typecast to convert this to an int.
            // Method returns first column of first row.
            SqlDataReader hashReader = cmdLogin.ExecuteReader();
            if (hashReader.Read())
            {
                string correctHash = hashReader["Password"].ToString();

                if (PasswordHash.ValidatePassword(Password, correctHash))
                {
                    return true;
                }
            }

            return false;
        }


        public static bool StoredProcedureLogin(string Username, string Password)
        {

            //call the stored procedure to retrieve the true password from the DB based on the Username 
            //check the parameter hashed and the retrival from the SP. 



            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = AuthConStr;
            cmdProductRead.CommandType = System.Data.CommandType.StoredProcedure;
            cmdProductRead.Parameters.AddWithValue("@Username", Username);
            cmdProductRead.CommandText = "sp_hashedlogin";
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();

            string correctHash = "";
            while (tempReader.Read())
            {
                correctHash = tempReader["Password"].ToString();
            }

            if (PasswordHash.ValidatePassword(Password, correctHash))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static void CreateHashedUser(string Username, string Password, Users u)
        {
            string loginQuery =
                "INSERT INTO HashedCredentials (Username,Password) values (@Username, @Password)";

            SqlCommand cmdLogin = new SqlCommand();
            cmdLogin.Connection = new SqlConnection();
            cmdLogin.Connection.ConnectionString = AuthConStr;

            cmdLogin.CommandText = loginQuery;
            cmdLogin.Parameters.AddWithValue("@Username", Username);
            cmdLogin.Parameters.AddWithValue("@Password", PasswordHash.HashPassword(Password));

            cmdLogin.Connection.Open();

            // ExecuteScalar() returns back data type Object
            // Use a typecast to convert this to an int.
            // Method returns first column of first row.
            cmdLogin.ExecuteNonQuery();

            string sqlQuery = "INSERT INTO Users (FirstName, LastName, Email, ProfessionalEmail, Phone, Position) VALUES (";
            sqlQuery += "'" + u.FirstName + "',";
            sqlQuery += "'" + u.LastName + "',";
            sqlQuery += "'" + u.Email + "',";
            sqlQuery += "'" + u.ProfessionalEmail + "',";
            sqlQuery += "'" + u.Phone + "',";
            sqlQuery += "'" + u.Position + "');";
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = sqlQuery;
            cmdProductRead.Connection.Open();
            cmdProductRead.ExecuteNonQuery();

        }

        public static int NotificationNumber(int userID)
        {

            string sqlQuery = "SELECT Count(Users.FirstName) as number FROM Users JOIN Requests ON Requests.UserID = Users.UserID JOIN Projects " +
            "ON Projects.ProjectID = Requests.ProjectID" +
            " where Requests.ProjectOwnerID = ";
            sqlQuery += userID + "AND Requests.Status = 'Pending';";

            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = sqlQuery;
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();

            int result = 0;

            while (tempReader.Read())
            {
                result += (int)tempReader["number"];
            }

            return result;
        }

        public static SqlDataReader ProjectCardDisplay()
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = "Select Projects.ProjectID, Projects.UserID, Projects.ProjectName, Projects.ProjectDescription, " +
            "Projects.ProjectBeginDate, Projects.ProjectMission, Projects.ProjectType, " +
            "concat(Users.FirstName, ' ', Users.LastName) as ProjectOwner FROM PROJECTS " +
            "JOIN USERS ON Users.UserID = projects.UserID ;";
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();
            return tempReader;
        }


        public static SqlDataReader UserSearch(string searchText)
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandType = System.Data.CommandType.StoredProcedure;
            cmdProductRead.Parameters.AddWithValue("@SearchText", searchText);
            cmdProductRead.CommandText = "sp_usersearch"; 
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();
            return tempReader;
        }

        public static SqlDataReader FilterUsersBySkill(string filterList, string email)
        {
            int temp = GetUserIDSession(email);

            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = "Select * from (Select DISTINCT * FROM Users WHERE Users.UserID " +
                "in(Select SkillsAssociation.UserID from SkillsAssociation where SkillsAssociation.SkillID in " +
                " (Select Skills.SkillID from Skills WHERE Skills.SkillID in (select value from string_split('" + filterList + "', ' '))))) " +
                " as a WHERE NOT UserID = " + temp + ";";
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();
            return tempReader;
        }

        public static SqlDataReader SkillSearch(string searchText)
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = "Select Skills.SkillID, Skills.SkillName from Skills" +
                " where Skills.SkillName Like '%' + '" + searchText + "' + '%';";
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();
            return tempReader;
        }

        public static void InsertRequest(int ProjectID, int ProjectOwnerID, string email)
        {
            int userIDTemp = GetUserIDSession(email);

            string sqlQuery = "INSERT INTO Requests (UserID, ProjectID, ProjectOwnerID, Status) VALUES (";
            sqlQuery += userIDTemp + ",";
            sqlQuery += ProjectID + ",";
            sqlQuery += ProjectOwnerID + ",";
            sqlQuery += " 'Pending');";

            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = sqlQuery;
            cmdProductRead.Connection.Open();
            cmdProductRead.ExecuteNonQuery();
        }

        public static void InsertInvite(int UserID, int ProjectID, string email)
        {
            int OwnerIDTemp = GetUserIDSession(email);

            string sqlQuery = "INSERT INTO Invites (UserID, ProjectID, ProjectOwnerID, Status) VALUES (";
            sqlQuery += UserID + ",";
            sqlQuery += ProjectID + ",";
            sqlQuery += OwnerIDTemp + ",";
            sqlQuery += " 'Pending');";

            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = sqlQuery;
            cmdProductRead.Connection.Open();
            cmdProductRead.ExecuteNonQuery();
        }

        public static int GetUserIDSession(string email)
        {
            int result = 0;

            string sqlQuery = "SELECT Users.UserID from Users where Users.Email = '" + email + "';";

            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = sqlQuery;
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();

            while (tempReader.Read())
            {
                result = (int)tempReader["UserID"];
            }
            return result;
        }

        public static SqlDataReader RequestTableReader(string email)
        {
            int OwnerID = GetUserIDSession(email);


            string sqlQuery = "SELECT concat(Users.FirstName, ' ',  Users.LastName) as 'FullName'," +
                " Users.Email, Requests.Status, Requests.ProjectID, Requests.UserID, Projects.ProjectName" +
                " FROM Users JOIN Requests ON Requests.UserID = Users.UserID" +
                " JOIN Projects ON Projects.ProjectID = Requests.ProjectID" +
                " where Requests.ProjectOwnerID = " + OwnerID + "ORDER BY Requests.Status;";


            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = sqlQuery;
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();

            return tempReader;

        }

        public static SqlDataReader RequestButtonStatus(int projectID, string email)
        {
            int temp = GetUserIDSession(email);

            string sqlQuery = "Select * from Requests where Requests.ProjectID = " + projectID + " AND Requests.UserID = " + temp + ";";
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = sqlQuery;
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();

            return tempReader;
        } 

        public static SqlDataReader InviteTableReader(string email)
        {
            int OwnerID = GetUserIDSession(email);


            string sqlQuery = "SELECT concat(Users.FirstName, ' ', Users.LastName) as 'ProjectOwner', Projects.ProjectName, Projects.ProjectType, Invites.Status, " +
                " Invites.ProjectID, Invites.UserID as 'UserIDBeingInvited' FROM Users" +
                " JOIN Projects on Projects.UserID = Users.UserID" +
                " JOIN Invites on Invites.ProjectID = Projects.ProjectID" +
                " WHERE Invites.UserID = 5;";


            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = sqlQuery;
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();

            return tempReader;

        }

        public static void ApproveRequest(int projectID, int userID)
        {


            string sqlQuery = "SELECT Teams.TeamID from Teams" +
                " JOIN Projects on Projects.ProjectID = Teams.ProjectID" +
                " JOIN Requests on Requests.ProjectID = Projects.ProjectID" +
                " WHERE requests.ProjectID = " + projectID + ";";

            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead.CommandText = sqlQuery;
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();

            int teamID = 0;

            while (tempReader.Read())
            {
                teamID = (int)tempReader["TeamID"];
            }

            string sqlQuery1 = "INSERT INTO TeamMembers (UserID, TeamID) VALUES (";
            sqlQuery1 += userID + ", ";
            sqlQuery1 += teamID + ");";

            SqlCommand cmdProductRead1 = new SqlCommand();
            cmdProductRead1.Connection = new SqlConnection();
            cmdProductRead1.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead1.CommandText = sqlQuery1;
            cmdProductRead1.Connection.Open();
            cmdProductRead1.ExecuteNonQuery();

            //now to chnage the status of the request from pending to approved

            string sqlQuery2 = "Update Requests SET Status = 'Approved' where Requests.ProjectID = " + projectID + " AND Requests.UserID = " + userID + ";";

            SqlCommand cmdProductRead2 = new SqlCommand();
            cmdProductRead2.Connection = new SqlConnection();
            cmdProductRead2.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead2.CommandText = sqlQuery2;
            cmdProductRead2.Connection.Open();
            cmdProductRead2.ExecuteNonQuery();

        }

        public static void DenyRequest(int ProjectID, int UserID)
        {
            string sqlQuery = "Update Requests SET Status = 'Denied' where Requests.ProjectID = " + ProjectID + " AND Requests.UserID = " + UserID + ";";

            SqlCommand cmdProductRead2 = new SqlCommand();
            cmdProductRead2.Connection = new SqlConnection();
            cmdProductRead2.Connection.ConnectionString = Lab1ConStr;
            cmdProductRead2.CommandText = sqlQuery;
            cmdProductRead2.Connection.Open();
            cmdProductRead2.ExecuteNonQuery();
        }


    }

}

