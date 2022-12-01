using Lab1.Pages.DataClasses;
using Lab1.Pages.DB_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab1.Pages.ProjectsPages
{
    public class MyProjectsModel : PageModel {

    [BindProperty]
    public Projects NewProject { get; set; }
    public List<Projects> ProjectList { get; set; }
    public MyProjectsModel()
    {
        ProjectList = new List<Projects>();
    }
    public IActionResult OnGet()
    {

        if (HttpContext.Session.GetString("username") == null)
        {
            return RedirectToPage("/BasicLogin");
        }
        return Page();
    }

        public IActionResult OnPostCreate()
        {
            int myID = DBClass.GetUserIDSession(HttpContext.Session.GetString("username"));

            DBClass.InsertProject(NewProject, myID);

            return RedirectToPage("MyProjects");
        }
    }
}
