using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using EQueueVidly.Models;
using EQueueVidly.ViewModels;
using Microsoft.AspNet.Identity;

namespace EQueueVidly.Controllers
{
    public class CalendarController : Controller
    {

        private ApplicationDbContext _context;

        public CalendarController()
        {
            _context = new ApplicationDbContext();
        }
        //
        // GET: /Calendar/
        public ActionResult Index()
        {
            var DiaryResourceView = new Resource();
            DiaryResourceView.DefaultView = "teachers";
            DiaryResourceView.ResourceTitle = "All Teachers";
            return View(DiaryResourceView);
        }

        public IEnumerable<ApplicationUser> GetApplicationUsersInRole(string roleName)
        {
            return from role in _context.Roles
                   where role.Name == roleName
                   from userRoles in role.Users
                   join user in _context.Users
                   on userRoles.UserId equals user.Id
                   select user;
        }

      /*  
*/
      /*  public JsonResult GetTeacherEvents(string start, string end, string Id)
        {
            DateTime Start = DateTime.Parse(start);
            DateTime End = DateTime.Parse(end);
            List<ScheduleEvent> EventItems = new List<ScheduleEvent>();
            IEnumerable<ScheduleEventVM> selectedEvents = null;
            selectedEvents = _context.Appointments.Where(s => (s.StartDate >= Start & s.EndDate <= End & s.User_Id == Id))
                .Include(s=>s.Attendees).Select(Mapper.Map<Appointment, ScheduleEventVM>);
            if (selectedEvents != null)
            {

                foreach (var scheduleEvent in selectedEvents)
                {
                    var temp = DateTime.Compare(scheduleEvent.StartDate, DateTime.Now);
                    ScheduleEvent itm = new ScheduleEvent();
                    itm.id = scheduleEvent.Id;
                    if (scheduleEvent.Title.Trim() == "")
                        itm.title = "Pusto";
                    else itm.title = scheduleEvent.Title;
                    itm.start = scheduleEvent.StartDate.ToString("s");
                    itm.end = scheduleEvent.EndDate.ToString("s");
                    itm.duration = scheduleEvent.Durration;
                    itm.timeLimit = scheduleEvent.TimeLimit;
                    itm.TeacherId = scheduleEvent.TeacherId;
                    itm.TeacherName = scheduleEvent.TeacherName;
                    itm.capacity = scheduleEvent.Capacity;
                    itm.queueIiems = scheduleEvent.QueueItemsCount;
                    itm.resourceId = scheduleEvent.TeacherId;
                    itm.color = temp <= 0 ? "#D32F2F" : "#7B1FA2";
                    EventItems.Add(itm);
                }

            }
            return Json(EventItems, JsonRequestBehavior.AllowGet);
        }*/

        /*[System.Web.Http.Route("Calendar/push")]*/
        [System.Web.Http.HttpPost]
        public ActionResult PushAppointment(ScheduleEvent appointment)
        {
            var schEvent = new Appointment();
            schEvent.StartDate = DateTime.Parse(appointment.start);
            schEvent.EndDate = DateTime.Parse(appointment.end);
            schEvent.Durration = appointment.duration;
            schEvent.TimeLimit = appointment.timeLimit;
            schEvent.Title = appointment.title;
            _context.Appointments.Add(schEvent);
            _context.SaveChanges();
            return Json(new {success = true}, JsonRequestBehavior.AllowGet);
        }

        public bool DeleteEvent(int AppointmentId)
        {
            try
            {
                var appointment = _context.Appointments.FirstOrDefault(a => a.Id == AppointmentId);
                _context.Appointments.Remove(appointment);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                return false;
            }
            return true;

        }



        /*------------------------------------------------------------------------------*/

       /* [HttpPost]
        public bool SaveEvent(ScheduleEvent appointment)
        {

            var schEvent = new Appointment();
            schEvent.StartDate = DateTime.Parse(appointment.start);
            schEvent.EndDate = DateTime.Parse(appointment.end);
            schEvent.Durration = appointment.duration;
            schEvent.TimeLimit = appointment.timeLimit;
            schEvent.Title = appointment.title;
            schEvent.User_Id = User.Identity.GetUserId();
            schEvent.Capacity = (int)appointment.duration / appointment.timeLimit;
            schEvent.CreationDate = DateTime.Today;
            try
            {
                _context.Appointments.Add(schEvent);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        [HttpPost]
        public bool EditEvent(ScheduleEvent appointment)
        {
            var app_id = Int32.Parse(appointment.id);
            var schEvent = _context.Appointments.FirstOrDefault(a => a.Id == app_id);
            if (schEvent == null)
            {
                return false;
            }
            schEvent.StartDate = DateTime.Parse(appointment.start);
            schEvent.EndDate = DateTime.Parse(appointment.end);
            schEvent.Durration = appointment.duration;
            schEvent.TimeLimit = appointment.timeLimit;
            schEvent.Title = appointment.title;
            schEvent.User_Id = User.Identity.GetUserId();
            try
            {
                _context.Entry(schEvent).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        /*[Authorize(Roles = "Student")]#1#
        [HttpGet]
        public ActionResult JoinEvent(int AppointmentId, String user)
        {
            var appointment = _context.Appointments.Include(a=>a.User).FirstOrDefault(a=>a.Id == AppointmentId);
            
            if (appointment != null)
            {
                int minutes = appointment.TimeLimit*(appointment.Attendees.Count);
                int span = appointment.TimeLimit*(appointment.Attendees.Count + 1);
                Attendee queueItem = new Attendee
                {
                    /*UserId = userUser.Identity.GetUserId()#1#
                    JoinDate = DateTime.Now,
                    AppointmentId = AppointmentId,
                    Start = appointment.StartDate.AddMinutes(minutes),
                    End = appointment.StartDate.AddMinutes(span)
                };
            
            
                try
                {
                    _context.Attendees.Add(queueItem);
                    _context.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    return Json(new { message = "error" });
                }
                
                return Json(new{message = "success", responseObj = new
                {
                    teacher = appointment.User.FirstName + " " + appointment.User.LastName,
                    start = queueItem.Start,
                    end = queueItem.End
                }}, JsonRequestBehavior.AllowGet);
            }

            return Json(new {message = "error"});
        }
*/
        public String GetMostInnerExeptionMsg(Exception ex)
        {
            if (ex.InnerException == null)
            {
                return ex.Message;
            }

            return GetMostInnerExeptionMsg(ex.InnerException);

        }

        

        
    }
}