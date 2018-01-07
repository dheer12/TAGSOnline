using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using TAGS_BARCODE_WEBAPP_V4.Models;

namespace TAGS_BARCODE_WEBAPP_V4.APIControllers
{
    public class ScanController : ApiController
    {

        [Route("Dashboard/api/Station1")]
        [HttpPost]
        public CheckInVM CheckTicketStation1(CheckInVM checkInVM)
        {
            using (var db = new TagsDataModel())
            {
                var model = (from ticket in db.TICKETED_CHECKINS
                             where ticket.TICKET_NUMBER.Equals(checkInVM.TicketNo.ToString())
                             select ticket).FirstOrDefault();

                //checking in now. Ticket exists
                if (model != null && model.STATION_1 == false)
                {
                    model.STATION_1 = true;
                    model.STATION_1_CHECKIN_TIME = DateTime.Now;
                    //TODO find logged in userID
                    var user = (from loggedInUser in db.TAGS_LOGIN
                                where loggedInUser.LAST_NAME.ToLower().Equals(User.Identity.Name)
                                select loggedInUser).FirstOrDefault();
                    model.STATION_1_USER_ID = user.USER_ID;

                    checkInVM.IsCheckedIn = true;
                    checkInVM.TicketNotFound = false;
                    checkInVM.AlreadyCheckedIn = false;
                }
                //ticket exists.. Already checked in
                else if (model != null && model.STATION_1 == true)
                {
                    checkInVM.IsCheckedIn = false;
                    checkInVM.TicketNotFound = false;
                    checkInVM.AlreadyCheckedIn = true;

                }
                //ticket doesn't exist
                else if (model == null)
                {
                    checkInVM.IsCheckedIn = false;
                    checkInVM.TicketNotFound = true;
                    checkInVM.AlreadyCheckedIn = false;
                }
                db.SaveChanges();
                return checkInVM;
            }
        }

        [Route("Dashboard/api/AddUser")]
        [HttpPost]
        public void CreateUser(AddUserVM addUserVM)
        {
            using (var db = new TagsDataModel())
            {
                TAGS_LOGIN newUser = new TAGS_LOGIN();
                newUser.FIRST_NAME = addUserVM.FIRST_NAME;
                newUser.LAST_NAME = addUserVM.LAST_NAME;
                newUser.PASSWORD = addUserVM.PASSWORD;
                newUser.USER_ROLE = addUserVM.USER_ROLE;
                //don't know what this is for. Is it is logged in? 
                newUser.IS_LOGGED_ID = 0;
                db.TAGS_LOGIN.Add(newUser);
                db.SaveChanges();
            }
        }

        [Route("Dashboard/api/verifyUserName")]
        [HttpGet]
        public bool verifyUserName()
        {
            using (var db = new TagsDataModel())
            {
                //var userNameExists = from user in db.TAGS_USER
                //                     where user.
            }
            return false;
        }
    }
}
