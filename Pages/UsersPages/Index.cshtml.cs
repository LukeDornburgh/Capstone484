using Lab1.Pages.DB_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Lab1.Pages.DataClasses;

namespace Lab1.Pages.UsersPages
{
    public class IndexModel : PageModel
    {
        public List<Users> UserList { get; set; }
        public IndexModel()
        {
            UserList = new List<Users>();
        }
        public IActionResult OnGet()
        {

            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/BasicLogin");
            }


            SqlDataReader userReader = DBClass.TableReader(HttpContext.Session.GetString("username"));
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
                }) ;
            }

            userReader.Close();
            return Page();
        }
    }

}
