using Lab1.Pages.DataClasses;
using Lab1.Pages.DB_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab1.Pages.UsersPages
{
    public class ViewOtherUserModel : PageModel
    {

        public Users DisplayedUser { get; set; }
        public void OnGet()
        {
            //SqlDataReader singleUser = DBClass.SingleUserReader(UserID);

            //while (singleUser.Read())
            //{
            //    DisplayedUser.UserID = UserID;
            //    UserToUpdate.FirstName = singleUser["FirstName"].ToString();
            //    UserToUpdate.LastName = singleUser["LastName"].ToString();
            //    UserToUpdate.Email = singleUser["Email"].ToString();
            //    UserToUpdate.ProfessionalEmail = singleUser["ProfessionalEmail"].ToString();
            //    UserToUpdate.Position = singleUser["Position"].ToString();
            }
        }
    }

