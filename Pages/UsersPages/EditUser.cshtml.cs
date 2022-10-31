using Lab1.Pages.DataClasses;
using Lab1.Pages.DB_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Security.Cryptography;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Lab1.Pages.UsersPages
{
    public class EditUserModel : PageModel
    {

        [BindProperty]
        public Users UserToUpdate { get; set; }

        [BindProperty]
        public int SkillIDSelected { get; set; }
        [BindProperty]
        public string searchText { get; set; }

        [BindProperty]
        public int TeamIDSelected { get; set; }

        [BindProperty]
        public List<int> SelectedSkills { get; set; }
        public List<Skills> SkillsDisplay { get; set; }
        public List <Teams> TeamsToDisplay { get; set; }
        [BindProperty]
        public IFormFile upload { get; set; }
        private IHostingEnvironment _environment;
        public EditUserModel(IHostingEnvironment environment)
        {
            _environment = environment;
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
            // to do: add an if to handle if profile picture isn't populated
            // to do: handle if the profile picture was previously uploaded so don't overwrite it 
            // to do: put a breakpoint on the next line to determine where on the server the uploads folder needs to be created (set permissions if necessary)
            string profilePictureFilePath = Path.Combine(_environment.ContentRootPath, "uploads", upload.FileName);
            using (FileStream fileStream = new FileStream(profilePictureFilePath, FileMode.Create))

            {
                upload.CopyTo(fileStream);

            }

            // to do: store the profile picture in user to update class and database 

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

        public IActionResult OnPostSearch()
        {
            return Page();
        }


    }
}
