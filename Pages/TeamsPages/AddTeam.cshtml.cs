using Lab1.Pages.DataClasses;
using Lab1.Pages.DB_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab1.Pages.TeamsPages
{
    public class AddTeamModel : PageModel
    {
        [BindProperty]
        public Teams NewTeam { get; set; }

        [BindProperty]
        public List <Projects> ProjectSelector { get; set; }

        public AddTeamModel()
        {
            ProjectSelector = new List<Projects>();
        }
        //add rpute so the project is autoselected
        public void OnGet()
        {
            SqlDataReader varProjectReader = DBClass.MyProjectsTableReader(HttpContext.Session.GetString("username"));
            //Loop through the rows of the product reader
            //for each record in product reader
            //create a new instance object of Product and fill its properties with the columns from that DB row.
            while (varProjectReader.Read())
            {
                ProjectSelector.Add(new Projects
                {
                    ProjectID = Int32.Parse(varProjectReader["ProjectID"].ToString()),
                    ProjectName = varProjectReader["ProjectName"].ToString(),
                    ProjectType = varProjectReader["ProjectType"].ToString(),
                    UserID = Int32.Parse(varProjectReader["UserID"].ToString()),
                });
            }
        }
        public IActionResult OnPost()
        {
            DBClass.InsertTeam(NewTeam, HttpContext.Session.GetString("username"));

            return RedirectToPage("Index");
        }

        public IActionResult OnPostPopulateHandler()
        {
            if (!ModelState.IsValid)
            {

                SqlDataReader varProjectReader = DBClass.MyProjectsTableReader(HttpContext.Session.GetString("username"));
                //Loop through the rows of the product reader
                //for each record in product reader
                //create a new instance object of Product and fill its properties with the columns from that DB row.
                while (varProjectReader.Read())
                {
                    ProjectSelector.Add(new Projects
                    {
                        ProjectID = Int32.Parse(varProjectReader["ProjectID"].ToString()),
                        ProjectName = varProjectReader["ProjectName"].ToString(),
                        ProjectType = varProjectReader["ProjectType"].ToString(),
                        UserID = Int32.Parse(varProjectReader["UserID"].ToString()),
                    });
                }

                ModelState.Clear();
                NewTeam.TeamName = "Team 11";
                NewTeam.TeamEmailAddress = "123@yahoo.com";
                NewTeam.TeamDescription = "New team";
                NewTeam.ProjectID = 3;
            }
            return Page();
        }
    }
}
