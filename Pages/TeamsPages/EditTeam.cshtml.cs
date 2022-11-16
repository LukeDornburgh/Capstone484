using Lab1.Pages.DataClasses;
using Lab1.Pages.DB_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab1.Pages.TeamsPages
{
    public class EditTeamModel : PageModel
    {
        [BindProperty]
        public Teams TeamToUpdate { get; set; }

        public EditTeamModel()
        {
            TeamToUpdate = new Teams();
        }
        public void OnGet(int TeamID)
        {
            SqlDataReader singleTeam = DBClass.SingleTeamReader(TeamID);

            while (singleTeam.Read())
            {
                TeamToUpdate.TeamID = TeamID;
                TeamToUpdate.TeamName = singleTeam["TeamName"].ToString();
                TeamToUpdate.TeamEmailAddress = singleTeam["TeamEmailAddress"].ToString();
                TeamToUpdate.TeamDescription = singleTeam["TeamDescription"].ToString();
                TeamToUpdate.ProjectID = Int32.Parse(singleTeam["ProjectID"].ToString());
            }

            singleTeam.Close();
            DBClass.CloseGlobalConnection();
        }

        public IActionResult OnPost()
        {
            DBClass.UpdateTeam(TeamToUpdate);

            return RedirectToPage("Index");
        }
    }
}


