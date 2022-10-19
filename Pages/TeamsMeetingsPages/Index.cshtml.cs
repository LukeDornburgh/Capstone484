using Lab1.Pages.DataClasses;
using Lab1.Pages.DB_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab1.Pages.TeamMeetingsPages
{
    public class IndexModel : PageModel
    {
        public List<TeamMeetings> TeamMeetingsList { get; set; }
        public IndexModel()
        {
            TeamMeetingsList = new List<TeamMeetings>();
        }
        public IActionResult OnGet()
        {

            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/BasicLogin");
            }
            SqlDataReader teamMeetingsReader = DBClass.TeamMeetingsTableReader(HttpContext.Session.GetString("username"));
            //Loop through the rows of the product reader
            //for each record in product reader
            //create a new instance object of Product and fill its properties with the columns from that DB row.
            while (teamMeetingsReader.Read())
            {
                TeamMeetingsList.Add(new TeamMeetings
                {
                    TeamMeetingID = Int32.Parse(teamMeetingsReader["TeamMeetingID"].ToString()),
                    Date = Convert.ToDateTime(teamMeetingsReader["Date"].ToString()),
                    Time = teamMeetingsReader["Time"].ToString(),
                    Notes = teamMeetingsReader["Notes"].ToString(),
                    Location = teamMeetingsReader["Location"].ToString(),
                    TeamID = Int32.Parse(teamMeetingsReader["TeamID"].ToString()),
                });
            }

            teamMeetingsReader.Close();
            return Page();
        }
    }
}

