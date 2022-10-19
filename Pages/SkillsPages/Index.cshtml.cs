using Lab1.Pages.DataClasses;
using Lab1.Pages.DB_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab1.Pages.SkillsPages
{
    public class IndexModel : PageModel
    {
        public List<Skills> SkillList { get; set; }
        public IndexModel()
        {
            SkillList = new List<Skills>();
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/BasicLogin");
            }
            SqlDataReader skillReader = DBClass.SkillsTableReader();
            //Loop through the rows of the product reader
            //for each record in product reader
            //create a new instance object of Product and fill its properties with the columns from that DB row.
            while (skillReader.Read())
            {
                SkillList.Add(new Skills
                {
                    SkillID = Int32.Parse(skillReader["SkillID"].ToString()),
                    SkillName = skillReader["SkillName"].ToString(),
                });
            }

            skillReader.Close();
            return Page();
        }
    }
}
