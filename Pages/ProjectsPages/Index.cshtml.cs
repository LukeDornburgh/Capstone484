using Lab1.Pages.DataClasses;
using Lab1.Pages.DB_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab1.Pages.ProjectsPages
{
    public class IndexModel : PageModel
    {
        public List<Projects> ProjectList { get; set; }

        [BindProperty]
        public string searchText { get; set; }
        public IndexModel()
        {
            ProjectList = new List<Projects>();
        }
        public IActionResult OnGet()
        {
            int temp = DBClass.GetUserIDSession(HttpContext.Session.GetString("username"));
            int badgeNum = DBClass.NotificationNumber(temp);
            HttpContext.Session.SetInt32("badgeNum", badgeNum);

            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/BasicLogin");
            }
            SqlDataReader projectReader = DBClass.ProjectsTableReader();
            //Loop through the rows of the product reader
            //for each record in product reader
            //create a new instance object of Product and fill its properties with the columns from that DB row.
            while (projectReader.Read())
            {
                ProjectList.Add(new Projects
                {
                    ProjectID = Int32.Parse(projectReader["ProjectID"].ToString()),
                    ProjectName = projectReader["ProjectName"].ToString(),
                    ProjectBeginDate = Convert.ToDateTime(projectReader["ProjectBeginDate"].ToString()),
                    ProjectMission = projectReader["ProjectMission"].ToString(),
                    ProjectDescription = projectReader["ProjectDescription"].ToString(),
                    ProjectType = projectReader["ProjectType"].ToString(),
                    UserID = Int32.Parse(projectReader["UserID"].ToString()),
                }) ;
            }

            projectReader.Close();
            return Page();
        }

        public void OnPost(int ProjectOwnerID, int ProjectID)
        {
            DBClass.InsertRequest(ProjectID, ProjectOwnerID, HttpContext.Session.GetString("username"));

        }

        public IActionResult OnPostSearch()
        {
            return Page();
        }


    }
}

