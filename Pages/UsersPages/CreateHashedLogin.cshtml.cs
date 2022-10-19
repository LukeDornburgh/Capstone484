using Lab1.Pages.DataClasses;
using Lab1.Pages.DB_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1.Pages.UsersPages;

public class CreateHashedLoginModel : PageModel
{
    [BindProperty]
    public string Username { get; set; }
    [BindProperty]
    public string Password { get; set; }

    [BindProperty]
    public Users NewUser { get; set; }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        // Perform Validation First on Form
        // then...
        NewUser.Email = Username;

        DBClass.CreateHashedUser(Username, Password, NewUser);

        // Perform actual logic to check if user was successfully
        //  added in your projects but for demo purposes we can say:

        ViewData["UserCreate"] = "User Successfully Created!";

        return RedirectToPage("/BasicLogin");
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
