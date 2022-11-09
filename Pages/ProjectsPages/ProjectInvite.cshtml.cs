using Lab1.Pages.DB_Class;
using Lab1.Pages.DataClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Identity;

namespace Lab1.Pages.ProjectsPages
{
    public class ProjectInviteModel : PageModel
    {
        [BindProperty]
        public int selectedProject { get; set; }

        public string inviteText { get; set; }

        public SqlDataReader ExistingInvites { get; set; }

        public List<Projects> projectDropDown = new List<Projects>();
        public void OnGet(int UserID)
        {
            HttpContext.Session.SetInt32("userToBeInvited", UserID);

            SqlDataReader myProjects = DBClass.InvitesDropdownReader(HttpContext.Session.GetString("username"), UserID);

            while (myProjects.Read())
            {
                projectDropDown.Add(new Projects
                {
                    ProjectID = Int32.Parse(myProjects["ProjectID"].ToString()),
                    ProjectName = myProjects["ProjectName"].ToString(),
                    ProjectDescription = myProjects["ProjectDescription"].ToString(),
                    ProjectType = myProjects["ProjectType"].ToString(),
                    UserID = Int32.Parse(myProjects["UserID"].ToString()),
                });
            }
            myProjects.Close();

            ExistingInvites = DBClass.ExistingInvites(HttpContext.Session.GetString("username"), UserID);
            
        }
        

        public IActionResult OnPost()
        {
            //call a db class method to insert an invite into the invite bridge table
            inviteText = DBClass.InsertInvite((int)HttpContext.Session.GetInt32("userToBeInvited"), selectedProject, HttpContext.Session.GetString("username"));
            ExistingInvites = DBClass.ExistingInvites(HttpContext.Session.GetString("username"), (int)HttpContext.Session.GetInt32("userToBeInvited"));
            return Page();
        }
    }
}
