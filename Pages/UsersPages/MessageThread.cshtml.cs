using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Lab1.Pages.DataClasses;
using Lab1.Pages.DB_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Numerics;

namespace Lab1.Pages.UsersPages
{
    public class MessageThreadModel : PageModel
    {
        public List<Messages> MessagesToDisplay { get; set; }

        public Users OtherPerson { get; set; }

        [BindProperty]
        public string MessageToSend { get; set; }
        public MessageThreadModel()
        {
            MessagesToDisplay = new List<Messages>();
            OtherPerson = new Users();
        }

        public void OnGet(int UserID)
        {
            /*need to load the messagethread corresponding to the user who's message card was clicked
            meaning show all the messages youve sent to this person (they are the receiver) and all
            messages where you are the receiver*/

            /* 
             * 1. We need both of these sets of messages together in 1 query and sorted by DateTime
             * 2. We need to loop through these messages on the view page to display them but also chnage their display components
             * based on sent or received. We can run an if check on each message, the receiverID = our ID then display to the right,
             * else, display to the left side
             */

            //lets query the DB and create a message object for each row and then add those objects to the model List

            SqlDataReader messageReader = DBClass.GetMessages(UserID, HttpContext.Session.GetString("username"));

            while (messageReader.Read())
            {
                MessagesToDisplay.Add(new Messages
                {
                    MessageID = Int32.Parse(messageReader["MessageID"].ToString()),
                    MessageBody = messageReader["MessageBody"].ToString(),
                    SendTime = Convert.ToDateTime(messageReader["SendTime"].ToString()),
                    SenderID = Int32.Parse(messageReader["SenderID"].ToString()),
                    ReceiverID = Int32.Parse(messageReader["ReceiverID"].ToString())
                });  
                
            }

            messageReader.Close();

            //we will also want to get the user data of myself and the person I am chatting with to display on the page

            SqlDataReader otherUser = DBClass.SingleUserReader(UserID);

            while (otherUser.Read())
            {
                OtherPerson.UserID = Int32.Parse(otherUser["UserID"].ToString());
                OtherPerson.FirstName = otherUser["FirstName"].ToString();
                OtherPerson.LastName = otherUser["LastName"].ToString();
                OtherPerson.Email = otherUser["Email"].ToString();
                OtherPerson.ProfessionalEmail = otherUser["ProfessionalEmail"].ToString();
                OtherPerson.Phone = otherUser["Phone"].ToString();
                OtherPerson.Position = otherUser["Position"].ToString();
            }

            otherUser.Close();
        }

        public IActionResult OnPost()
        {
            //we want to 

            return Page();
        }
    }
}
