using Lab1.Pages.DB_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using Lab1.Pages.DataClasses;
using System.Data.SqlClient;

namespace Lab1.Pages.UsersPages
{
    public class CalendarModel : PageModel
    {
        [BindProperty]
        public DateTime FirstDayOfMonth { get; set; }

        [BindProperty]
        public DateTime FirstDayOfNextMonth { get; set; }
        [BindProperty]
        public DateTime FirstDayOfPrevMonth { get; set; }
        public List<Events> Events { get; set; }

        [BindProperty]
        public Events NewEvent { get; set; }
        public void OnGet(DateTime URLDate)
        {
            if (URLDate == DateTime.MinValue)
            {
                URLDate = DateTime.Today;
            }
            FirstDayOfMonth = new DateTime(URLDate.Year, URLDate.Month, 1);
            FirstDayOfNextMonth = new DateTime(FirstDayOfMonth.AddMonths(1).Year, FirstDayOfMonth.AddMonths(1).Month, 1);
            FirstDayOfPrevMonth = new DateTime(FirstDayOfMonth.AddMonths(-1).Year, FirstDayOfMonth.AddMonths(-1).Month, 1);
            Events = new List<Events>();
            int currentUserID = DBClass.GetUserIDSession(HttpContext.Session.GetString("username"));
            SqlDataReader eventReader = DBClass.EventsTableReader(currentUserID);

            while (eventReader.Read())
            {
                Events.Add(new Events
                {
                    EventID = Int32.Parse(eventReader["EventID"].ToString()),
                    EventName = eventReader["EventName"].ToString(),
                    EventDate = DateTime.Parse(eventReader["EventDate"].ToString()),
                    EventTime = eventReader["EventTime"].ToString(),
                    EventDescription = eventReader["EventDescription"].ToString(),
                    EventLink = eventReader["EventLink"].ToString()
                });
            }

            eventReader.Close();
            DBClass.CloseGlobalConnection();
        }

        public IActionResult OnPost()
        {
            int currentUserID = DBClass.GetUserIDSession(HttpContext.Session.GetString("username"));

            DBClass.InsertEvent(NewEvent, currentUserID);

            return RedirectToPage("Calendar");

        }
    }
}

