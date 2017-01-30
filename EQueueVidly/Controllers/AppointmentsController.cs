using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using DHTMLX.Common;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Controls;
using DHTMLX.Scheduler.Data;
using EQueueVidly.Domain;
using EQueueVidly.Dtos;
using EQueueVidly.Models;
using EQueueVidly.ViewModels;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace EQueueVidly.Controllers
{
    [Authorize]
    public class AppointmentsController : BaseController
    {
#region create
        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public JsonResult SaveAppointment(ScheduleEvent appointment)
        {
            var teacher = unitOfWork.Teachers.GetTeacherByAccountId(User.Identity.GetUserId());
            var schEvent = new Appointment();
            schEvent.StartDate = DateTime.Parse(appointment.start);
            schEvent.EndDate = DateTime.Parse(appointment.end);
            schEvent.Durration = appointment.duration;
            schEvent.TimeLimit = appointment.timeLimit;
            schEvent.Title = appointment.title;
            schEvent.TeacherId = teacher.Id;
            schEvent.Capacity = (int)appointment.duration / appointment.timeLimit;
            schEvent.CreationDate = DateTime.Today;
            try
            {
                unitOfWork.Appointments.Add(schEvent);
                unitOfWork.Complete();
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet); ;
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
#endregion
#region read
        public JsonResult GetAppointments(string start, string end)
        {
            var teacher = unitOfWork.Teachers.GetTeacherByAccountId(User.Identity.GetUserId());
            DateTime Start = DateTime.Parse(start);
            DateTime End = DateTime.Parse(end);
            List<ScheduleEvent> EventItems = new List<ScheduleEvent>();
            IEnumerable<Appointment> appointments = null;
            appointments = unitOfWork.Teachers.GetAllAppointments(Start, End, teacher.Id).ToList();
            foreach (var scheduleEvent in appointments)
            {
                var temp = DateTime.Compare(scheduleEvent.StartDate, DateTime.Now);
                ScheduleEvent itm = new ScheduleEvent();
                itm.id = scheduleEvent.Id.ToString();
                itm.title = scheduleEvent.Title;
                itm.start = scheduleEvent.StartDate.ToString("s");
                itm.end = scheduleEvent.EndDate.ToString("s");
                itm.duration = scheduleEvent.Durration;
                itm.timeLimit = scheduleEvent.TimeLimit;
                itm.TeacherId = scheduleEvent.TeacherId;
                itm.TeacherName = scheduleEvent.Teacher.account.FirstName + scheduleEvent.Teacher.account.LastName;
                itm.capacity = scheduleEvent.Capacity;
                itm.queueIiems = scheduleEvent.Attendees.Count;
                itm.color = temp <= 0 ? "#D32F2F" : "#7B1FA2";
                EventItems.Add(itm);
            }
            return Json(EventItems, JsonRequestBehavior.AllowGet);
        }

        //returns events to student
        public JsonResult GetAppointmentsForTeacher(string start, string end, int Id)
        {
            var student = unitOfWork.Students.GetStudentByAccountId(User.Identity.GetUserId());
            var teacher = unitOfWork.Teachers.GetTeacherById(Id);
            IQueryable<Attendee> currentUserAppointments=null;
            if (student != null)
            {
                 currentUserAppointments = unitOfWork.Attendees.GetAttendeesByStudentId(student.Id);
            }
            DateTime Start = DateTime.Parse(start);
            DateTime End = DateTime.Parse(end);
            List<ScheduleEvent> EventItems = new List<ScheduleEvent>();
            IEnumerable<Appointment> appointments = null;
            appointments = unitOfWork.Teachers.GetAllAppointments(Start, End, Id).ToList();
            foreach (var scheduleEvent in appointments)
            {
                var temp = DateTime.Compare(scheduleEvent.StartDate, DateTime.Now);
                ScheduleEvent itm = new ScheduleEvent();
                itm.id = scheduleEvent.Id.ToString();
                itm.title = scheduleEvent.Title;
                itm.start = scheduleEvent.StartDate.ToString("s");
                itm.end = scheduleEvent.EndDate.ToString("s");
                itm.duration = scheduleEvent.Durration;
                itm.timeLimit = scheduleEvent.TimeLimit;
                itm.TeacherId = scheduleEvent.TeacherId;
                itm.TeacherName = teacher.account.FirstName +" "+ teacher.account.LastName;
                itm.capacity = scheduleEvent.Capacity;
                itm.queueIiems = scheduleEvent.Attendees.Count;
                if (currentUserAppointments != null)
                {
                    foreach (var userAppointment in currentUserAppointments)
                    {
                        if (userAppointment.AppointmentId == scheduleEvent.Id)
                        {
                            itm.imageurl = "Content/images/ready.png";
                        }
                    }
                }
                itm.color = temp <= 0 ? "#D32F2F" : "#7B1FA2";
                EventItems.Add(itm);
            }
            return Json(EventItems, JsonRequestBehavior.AllowGet);
        }

        public ViewResult Details(int id)
        {
            var Appointment =  unitOfWork.Appointments.GetAppointmentById(id);

            if (Appointment == null)
            {
                return null;
            }

            var model = new AppointmentDetailsViewModel();
            model.Title = Appointment.Title;
            model.StartDate = Appointment.StartDate;
            model.EndDate = Appointment.EndDate;
            model.Capacity = Appointment.Capacity;
            model.TimeLimit = Appointment.TimeLimit;

            foreach (var attendee in Appointment.Attendees)
            {
                var item = unitOfWork.Attendees.GetAttendeesById(attendee.Id);
                model.Attendees.Add(new AttendeeViewModel
                {
                    FullName = item.Student.Account.FirstName + " " + item.Student.Account.LastName,
                    Start = item.Start,
                    End = item.End
                });
            }
            return View(model);
        }

        #endregion#
#region update
[Authorize(Roles = "Teacher")]
[HttpPost]
public JsonResult EditAppointment(ScheduleEvent appointment)
{
    var teacher = unitOfWork.Teachers.GetTeacherByAccountId(User.Identity.GetUserId());
    var app_id = Int32.Parse(appointment.id);
    var schEvent = unitOfWork.Appointments.Find(a => a.Id == app_id).FirstOrDefault();
    if (schEvent == null)
    {
        return Json(false, JsonRequestBehavior.AllowGet); ;
    }
    schEvent.StartDate = DateTime.Parse(appointment.start);
    schEvent.EndDate = DateTime.Parse(appointment.end);
    schEvent.Durration = appointment.duration;
    schEvent.TimeLimit = appointment.timeLimit;
    schEvent.Title = appointment.title;
    schEvent.TeacherId = teacher.Id;
    schEvent.Capacity = (int) appointment.duration/appointment.timeLimit;
    try
    {
        unitOfWork.Appointments.Update(schEvent);
        unitOfWork.Complete();
    }
    catch (Exception)
    {
        return Json(false, JsonRequestBehavior.AllowGet); ;
    }
    return Json(true, JsonRequestBehavior.AllowGet); ;
}
#endregion
#region delete
[Authorize(Roles = "Teacher")]
public bool DeleteAppointment(int AppointmentId)
{
    try
    {
        var appointment = unitOfWork.Appointments.Find(a => a.Id == AppointmentId).FirstOrDefault();
        unitOfWork.Appointments.Remove(appointment);
        unitOfWork.Complete();
    }
    catch (Exception)
    {
        return false;
    }
    return true;

}
#endregion
#region old
       /* private ApplicationDbContext _context;

        public AppointmentsController()
        {
            _context = new ApplicationDbContext();
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            AppointmentViewModel work = new AppointmentViewModel()
            {
                WorkDate = DateTime.Now,
                EndDate = DateTime.Now,
                IterationTypes = GetIterationTypes()
            };
            return View(work);
        }

        [HttpPost]
        public ActionResult Create(AppointmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
            return View(model);       
            }
            var Iteration = _context.IrerationTypes.First(i => i.Id == model.SelectedIterationTypeId);

            do
            {
                _context.Appointments.Add(
                    new Appointment()
                    {
                        StartDate = model.WorkDate,
                        CreationDate = DateTime.Now,
                        /*StartTime = model.StartTime,
                        EndTime = model.EndTime,#1#
                        TimeLimit = model.limit,
                        User_Id = User.Identity.GetUserId(),
                        /*IterationTypeId = model.SelectedIterationTypeId,#1#
                        EndDate = model.EndDate,
                        Capacity = (int)((model.EndTime.TotalSeconds - model.StartTime.TotalSeconds) /
                           model.limit)
                    });
                    model.WorkDate = model.WorkDate.AddDays(Iteration.Span);
                
            } while (model.WorkDate <= model.EndDate);
            
            _context.SaveChanges();

            return RedirectToAction("Index", "Manage");
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var work = _context.Appointments.First(w => w.Id == Id);
            if (work == null)
            {
                return HttpNotFound();
            }
            AppointmentViewModel model = new AppointmentViewModel()
            {
                Id = work.Id,
                WorkDate = work.StartDate,
                EndDate = work.EndDate,
                /*StartTime = work.StartTime.Value,
                EndTime = work.EndTime.Value,
                SelectedIterationTypeId =  (int) (work.IterationTypeId!=null?2:work.IterationTypeId),#1#
                IterationTypes = GetIterationTypes(),
                limit =  work.TimeLimit,
            };
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(AppointmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Appointment work = _context.Appointments.First(w => w.Id == model.Id);
            work.StartDate = model.WorkDate; 
            /*work.StartTime = model.StartTime; 
            work.EndTime = model.EndTime; #1#
            work.TimeLimit = model.limit;
            work.Capacity = (int) ((model.EndTime.TotalSeconds - model.StartTime.TotalSeconds)/
                                   model.limit);
            _context.Entry(work).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index", "Manage");
            /*var Iteration = _context.IrerationTypes.First(i => i.Id == model.SelectedIterationTypeId);
            do
            {
                _context.Appointments.Add(
                    new Appointment()
                    {
                        AppointmentDate = model.AppointmentDate,
                        CreationDate = DateTime.Now,
                        StartTime = model.StartTime,
                        EndTime = model.EndTime,
                        TimePerClient = model.limit,
                        User_Id = User.Identity.GetUserId(),
                        IterationTypeId = model.SelectedIterationTypeId,
                        IterationFinish = model.EndDate
                    });
                model.AppointmentDate = model.AppointmentDate.AddDays(Iteration.Span);
            } while (model.AppointmentDate <= model.EndDate);

            _context.SaveChanges();

            return RedirectToAction("Index", "Manage");#1#
        }

        private IEnumerable<SelectListItem> GetIterationTypes()
        {
            
            var roles = _context.IrerationTypes.OrderBy(x=>x.Span).Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.Name,
                                });

            return new SelectList(roles, "Value", "Text");
        }

        public ActionResult Details(int Id)
        {
            var work = _context.Appointments.Where(w => w.Id == Id).Include(u => u.User).Include(q=>q.Attendees).First();
            return View(work);
        }*/
#endregion old
    }
}