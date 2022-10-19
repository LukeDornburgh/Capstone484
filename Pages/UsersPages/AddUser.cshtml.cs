using Lab1.Pages.DB_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Lab1.Pages.DataClasses;

namespace Lab1.Pages.UsersPages
{
    public class AddUserModel : PageModel
    {
        [BindProperty]
        public Users NewUser { get; set; } 
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            DBClass.InsertUser(NewUser);

            return RedirectToPage("Index");
        }

        public IActionResult OnPostPopulateHandler()
        {
            if (!ModelState.IsValid)
            {
                ModelState.Clear();
                NewUser.FirstName = "Aaron";
                NewUser.LastName = "Rodgers";
                NewUser.Email = "aaronr@gmail.com";
                NewUser.ProfessionalEmail = null;
                NewUser.Phone = "777-777-7777";
                NewUser.Position = "Faculty";
            }
            return Page();
        }
    }
}

