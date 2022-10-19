using Lab1.Pages.DataClasses;
using Lab1.Pages.DB_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab1.Pages.SkillsPages
{
    public class EditSkillModel : PageModel
    {
        [BindProperty]
        public Skills SkillToUpdate { get; set; }

        public EditSkillModel()
        {
            SkillToUpdate = new Skills();
        }

        public void OnGet(int SkillID)
        {
            SqlDataReader singleSkill = DBClass.SingleSkillReader(SkillID);

            while (singleSkill.Read())
            {
                SkillToUpdate.SkillID = SkillID;
                SkillToUpdate.SkillName = singleSkill["SkillName"].ToString();
            }

            singleSkill.Close();
        }

        public IActionResult OnPost()
        {
            DBClass.UpdateSkill(SkillToUpdate);

            return RedirectToPage("Index");
        }
    }
}
