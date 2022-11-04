using Lab1.Pages.DB_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1.Pages.ProjectsPages
{
    public class RequestNotificationsModel : PageModel
    {
        [BindProperty]
        public Boolean Approve { get; set; }

        public void OnGet()
        {


        }

        public IActionResult OnPostDeny(int projectID, int UserID) 
        { 
            DBClass.DenyRequest(projectID, UserID);
            int temp = DBClass.GetUserIDSession(HttpContext.Session.GetString("username"));
            int badgeNum = DBClass.NotificationNumber(temp);
            HttpContext.Session.SetInt32("badgeNum", badgeNum);
            return Page();
        }


        public IActionResult OnPostApprove(int projectID, int UserID)
        {
            DBClass.ApproveRequest(projectID, UserID);
            int temp = DBClass.GetUserIDSession(HttpContext.Session.GetString("username"));
            int badgeNum = DBClass.NotificationNumber(temp);
            HttpContext.Session.SetInt32("badgeNum", badgeNum);
            return Page();
        }
    }
}
