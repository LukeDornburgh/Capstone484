using Lab1.Pages.DB_Class;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Lab1.Pages.DB;

namespace Lab1.Pages
{
    public class BasicLoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public Hashtable userInfoHash;

        public string alert { get; set; }

        public void OnGet(string logout)
        {
            if (logout != null)
            {
                HttpContext.Session.Clear();
                ViewData["ErrorMessage"] = "Logged Out Successfully!";
            }

            if(HttpContext.Session.GetString("loginAlert") != null)
            {
                alert += (HttpContext.Session.GetString("loginAlert"));
            }


        }

        public IActionResult OnPost()
        {

            if (DBClass.StoredProcedureLogin(Username, Password))
            {
                HttpContext.Session.SetString("username", Username);
                ViewData["LoginMessage"] = "Login Successful";
                int temp = DBClass.GetUserIDSession(HttpContext.Session.GetString("username"));
                int badgeNum = DBClass.NotificationNumber(temp);
                HttpContext.Session.SetInt32("badgeNum", badgeNum);

                return RedirectToPage("Index");
            }
            else
            {
                ViewData["LoginMessage"] = "Incorrect Username and/or Password";
                return Page();
            }
        }
    }
}
