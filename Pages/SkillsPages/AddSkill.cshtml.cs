using Lab1.Pages.DataClasses;
using Lab1.Pages.DB_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1.Pages.SkillsPages
{
    public class AddSkillModel : PageModel
    {
        [BindProperty]
        public Skills NewSkill { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            DBClass.InsertSkill(NewSkill);

            return RedirectToPage("Index");
        }

        public IActionResult OnPostPopulateHandler()
        {
            if (!ModelState.IsValid)
            {
                ModelState.Clear();
                NewSkill.SkillName = "Leadership";

            }
            return Page();
        }
    }
}
