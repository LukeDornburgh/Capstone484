using Microsoft.AspNetCore.Mvc.RazorPages;
using Lab1.Pages.DB_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Lab1.Pages.DataClasses;

namespace Lab1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public int badgeNum { get; set; }

        public int messageNum { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                //if they user is already logged in, everytime they return to the home page this will fire off and 
                //update the notifications badge
                int temp = DBClass.GetUserIDSession(HttpContext.Session.GetString("username"));
                int badgeNum = DBClass.NotificationNumber(temp);
                HttpContext.Session.SetInt32("badgeNum", badgeNum);
                int messageNum = DBClass.MessagesNumber(temp);
                HttpContext.Session.SetInt32("messageNum", messageNum);
            }
            else
            {
            }

        }
    }
}