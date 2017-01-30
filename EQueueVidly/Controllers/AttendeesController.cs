using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EQueueVidly.Models;
using EQueueVidly.ViewModels;
using Microsoft.AspNet.Identity;

namespace EQueueVidly.Controllers
{
    public class AttendeesController : BaseController
    {
        #region creat
        [Authorize(Roles = "Student")]
        [HttpGet]
        public JsonResult JoinEvent(int AppointmentId)
        {
            var appointment = unitOfWork.Appointments.GetAppointmentById(AppointmentId);
            var userId = User.Identity.GetUserId();
            var student = unitOfWork.Students.Find(s => s.AccountId == userId).FirstOrDefault();

            if (appointment != null && student != null)
            {
                int minutes = appointment.TimeLimit * (appointment.Attendees.Count);
                int span = appointment.TimeLimit * (appointment.Attendees.Count + 1);
                Attendee queueItem = new Attendee
                {
                    StudentId = student.Id/*User.Identity.GetUserId()*/,
                    JoinDate = DateTime.Now,
                    AppointmentId = AppointmentId,
                    Start = appointment.StartDate.AddMinutes(minutes),
                    End = appointment.StartDate.AddMinutes(span)
                };


                try
                {
                    unitOfWork.Attendees.Add(queueItem);
                    unitOfWork.Complete();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }

                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region read

        public ActionResult GetStudentEvents(string start, string end)
        {
            var student = unitOfWork.Students.GetStudentByAccountId(User.Identity.GetUserId());
            DateTime Start = DateTime.Parse(start);
            DateTime End = DateTime.Parse(end);
            var events = unitOfWork.Attendees.GetAttendeesByTimeSpan(Start, End, student.Id).ToList();
            List<StudentEvent> EventItems = new List<StudentEvent>();
            foreach (var queueItem in events)
            {
                StudentEvent studentEvent = new StudentEvent()
                {
                    id = queueItem.Id,
                    start = queueItem.Start.ToString("s"),
                    end = queueItem.End.ToString("s"),
                    TeacherName = queueItem.Appointment.Teacher.account.FirstName + " " + queueItem.Appointment.Teacher.account.LastName,
                    title = queueItem.Appointment.Title
                };
                EventItems.Add(studentEvent);
            }
            return Json(EventItems, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public bool IsAppointmentMember(string userId, int eventId)
        {
            var student = unitOfWork.Students.Find(s => s.AccountId == userId).FirstOrDefault();
            var items = unitOfWork.Attendees.Find(q => (q.StudentId == student.Id && q.AppointmentId == eventId)).ToList();
            if (items.Count != 0)
            {
                return true;
            }
            return false;
        }

        public ActionResult QrCode(int id)
        {
           var appointment = unitOfWork.Attendees.GetAttendeesById(id);
            var user_id = User.Identity.GetUserId();
           var user = context.Users.Find(user_id);
            QrCodeViewModel viewModel = new QrCodeViewModel()
            {
                StudentName = user.FirstName + " " +user.LastName,
                Start = appointment.Start.ToString("g"),
                End = appointment.End.ToString("g")
            };
           
           return View(viewModel);
        }
        #endregion
        #region update
        #endregion
        #region delete
        public bool DeleteEvent(int AttendeeId)//todo replase queue items after deleting
        {
            try
            {
                var attendee = unitOfWork.Attendees.GetAttendeesById(AttendeeId);
                var attendees = attendee.Appointment.Attendees.OrderByDescending(a=>a.Start);
                foreach (var item in attendees)
                {
                    if (item.Start > attendee.Start)
                    {
                        var subtr = 0 - attendee.Appointment.TimeLimit;
                        item.Start = item.Start.AddMinutes(subtr);
                        item.End = item.End.AddMinutes(subtr);
                        unitOfWork.Attendees.Update(item);
                        unitOfWork.Complete();
                    }
                }
                unitOfWork.Attendees.Remove(attendee);
                unitOfWork.Complete();
            }
            catch (Exception)
            {
                return false;
            }
            return true;

        }
        #endregion
    }
}