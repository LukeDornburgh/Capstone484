using Lab1.Pages.DataClasses;
using Lab1.Pages.DB_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
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
        public List<Teams> TeamsToDisplay { get; set; }

        public IFormFile upload { get; set; }
        private IHostingEnvironment _environment;
        public string ProfilePictureUrl { get; set; }
        public EditUserModel(IHostingEnvironment environment)
        {
            _environment = environment;
            UserToUpdate = new Users();
            SkillsDisplay = new List<Skills>();
            TeamsToDisplay = new List<Teams>();
            ProfilePictureUrl = String.Empty;
        }
        public void OnGet(string email)
        {
            int UserID = DBClass.GetUserIDSession(email);

            SqlDataReader singleUser = DBClass.SingleUserReader(UserID);

            while (singleUser.Read())
            {
                UserToUpdate.UserID = UserID;
                UserToUpdate.FirstName = singleUser["FirstName"].ToString();
                UserToUpdate.LastName = singleUser["LastName"].ToString();
                UserToUpdate.Email = singleUser["Email"].ToString();
                UserToUpdate.ProfessionalEmail = singleUser["ProfessionalEmail"].ToString();
                UserToUpdate.Position = singleUser["Position"].ToString();
                UserToUpdate.Phone = singleUser["Phone"].ToString();
                UserToUpdate.PersonalInterests = singleUser["PersonalInterests"].ToString();
                UserToUpdate.ProfessionalInterests = singleUser["ProfessionalInterests"].ToString();
                UserToUpdate.Bio = singleUser["Bio"].ToString();
                UserToUpdate.College = singleUser["College"].ToString();
                UserToUpdate.GeneralAvailability = singleUser["GeneralAvailability"].ToString();
                if (!singleUser.IsDBNull(singleUser.GetOrdinal("ProfilePicturePath")))
                {
                    ProfilePictureUrl = "/uploads/" + singleUser["ProfilePicturePath"].ToString();
                }
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

            //get the ID of the user that is logged in
            int signedInUserID = DBClass.GetUserIDSession(HttpContext.Session.GetString("username"));

            // TODO: get what the user already has

            string sqlQuery = "SELECT Skills.SkillName, Skills.SkillID from Skills where Skills.SkillID in(Select SkillsAssociation.SkillID from SkillsAssociation where SkillsAssociation.UserID in(SELECT users.UserID FROM Users WHERE users.UserID = " + signedInUserID + "));";

            SqlDataReader QueryResults = DBClass.GeneralReaderQuery(sqlQuery);

            //create the list to remove
            List<int> listToRemove = new List<int>();
            while (QueryResults.Read())
            {
                listToRemove.Add((int)QueryResults["SkillID"]);
            }

            // [8,9,10,11] // listToRemove(pre-existing)
            // [8,9,10,11,12,13] // selectedSkills

            for (int i = 0; i < listToRemove.Count; i++)
            {
                for (int j = 0; j < SelectedSkills.Count; j++)
                {
                    //compare this list to remove to each number in selected
                    if (listToRemove[i] == SelectedSkills[j])
                    {
                        listToRemove.RemoveAt(i);
                        i--;
                        SelectedSkills.RemoveAt(j);
                        break;
                    }


                }
            }
            if (upload != null)
            {
                string profilePictureFilePath = Path.Combine(_environment.ContentRootPath, "wwwroot", "uploads", upload.FileName);
                using (FileStream fileStream = new FileStream(profilePictureFilePath, FileMode.Create))
                {
                    upload.CopyTo(fileStream);
                }
                UserToUpdate.ProfilePicturePath = upload.FileName;
            }

            DBClass.UpdateUser(UserToUpdate);
            if (SelectedSkills != null)
            {
                foreach (var skillID in SelectedSkills)
                {
                    // TODO: is it a new skill they don't have? add
                    DBClass.PopulateSkillBridge(UserToUpdate, skillID);
                }
            }
            if (listToRemove != null)
            {
                foreach (var skillID in listToRemove)
                {
                    DBClass.RemoveSkillFromUser(UserToUpdate, skillID);
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
