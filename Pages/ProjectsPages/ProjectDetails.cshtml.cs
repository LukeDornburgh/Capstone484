using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Lab1.Pages.DataClasses;
using Lab1.Pages.DB_Class;
namespace Lab1.Pages.ProjectsPages
{
    public class ProjectDetailsModel : PageModel
    {

        public int projID { get; set; }

        [BindProperty]
        public Projects ProjectToUpdate { get; set; }

        public ProjectDetailsModel()
        {
            ProjectToUpdate = new Projects();
        }

        public void OnGet(int ProjectID)
        {
            //set the projectID to a model variable so we can use it on the view side and call code

            projID = ProjectID;

            SqlDataReader singleUser = DBClass.SingleProjectReader(ProjectID);

            while (singleUser.Read())
            {
                ProjectToUpdate.ProjectID = ProjectID;
                ProjectToUpdate.ProjectName = singleUser["ProjectName"].ToString();
                ProjectToUpdate.ProjectBeginDate = Convert.ToDateTime(singleUser["ProjectBeginDate"].ToString());
                ProjectToUpdate.ProjectMission = singleUser["ProjectMission"].ToString();
                ProjectToUpdate.ProjectDescription = singleUser["ProjectDescription"].ToString();
                ProjectToUpdate.ProjectType = singleUser["ProjectType"].ToString();
                ProjectToUpdate.ProjectDuration = singleUser["ProjectDuration"].ToString();
                ProjectToUpdate.GeneralTimeAvailability = singleUser["GeneralTimeAvailability"].ToString();
                ProjectToUpdate.college = singleUser["college"].ToString();
                ProjectToUpdate.DesiredSkills = singleUser["DesiredSkills"].ToString();
            }
            singleUser.Close();
            DBClass.CloseGlobalConnection();

        }
        public IActionResult OnPost()
        {
            DBClass.UpdateProject(ProjectToUpdate);

            return RedirectToPage(new {ProjectID = ProjectToUpdate.ProjectID});
        }

        public IActionResult OnPostRequest(int ProjectOwnerID, int ProjectID)
        {
            DBClass.InsertRequest(ProjectID, ProjectOwnerID, HttpContext.Session.GetString("username"));

            return RedirectToPage(new { ProjectID = ProjectID });

        }
    }
}
