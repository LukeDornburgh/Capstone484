using Lab1.Pages.DataClasses;
using Lab1.Pages.DB_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab1.Pages.UsersPages
{
    public class ViewOtherUserModel : PageModel
    {
        public List<Users> DisplayedUser { get; set; }

        public ViewOtherUserModel()
        {
            DisplayedUser = new List<Users>();
        }
        public void OnGet(int UserID)
        {
            SqlDataReader singleUser = DBClass.SingleUserReader(UserID);

            while (singleUser.Read())
            {
                DisplayedUser.Add(new Users
                {
                    UserID = UserID,
                    FirstName = singleUser["FirstName"].ToString(),
                    LastName = singleUser["LastName"].ToString(),
                    Email = singleUser["Email"].ToString(),
                    ProfessionalEmail = singleUser["ProfessionalEmail"].ToString(),
                    Position = singleUser["Position"].ToString()

                });
            }
        }
    }
}

