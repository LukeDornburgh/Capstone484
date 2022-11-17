using Lab1.Pages.DataClasses;
using Lab1.Pages.DB_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Reflection;

namespace Lab1.Pages.ProjectsPages
{
    public class AddProjectModel : PageModel
    {
        [BindProperty]
        public Projects NewProject { get; set; }


        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            int myID = DBClass.GetUserIDSession(HttpContext.Session.GetString("username"));

            DBClass.InsertProject(NewProject, myID);

            return RedirectToPage("Index");
        }

        public IActionResult OnPostPopulateHandler()
        {
            if (!ModelState.IsValid)
            {


                ModelState.Clear();
                NewProject.ProjectName = "Project 25";
                NewProject.ProjectDescription = "New project!";
                NewProject.ProjectBeginDate = DateTime.Now;
                NewProject.ProjectMission = "this is our mission for lab 2";
                NewProject.ProjectType = "Advanced";
                NewProject.UserID = 1;
            }       
            return Page();
        }
    }
}
