using Lab1.Pages.DataClasses;
using Lab1.Pages.DB_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab1.Pages.ProjectsPages
{
    public class MyProjectsModel : PageModel { 
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
        SqlDataReader projectReader = DBClass.MyProjectsTableReader(HttpContext.Session.GetString("username"));
        //Loop through the rows of the product reader
        //for each record in product reader
        //create a new instance object of Product and fill its properties with the columns from that DB row.
        while (projectReader.Read())
        {
            ProjectList.Add(new Projects
            {
                ProjectID = Int32.Parse(projectReader["ProjectID"].ToString()),
                ProjectName = projectReader["ProjectName"].ToString(),
                ProjectDescription = projectReader["ProjectDescription"].ToString(),
                ProjectType = projectReader["ProjectType"].ToString(),
                UserID = Int32.Parse(projectReader["UserID"].ToString()),
            });
        }

        projectReader.Close();
        return Page();
    }
}
}
