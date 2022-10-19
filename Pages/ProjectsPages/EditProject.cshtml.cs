using Lab1.Pages.DataClasses;
using Lab1.Pages.DB_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab1.Pages.ProjectsPages
{
    public class EditProjectModel : PageModel
    {
        [BindProperty]
        public Projects ProjectToUpdate { get; set; }

        public EditProjectModel()
        {
            ProjectToUpdate = new Projects();
        }
        public void OnGet(int ProjectID)
        {
            SqlDataReader singleProject = DBClass.SingleProjectReader(ProjectID);

            while (singleProject.Read())
            {
                ProjectToUpdate.ProjectID = ProjectID;
                ProjectToUpdate.ProjectName = singleProject["ProjectName"].ToString();
                ProjectToUpdate.ProjectDescription = singleProject["ProjectDescription"].ToString();
                ProjectToUpdate.ProjectBeginDate = Convert.ToDateTime(singleProject["ProjectBeginDate"].ToString());
                ProjectToUpdate.ProjectMission = singleProject["ProjectMission"].ToString();
                ProjectToUpdate.ProjectType = singleProject["ProjectType"].ToString();
                ProjectToUpdate.UserID = Int32.Parse(singleProject["UserID"].ToString());
            }

            singleProject.Close();
        }

        public IActionResult OnPost()
        {
            DBClass.UpdateProject(ProjectToUpdate);

            return RedirectToPage("Index");
        }
    }
}

