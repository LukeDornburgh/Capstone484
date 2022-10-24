using Lab1.Pages.DataClasses;
using Lab1.Pages.DB_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab1.Pages.UsersPages
{
    public class AllProfilesModel : PageModel
    {
        public List<Users> UserList { get; set; }

        [BindProperty]
        public string searchText { get; set; }
        public AllProfilesModel()
        {
            UserList = new List<Users>();
        }
        public IActionResult OnGet()
        {

            

            if (HttpContext.Session.GetString("username") == null)
            {
                HttpContext.Session.SetString("loginAlert", "Please Log In");

                return RedirectToPage("/BasicLogin");

            }


            SqlDataReader userReader = DBClass.UserReader();
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
            return Page();
        }
        public IActionResult OnPostSearch()
        {
            return Page();
        }
    }

}