using Lab1.Pages.DataClasses;
using Lab1.Pages.DB_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab1.Pages.TeamsMeetingsPages
{
    public class AddTeamMeetingModel : PageModel
    {
        [BindProperty]
        public TeamMeetings NewTeamMeeting { get; set; }

        [BindProperty]
        public List<Teams> TeamSelector { get; set; }

        public AddTeamMeetingModel()
        {
            TeamSelector = new List<Teams>();
        }
        public void OnGet()
        {
            SqlDataReader varTeamFKReader = DBClass.MyTeamsTableReader(HttpContext.Session.GetString("username"));
            //Loop through the rows of the product reader
            //for each record in product reader
            //create a new instance object of Product and fill its properties with the columns from that DB row.
            while (varTeamFKReader.Read())
            {
                TeamSelector.Add(new Teams
                {
                    TeamID = Int32.Parse(varTeamFKReader["TeamID"].ToString()),
                    TeamName = varTeamFKReader["TeamName"].ToString(),
                    ProjectID = Int32.Parse(varTeamFKReader["ProjectID"].ToString()),
                });
            }

            varTeamFKReader.Close();
            DBClass.CloseGlobalConnection();
        }
        public IActionResult OnPost()
        {
            NewTeamMeeting.Time = NewTeamMeeting.Time.ToString();
            var fullDate = NewTeamMeeting.Date;
            NewTeamMeeting.Date = fullDate.Date;
            DBClass.InsertTeamMeeting(NewTeamMeeting);

            return RedirectToPage("Index");
        }

        public IActionResult OnPostPopulateHandler()
        {
            if (!ModelState.IsValid)
            {

                SqlDataReader varTeamFKReader = DBClass.MyTeamsTableReader(HttpContext.Session.GetString("username"));
                //Loop through the rows of the product reader
                //for each record in product reader
                //create a new instance object of Product and fill its properties with the columns from that DB row.
                while (varTeamFKReader.Read())
                {
                    TeamSelector.Add(new Teams
                    {
                        TeamID = Int32.Parse(varTeamFKReader["TeamID"].ToString()),
                        TeamName = varTeamFKReader["TeamName"].ToString(),
                        ProjectID = Int32.Parse(varTeamFKReader["ProjectID"].ToString()),
                    });
                }

                varTeamFKReader.Close();
                DBClass.CloseGlobalConnection();

                ModelState.Clear();
                NewTeamMeeting.Date = DateTime.Now;
                NewTeamMeeting.Time = "3:35";
                NewTeamMeeting.Notes = "meeting kickoff";
                NewTeamMeeting.Location = "office 1010";
                NewTeamMeeting.TeamID = 4;

            }
            return Page();
        }
    }
}
