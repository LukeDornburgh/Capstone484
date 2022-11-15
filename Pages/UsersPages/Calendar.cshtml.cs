using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

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
        public  List<Event> Events { get; set; }
        public void OnGet(DateTime URLDate)
        {
            if(URLDate == DateTime.MinValue)
            {
                URLDate = DateTime.Today;
            }
            FirstDayOfMonth = new DateTime(URLDate.Year, URLDate.Month, 1);
            FirstDayOfNextMonth = new DateTime(FirstDayOfMonth.AddMonths(1).Year, FirstDayOfMonth.AddMonths(1).Month, 1);
            FirstDayOfPrevMonth = new DateTime(FirstDayOfMonth.AddMonths(-1).Year, FirstDayOfMonth.AddMonths(-1).Month, 1);
            Events = new List<Event>();
            Events.Add(new Event
            {
                EventDate = new DateTime(2022, 11, 8),
                EventName = "Election Day"
            });
            Events.Add(new Event
            {
                EventDate = new DateTime(2022, 11, 24),
                EventName = "Thanksgiving"
            });
            Events.Add(new Event
            {
                EventDate = new DateTime(2022, 11, 25),
                EventName = "Black Friday"
            });
            Events.Add(new Event
            {
                EventDate = new DateTime(2022, 11, 28),
                EventName = "Cyber Monday"
            });
            Events.Add(new Event
            {
                EventDate = new DateTime(2022, 11, 28),
                EventName = "Test Event"
            });

            Events.Add(new Event
            {
                EventDate = new DateTime(2022, 12, 25),
                EventName = "Christmas"
            });
        }
    }
    // this is demo data that will be deleted in the next sprint
    public class Event {
        public DateTime EventDate { get; set; }
        public string EventName { get; set; }
    }
}
