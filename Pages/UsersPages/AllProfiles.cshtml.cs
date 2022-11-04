using Lab1.Pages.DataClasses;
using Lab1.Pages.DB_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab1.Pages.UsersPages
{
    public class AllProfilesModel : PageModel
    {
        public List<Users> UserList { get; set; }

        public SqlDataReader returnReader { get; set; }

        [BindProperty]
        public List<int> SelectedSkills { get; set; }

        public List<Skills> skillsAlreadySelected { get; set; }

        public List<Skills> SkillsDisplay { get; set; }
        public AllProfilesModel()
        {
            UserList = new List<Users>();
            SkillsDisplay = new List<Skills>();
            skillsAlreadySelected = new List<Skills>();
        }
        public IActionResult OnGet()
        {
            int temp = DBClass.GetUserIDSession(HttpContext.Session.GetString("username"));
            int badgeNum = DBClass.NotificationNumber(temp);
            HttpContext.Session.SetInt32("badgeNum", badgeNum);

            if (HttpContext.Session.GetString("username") == null)
            {
                HttpContext.Session.SetString("loginAlert", "Please Log In");

                return RedirectToPage("/BasicLogin");

            }


            SqlDataReader userReader = DBClass.UserReader(HttpContext.Session.GetString("username"));
            //Loop through the rows of the product reader
            //for each record in product reader
            //create a new instance object of Product and fill its properties with the columns from that DB row.
            while (userReader.Read())
            {
                UserList.Add(new Users
                {
                    UserID = Int32.Parse(userReader["UserID"].ToString()),
                    FirstName = userReader["FirstName"].ToString(),
                    LastName = userReader["LastName"].ToString(),
                    Email = userReader["Email"].ToString(),
                    ProfessionalEmail = userReader["ProfessionalEmail"].ToString(),
                    Phone = userReader["Phone"].ToString(),
                    Position = userReader["Position"].ToString()
                });
            }

            SqlDataReader varSkillReader = DBClass.SkillsTableReader();
            //Loop through the rows of the product reader
            //for each record in product reader
            //create a new instance object of Product and fill its properties with the columns from that DB row.
            while (varSkillReader.Read())
            {
                SkillsDisplay.Add(new Skills
                {
                    SkillID = Int32.Parse(varSkillReader["SkillID"].ToString()),
                    SkillName = varSkillReader["SkillName"].ToString(),
                });
            }

            varSkillReader.Close();
            userReader.Close();
            return Page();
        }
        public IActionResult OnPost()
        {
            //I need to create a new list equal to the value of the selected skills
            //then I need to pass it into the redirect so it can be grabbed in OnGet()
            //then, in the get we need to create skill objects for each of the selected skills
            //then in the view, we iterate over the list of skill objects and display them as pills and make sure they begin checked

            List<int> justSelected = new List<int>();

            string skillIDList = "";

            foreach (var num in SelectedSkills)
            {
                justSelected.Add(num);
                skillIDList += num + " ";
            }
            returnReader = DBClass.FilterUsersBySkill(skillIDList, HttpContext.Session.GetString("username"));

            if (justSelected != null)
            {
                foreach (var num in justSelected)
                {
                    SqlDataReader preSkillReader = DBClass.GetSpecificSkillObject(num);
                    while (preSkillReader.Read())
                    {
                        skillsAlreadySelected.Add(new Skills
                        {
                            SkillID = Int32.Parse(preSkillReader["SkillID"].ToString()),
                            SkillName = preSkillReader["SkillName"].ToString()
                        });
                    }
                }
            }

            SqlDataReader varSkillReader = DBClass.SkillsTableReader();
            while (varSkillReader.Read())
            {
                SkillsDisplay.Add(new Skills
                {
                    SkillID = Int32.Parse(varSkillReader["SkillID"].ToString()),
                    SkillName = varSkillReader["SkillName"].ToString(),
                });
            }

            varSkillReader.Close();

            return Page();
            //return RedirectToPage(new { alreadySelected = justSelected });
        }
    }

}
