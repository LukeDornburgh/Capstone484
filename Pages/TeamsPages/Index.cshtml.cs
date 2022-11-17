using Lab1.Pages.DataClasses;
using Lab1.Pages.DB_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab1.Pages.TeamsPages
{
    public class IndexModel : PageModel
    {
        public List<Teams> TeamList { get; set; }
        public IndexModel()
        {
            TeamList = new List<Teams>();
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/BasicLogin");
            }

            SqlDataReader teamReader = DBClass.TeamsTableReader();
            //Loop through the rows of the product reader
            //for each record in product reader
            //create a new instance object of Product and fill its properties with the columns from that DB row.
            while (teamReader.Read())
            {
                TeamList.Add(new Teams
                {
                    TeamID = Int32.Parse(teamReader["TeamID"].ToString()),
                    TeamName = teamReader["TeamName"].ToString(),
                    TeamEmailAddress = teamReader["TeamEmailAddress"].ToString(),
                    TeamDescription = teamReader["TeamDescription"].ToString(),
                    ProjectID = Int32.Parse(teamReader["ProjectID"].ToString()),
                });
            }

            teamReader.Close();
            DBClass.CloseGlobalConnection();
            return Page();
        }
    }
}
