using Lab1.Pages.DataClasses;
using Lab1.Pages.DB_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Lab1.Pages.UsersPages
{
    public class EditUserModel : PageModel
    {

        [BindProperty]
        public Users UserToUpdate { get; set; }

        [BindProperty]
        public int SkillIDSelected { get; set; }

        [BindProperty]
        public int TeamIDSelected { get; set; }

        [BindProperty]
        public List<int> SelectedSkills { get; set; }
        public List<Skills> SkillsDisplay { get; set; }
        public List <Teams> TeamsToDisplay { get; set; }
        public EditUserModel()
        {
            UserToUpdate = new Users();
            SkillsDisplay = new List <Skills>();
            TeamsToDisplay = new List <Teams>();
        }

        public void OnGet(int UserID)
        {
            SqlDataReader singleUser = DBClass.SingleUserReader(UserID);

            while (singleUser.Read())
            {
                UserToUpdate.UserID = UserID;
                UserToUpdate.FirstName = singleUser["FirstName"].ToString();
                UserToUpdate.LastName = singleUser["LastName"].ToString();
                UserToUpdate.Email = singleUser["Email"].ToString();
                UserToUpdate.ProfessionalEmail = singleUser["ProfessionalEmail"].ToString();
                UserToUpdate.Position = singleUser["Position"].ToString();
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

            SqlDataReader teamReader = DBClass.TeamsTableReader();
            //Loop through the rows of the product reader
            //for each record in product reader
            //create a new instance object of Product and fill its properties with the columns from that DB row.
            while (teamReader.Read())
            {
                TeamsToDisplay.Add(new Teams
                {
                    TeamID = Int32.Parse(teamReader["TeamID"].ToString()),
                    TeamName = teamReader["TeamName"].ToString(),
                    TeamEmailAddress = teamReader["TeamEmailAddress"].ToString(),
                    TeamDescription = teamReader["TeamDescription"].ToString(),
                    ProjectID = Int32.Parse(teamReader["ProjectID"].ToString()),
                });

            }

                teamReader.Close();

                varSkillReader.Close();

                singleUser.Close();
         }

        public IActionResult OnPost()
        {


            DBClass.UpdateUser(UserToUpdate);
            if (SelectedSkills != null)
            {
                foreach (var skillID in SelectedSkills)
                {
                    DBClass.PopulateSkillBridge(UserToUpdate, skillID);
                }
            }

            if (TeamIDSelected != 0)
            {
                DBClass.PopulateTeamMembersBridge(UserToUpdate, TeamIDSelected);
            }
            return RedirectToPage(new { UserID = UserToUpdate.UserID });
        }


    }
}
