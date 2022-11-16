using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Lab1.Pages.DataClasses;
using Lab1.Pages.DB_Class;
using System.Data.SqlClient;
using System.Numerics;

namespace Lab1.Pages.UsersPages
{
    public class MessagesModel : PageModel
    {
        public List<Users> UserList { get; set; }

        public MessagesModel()
        {
            UserList = new List<Users>();
        }
        public void OnGet()
        {
            //this page should serve just as a place that users see their exisitng conversations with other users


            SqlDataReader userReader = DBClass.UserReader(HttpContext.Session.GetString("username"));
            //Loop through the rows of the product reader
            //for each record in product reader
            //create a new instance object of Product and fill its properties with the columns from that DB row.
            while (userReader.Read())
            {
                UserList.Add(new Users
                {
                    UserID = Int32.Parse(userReader["UserID"].ToString()),
                    FirstName = userReader["FirstName"].ToString(),
                    LastName = userReader["LastName"].ToString(),
                    Email = userReader["Email"].ToString(),
                    ProfessionalEmail = userReader["ProfessionalEmail"].ToString(),
                    Phone = userReader["Phone"].ToString(),
                    Position = userReader["Position"].ToString()
                });
            }

            userReader.Close();
            DBClass.CloseGlobalConnection();
        }
    }
}
