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

        public SqlDataReader returnReader { get; set; }

        //this will be used to precheck boxes and preload tags
        public List<string> justSelected { get; set; }

        [BindProperty]
        public List<string> SelectedCollege { get; set; }

        public List<string> CollegeDepartments { get; set; }
        public IndexModel()
        {
            ProjectList = new List<Projects>();
            CollegeDepartments = new List<string>();
            CollegeDepartments.Add("Arts and Letters");
            CollegeDepartments.Add("Business");
            CollegeDepartments.Add("Education");
            CollegeDepartments.Add("Health and Behavioral Studies");
            CollegeDepartments.Add("Integrated Science and Engineering");
            CollegeDepartments.Add("Science and Mathematics");
            CollegeDepartments.Add("Visual and Performing Arts");
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

        public IActionResult OnPost(int ProjectOwnerID, int ProjectID)
        {
            DBClass.InsertRequest(ProjectID, ProjectOwnerID, HttpContext.Session.GetString("username"));

            return RedirectToPage();

        }

        public IActionResult OnPostCollege()
        {
            //create a list to store the names of the boxes that need to start checked when the page reloads, we will pass this into a 
            //query and then add the results of that query to 
            justSelected = new List <string>();

            //create a string to store each college name seperated by commas
            string collegeList = "";

            foreach(var word in SelectedCollege)
            {
                collegeList += word + ",";
                justSelected.Add(word);
            }
            //pass the string to filter by
            returnReader = DBClass.FilterProjectsByCollege(collegeList, HttpContext.Session.GetString("username"));

            return Page();

        }




    }
}

