using Lab1.Pages.DataClasses;
using Lab1.Pages.DB_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography;

namespace Lab1.Pages.ProjectsPages
{
    public class ProjectDetailsModel : PageModel
    {
        [BindProperty]
        public Users UserToUpdate { get; set; }

        [BindProperty]
        public int SkillIDSelected { get; set; }

        [BindProperty]

        public List<int> SelectedSkills { get; set; }
        public List<Projects> DisplayedProject { get; set; }

        public List<Skills> SkillsWanted { get; set; }

        public List<Users> TeamMembers { get; set; }



        public ProjectDetailsModel()
        {
            DisplayedProject = new List<Projects>();
            UserToUpdate = new Users();
            SkillsWanted = new List<Skills>();
            TeamMembers = new List<Users>();
        }
        public void OnGet(int ProjectID)
        {
            SqlDataReader projectReader = DBClass.SingleProjectReader(ProjectID);


            while (projectReader.Read())
            {
                DisplayedProject.Add(new Projects
                {
                    ProjectID = ProjectID,
                    ProjectName = projectReader["ProjectName"].ToString(),
                    ProjectBeginDate = Convert.ToDateTime(projectReader["ProjectBeginDate"].ToString()),
                    ProjectDuration = projectReader["ProjectDuration"].ToString(),
                    ProjectDescription = projectReader["ProjectDescription"].ToString(),
                    DesiredSkills = projectReader["DesiredSkills"].ToString(),
                    GeneralTimeAvailability = projectReader["GeneralTimeAvailability"].ToString()
                });


                SqlDataReader varSkillReader = DBClass.SkillsTableReader();

                while (varSkillReader.Read())
                {
                    SkillsWanted.Add(new Skills
                    {
                        SkillID = Int32.Parse(varSkillReader["SkillID"].ToString()),
                        SkillName = varSkillReader["SkillName"].ToString(),
                    });
                }


            }

        }
        public IActionResult OnPost(int ProjectOwnerID, int ProjectID)
        {
            DBClass.InsertRequest(ProjectID, ProjectOwnerID, HttpContext.Session.GetString("username"));

            return RedirectToPage("Index");
        }



        //SqlDataReader varSingleUserReader = DBClass.SingleUserReader();

        //while (varSingleUserReader.Read())
        //{
        // TeamMembers.Add(new Users
        //  {
        //  UserID = Int32.Parse(varSingleUserReader["UserID"].ToString()),
        //   FirstName = varSingleUserReader["FirstName"].ToString(),
        // LastName = varSingleUserReader["LastName"].ToString(),

        // });



    }
}

