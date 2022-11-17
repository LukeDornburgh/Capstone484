using Lab1.Pages.DataClasses;
using Lab1.Pages.DB_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab1.Pages.TeamMeetingsPages
{
    public class EditTeamMeetingModel : PageModel
    {
        [BindProperty]
        public TeamMeetings TeamMeetingToUpdate { get; set; }

        public EditTeamMeetingModel()
        {
            TeamMeetingToUpdate = new TeamMeetings();
        }
        public void OnGet(int TeamMeetingID)
        {
            SqlDataReader singleTeamMeeting = DBClass.SingleTeamMeetingReader(TeamMeetingID);

            while (singleTeamMeeting.Read())
            {
                TeamMeetingToUpdate.TeamMeetingID = TeamMeetingID;
                TeamMeetingToUpdate.Date = Convert.ToDateTime(singleTeamMeeting["Date"].ToString());
                TeamMeetingToUpdate.Time = singleTeamMeeting["Time"].ToString();
                TeamMeetingToUpdate.Notes = singleTeamMeeting["Notes"].ToString();
                TeamMeetingToUpdate.Location = singleTeamMeeting["Location"].ToString();
                TeamMeetingToUpdate.TeamID = Int32.Parse(singleTeamMeeting["TeamID"].ToString());
            }

            singleTeamMeeting.Close();
            DBClass.CloseGlobalConnection();
        }

        public IActionResult OnPost()
        {
            DBClass.UpdateTeamMeeting(TeamMeetingToUpdate);

            return RedirectToPage("Index");
        }
    }
}

