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

        [BindProperty]
        public List <Users> ProjectOwners { get; set; }

        public AddProjectModel()
        {
            ProjectOwners = new List<Users>();    
        }
        public void OnGet()
        {
            SqlDataReader varOwnerReader = DBClass.UserReader();
            //Loop through the rows of the product reader
            //for each record in product reader
            //create a new instance object of Product and fill its properties with the columns from that DB row.
            while (varOwnerReader.Read())
            {
                ProjectOwners.Add(new Users
                {
                    UserID = Int32.Parse(varOwnerReader["UserID"].ToString()),
                    FirstName = varOwnerReader["FirstName"].ToString(),
                    LastName = varOwnerReader["LastName"].ToString(),
            });
            }
            varOwnerReader.Close();
        }
        public IActionResult OnPost()
        {
            DBClass.InsertProject(NewProject);

            return RedirectToPage("Index");
        }

        public IActionResult OnPostPopulateHandler()
        {
            if (!ModelState.IsValid)
            {
                SqlDataReader varOwnerReader = DBClass.UserReader();
                //Loop through the rows of the product reader
                //for each record in product reader
                //create a new instance object of Product and fill its properties with the columns from that DB row.
                while (varOwnerReader.Read())
                {
                    ProjectOwners.Add(new Users
                    {
                        UserID = Int32.Parse(varOwnerReader["UserID"].ToString()),
                        FirstName = varOwnerReader["FirstName"].ToString(),
                        LastName = varOwnerReader["LastName"].ToString(),
                    });
                }

                varOwnerReader.Close();

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
