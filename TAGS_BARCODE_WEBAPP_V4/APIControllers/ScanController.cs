using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using TAGS_BARCODE_WEBAPP_V4.Models;
using TAGS_BARCODE_WEBAPP_V4.ViewModels;

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

        [Route("Dashboard/api/SearchMember")]
        [HttpPost]
        public MemberVM SearchMember(SearchMemberVM SearchMember)
        {
            using (var db = new TagsDataModel())
            {
                MemberVM memb = new MemberVM();
                var model = (from member in db.TAGS_MEMBER
                             where member.EMAIL_ID.ToLower().Equals(SearchMember.email.ToLower())
                             select member).FirstOrDefault();

                if (model != null)
                {
                    memb.CELL_PHONE = model.CELL_PHONE;
                    memb.COMPANY_NAME = model.COMPANY_NAME;
                    memb.EMAIL_ID = model.EMAIL_ID;
                    memb.FIRST_NAME = model.FIRST_NAME;
                    memb.HOME_PHONE = model.HOME_PHONE;
                    memb.IS_VOLUNTEER = model.IS_VOLUNTEER;
                    memb.LAST_NAME = model.LAST_NAME;
                    memb.MemberNotFound = false;
                    memb.MEMBER_ID = model.MEMBER_ID;
                }
                else
                {
                    memb.MemberNotFound = true;
                }

                return memb;
            }
        }


        [Route("Dashboard/api/UpdateMember")]
        [HttpPost]
        public void UpdateMember(MemberVM UpdateMember)
        {
            using (var db = new TagsDataModel())
            {
                var model = (from member in db.TAGS_MEMBER
                             where member.EMAIL_ID.ToLower().Equals(UpdateMember.EMAIL_ID.ToLower())
                             select member).FirstOrDefault();

                if (model != null)
                {
                    model.CELL_PHONE = UpdateMember.CELL_PHONE;
                    model.COMPANY_NAME = UpdateMember.COMPANY_NAME;
                    model.EMAIL_ID = UpdateMember.EMAIL_ID;
                    model.FIRST_NAME = UpdateMember.FIRST_NAME;
                    model.HOME_PHONE = UpdateMember.HOME_PHONE;
                    model.IS_VOLUNTEER = UpdateMember.IS_VOLUNTEER;
                    model.LAST_NAME = UpdateMember.LAST_NAME;
                    db.SaveChanges();
                }
            }
        }

        [Route("Dashboard/api/AddMember")]
        [HttpPost]
        public MemberVM AddMember(MemberVM AddMember)
        {
            using (var db = new TagsDataModel())
            {
                TAGS_MEMBER NewMember = new TAGS_MEMBER();
                NewMember.CELL_PHONE = AddMember.CELL_PHONE;
                NewMember.COMPANY_NAME = AddMember.COMPANY_NAME;
                NewMember.EMAIL_ID = AddMember.EMAIL_ID;
                NewMember.FIRST_NAME = AddMember.FIRST_NAME;
                NewMember.HOME_PHONE = AddMember.HOME_PHONE;
                NewMember.IS_VOLUNTEER = AddMember.IS_VOLUNTEER;
                NewMember.LAST_NAME = AddMember.LAST_NAME;
                db.TAGS_MEMBER.Add(NewMember);
                db.SaveChanges();

                MemberVM member = new MemberVM();
                member.CELL_PHONE = NewMember.CELL_PHONE;
                member.COMPANY_NAME = NewMember.COMPANY_NAME;
                member.EMAIL_ID = NewMember.EMAIL_ID;
                member.FIRST_NAME = NewMember.FIRST_NAME;
                member.HOME_PHONE = NewMember.HOME_PHONE;
                member.IS_VOLUNTEER = NewMember.IS_VOLUNTEER;
                member.LAST_NAME = NewMember.LAST_NAME;
                member.MemberNotFound = false;
                member.MEMBER_ID = NewMember.MEMBER_ID;

                return member;
            }
        }

        [Route("Dashboard/api/SearchEventMember")]
        [HttpPost]
        public MemberCheckInVM SearchEventCheckIn(SearchMemberVM SearchMember)
        {
            MemberCheckInVM MemberCheckInVM = new MemberCheckInVM();
            using (var db = new TagsDataModel())
            {
                var currentEventId = Convert.ToInt32(ConfigurationManager.AppSettings["CurrentEventID"]);
                var model = (from memberEvent in db.MEMBER_EVENT_CHECKINS
                             join member in db.TAGS_MEMBER on memberEvent.MEMBER_ID equals member.MEMBER_ID
                             where member.EMAIL_ID.ToLower().Equals(SearchMember.email.ToLower()) && memberEvent.EVENT_ID == currentEventId
                             select memberEvent).FirstOrDefault();

                if (model != null)
                {
                    MemberCheckInVM.eventCheckInVM = new EventCheckInVM();
                    MemberCheckInVM.eventCheckInVM.EVENT_CHECKIN_ID = model.EVENT_CHECKIN_ID;
                    MemberCheckInVM.eventCheckInVM.EVENT_ID = model.EVENT_ID;
                    MemberCheckInVM.eventCheckInVM.IS_CHECKEDIN = model.IS_CHECKEDIN;
                    MemberCheckInVM.eventCheckInVM.IS_PAID = model.IS_PAID;
                    MemberCheckInVM.eventCheckInVM.MEMBER_ID = model.MEMBER_ID;
                    MemberCheckInVM.eventCheckInVM.UESR_ID = model.UESR_ID;

                    MemberCheckInVM.IsRegistered = true;
                    MemberCheckInVM.IsExistingMember = true;
                }
                else
                {
                    var TagsMember = (from member in db.TAGS_MEMBER
                                      where member.EMAIL_ID.ToLower().Equals(SearchMember.email.ToLower())
                                      select member).FirstOrDefault();

                    if (TagsMember != null)
                    {
                        MemberCheckInVM.eventCheckInVM = new EventCheckInVM();
                        MemberCheckInVM.eventCheckInVM.MEMBER_ID = TagsMember.MEMBER_ID;
                        MemberCheckInVM.eventCheckInVM.EVENT_ID = currentEventId;

                        MemberCheckInVM.IsRegistered = false;
                        MemberCheckInVM.IsExistingMember = true;

                    }
                    else
                    {
                        MemberCheckInVM.eventCheckInVM = new EventCheckInVM();
                        MemberCheckInVM.eventCheckInVM.EVENT_ID = currentEventId;
                        MemberCheckInVM.newMember = new MemberVM();

                        MemberCheckInVM.IsRegistered = false;
                        MemberCheckInVM.IsExistingMember = false;
                    }
                }
            }

            return MemberCheckInVM;
        }

        [Route("Dashboard/api/UpdateEventMember")]
        [HttpPost]
        public MemberCheckInVM UpdateEventCheckIn(MemberCheckInVM memberCheckInVM)
        {
            var currentEventId = Convert.ToInt32(ConfigurationManager.AppSettings["CurrentEventID"]);
            using (var db = new TagsDataModel())
            {
                var user = (from loggedInUser in db.TAGS_LOGIN
                            where loggedInUser.LAST_NAME.ToLower().Equals(User.Identity.Name)
                            select loggedInUser).FirstOrDefault();
                if (memberCheckInVM.IsExistingMember && memberCheckInVM.IsRegistered)
                {
                    var model = (from eve in db.MEMBER_EVENT_CHECKINS
                                 where eve.EVENT_CHECKIN_ID == memberCheckInVM.eventCheckInVM.EVENT_CHECKIN_ID
                                 select eve).FirstOrDefault();

                    model.IS_PAID = memberCheckInVM.eventCheckInVM.IS_PAID;
                    model.IS_CHECKEDIN = memberCheckInVM.eventCheckInVM.IS_CHECKEDIN;
                    model.UESR_ID = user.USER_ID;
                    db.SaveChanges();

                    memberCheckInVM.eventCheckInVM.isUpdated = true;
                }
                else if (memberCheckInVM.IsExistingMember && !memberCheckInVM.IsRegistered)
                {
                    MEMBER_EVENT_CHECKINS membCheckIn = new MEMBER_EVENT_CHECKINS();
                    membCheckIn.EVENT_ID = currentEventId;
                    membCheckIn.MEMBER_ID = Convert.ToInt32(memberCheckInVM.eventCheckInVM.MEMBER_ID);
                    db.MEMBER_EVENT_CHECKINS.Add(membCheckIn);
                    db.SaveChanges();

                    memberCheckInVM.eventCheckInVM.EVENT_CHECKIN_ID = membCheckIn.EVENT_CHECKIN_ID;
                    memberCheckInVM.eventCheckInVM.MEMBER_ID = membCheckIn.MEMBER_ID;
                    memberCheckInVM.eventCheckInVM.EVENT_ID = membCheckIn.EVENT_ID;
                    memberCheckInVM.IsRegistered = true;
                }
                else if (!memberCheckInVM.IsExistingMember && !memberCheckInVM.IsRegistered)
                {
                    MemberVM addedMember = AddMember(memberCheckInVM.newMember);

                    memberCheckInVM.eventCheckInVM.MEMBER_ID = addedMember.MEMBER_ID;
                    memberCheckInVM.IsExistingMember = true;
                }
            }

            return memberCheckInVM;
        }

        [Route("Dashboard/api/Reports/{eventId}")]
        [HttpGet]
        public ReportsVM GetEventReports(int eventId)
        {
            ReportsVM reports = new ReportsVM();
            reports.EventReports = new EventReportVM();
            reports.TicketReports = new List<TicketReportVM>();
            using (var db = new TagsDataModel())
            {
                var eventInfo = (from eventInf in db.TAGS_EVENTS
                                 where eventInf.EVENT_ID == eventId
                                 select eventInf).FirstOrDefault();

                reports.EVENT_DATE = eventInfo.EVENT_DATE;
                reports.EVENT_ID = eventInfo.EVENT_ID;
                reports.EVENT_LOCATION = eventInfo.EVENT_LOCATION;
                reports.EVENT_NAME = eventInfo.EVENT_NAME;
                reports.IS_TICKETED_EVENT = eventInfo.IS_TICKETED_EVENT;

                if (eventInfo.IS_TICKETED_EVENT == false)
                {
                    var MemberEventTotalCount = (from tickEve in db.MEMBER_EVENT_CHECKINS
                                                 where tickEve.EVENT_ID == eventId
                                                 select tickEve).Count();

                    var MemberEventCheckInCount = (from tickEve in db.MEMBER_EVENT_CHECKINS
                                                   where tickEve.EVENT_ID == eventId && tickEve.IS_CHECKEDIN == true
                                                   select tickEve).Count();

                    var MemberEventPaidCount = (from tickEve in db.MEMBER_EVENT_CHECKINS
                                                where tickEve.EVENT_ID == eventId && tickEve.IS_PAID == true
                                                select tickEve).Count();

                    reports.EventReports.checkIns = MemberEventCheckInCount;
                    reports.EventReports.isPaids = MemberEventPaidCount;
                    reports.EventReports.registrations = MemberEventTotalCount;
                }
                else if (eventInfo.IS_TICKETED_EVENT == true)
                {
                    var TicketTypes = (from tick in db.TICKETED_CHECKINS
                                       where tick.EVENT_ID == eventId
                                       select new { tickType = tick.TICKET_TYPE }).ToList().Distinct();

                    foreach (var ticketType in TicketTypes)
                    {
                        var count = (from tick in db.TICKETED_CHECKINS
                                     where tick.EVENT_ID == eventId && tick.TICKET_TYPE == ticketType.tickType && tick.STATION_1 == true
                                     select tick).Count();

                        TicketReportVM tickReport = new TicketReportVM();
                        tickReport.checkIns = count;
                        tickReport.TicketType = ticketType.tickType;

                        reports.TicketReports.Add(tickReport);
                    }
                }
            }
            return reports;
        }

        [Route("Dashboard/api/RevertCheckIn")]
        [HttpPost]
        public CheckInVM RevertCheckIn(CheckInVM checkInVM)
        {
            var currentEventId = Convert.ToInt32(ConfigurationManager.AppSettings["CurrentEventID"]);

            using (var db = new TagsDataModel())
            {
                var model = (from ticket in db.TICKETED_CHECKINS
                             where ticket.TICKET_NUMBER.Equals(checkInVM.TicketNo.ToString()) && ticket.EVENT_ID == currentEventId
                             select ticket).FirstOrDefault();

                //checking in now. Ticket exists
                if (model != null && model.STATION_1 == false)
                {
                    checkInVM.IsCheckedIn = true;
                    checkInVM.TicketNotFound = false;
                    checkInVM.AlreadyCheckedIn = false;
                }
                //ticket exists.. Already checked in,reverting now
                else if (model != null && model.STATION_1 == true)
                {
                    model.STATION_1 = false;
                    model.STATION_1_CHECKIN_TIME = null;

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

        [Route("Dashboard/api/IsUserAdmin")]
        [HttpGet]
        public bool IsUserAdmin()
        {
            using (var db = new TagsDataModel())
            {
                var user = (from loggedInUser in db.TAGS_LOGIN
                            where loggedInUser.LAST_NAME.ToLower().Equals(User.Identity.Name)
                            select loggedInUser).FirstOrDefault();

                if (user.USER_ROLE.ToLower().Equals("admin"))
                    return true;
                else
                    return false;
            }
        }

        [Route("Dashboard/api/GetEvents")]
        [HttpGet]
        public List<EventVM> Events()
        {
            List<EventVM> Events = new List<EventVM>();
            using (var db = new TagsDataModel())
            {
                var events = from evnt in db.TAGS_EVENTS
                             select evnt;

                foreach(var evnt in events)
                {
                    EventVM eventVM = new EventVM();
                    eventVM.EVENT_ID = evnt.EVENT_ID;
                    eventVM.EVENT_NAME = evnt.EVENT_NAME + " - " + (evnt.EVENT_DATE).ToString("MMM ddd d");
                    Events.Add(eventVM);
                }
            }
            return Events.OrderByDescending(e => e.EVENT_ID).ToList();
        }


    }
}
