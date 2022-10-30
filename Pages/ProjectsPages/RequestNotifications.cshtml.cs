using Lab1.Pages.DB_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1.Pages.ProjectsPages
{
    public class RequestNotificationsModel : PageModel
    {

        public void OnGet()
        {


        }


        public IActionResult OnPostApprove(int projectID, int UserID)
        {

            DBClass.ApproveRequest(projectID, UserID);

            return Page();
        }

        public IActionResult OnPost(int projectID, int UserID)
        {

            DBClass.DenyRequest(projectID, UserID);

            return Page();
        }
    }
}
