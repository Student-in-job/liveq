using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using EQueueVidly.Dtos;
using EQueueVidly.Models;
using EQueueVidly.ViewModels;

namespace EQueueVidly.Controllers.Api
{
    public class AppointmentsController : ApiController
    {
        private ApplicationDbContext _context;

        public AppointmentsController()
        {
            _context = new ApplicationDbContext();
        }
        //
        // GET: /api/works
        public IHttpActionResult GetAppointments(string query = null)
        {
            IQueryable<Appointment> workQuery = _context.Appointments.Include(m => m.IterationType);

            /*if (!String.IsNullOrWhiteSpace(query))
            {
                workQuery = workQuery.Where(w => w.User_Id == query).Include(m => m.IterationType);
            }*/
            
            var customerDtos = workQuery
                .ToList()
                .Select(Mapper.Map<Appointment, AppintmentDto>);
            return Ok(customerDtos);
        }
        [Route("api/schedule/appointments")]
        [HttpPost]
        public IHttpActionResult GetScheduleEvents(string start, string end)
        {
            DateTime Start = DateTime.Parse(start);
            DateTime End = DateTime.Parse(end);
            List<ScheduleEvent> EventItems = new List<ScheduleEvent>();
            IEnumerable<ScheduleEventVM> selectedEvents = null;
            selectedEvents = _context.Appointments.Where(s => (s.StartDate >= Start & s.EndDate <= End)).Select(Mapper.Map<Appointment,ScheduleEventVM>);
            if (selectedEvents != null)
            {

                foreach (var scheduleEvent in selectedEvents)
                {
                    ScheduleEvent itm = new ScheduleEvent();
                    itm.id = scheduleEvent.Id;
                    if (scheduleEvent.Title.Trim() == "")
                        itm.title = "Pusto";
                    else itm.title = scheduleEvent.Title;
                    itm.start = scheduleEvent.StartDate.ToString("s");
                    itm.duration = scheduleEvent.Durration;
                    itm.timeLimit = scheduleEvent.TimeLimit;
                    /*itm.TeacherId = scheduleEvent.TeacherId;*/
                    itm.TeacherName = scheduleEvent.TeacherName;
                    itm.resourceId = scheduleEvent.TeacherId;
                    EventItems.Add(itm);
                }
                
            }
            return Json(EventItems);
        }

        /*[Route("api/appointments/push")]
        [HttpPost]
        public void PushAppointment(ScheduleEvent appointment)
        {
            var schEvent = new Appointment();
            schEvent.StartDate = DateTime.Parse(appointment.start);
            schEvent.EndDate = DateTime.Parse(appointment.end);
            schEvent.Durration = appointment.duration;
            schEvent.TimeLimit = appointment.timeLimit;
            schEvent.Title = appointment.title;
            _context.Appointments.Add(schEvent);
            _context.SaveChanges();
        }*/

        // PUT /api/customers/1
        [System.Web.Http.HttpPut]
        public IHttpActionResult UpdateWork(int id, AppintmentDto appintmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customerInDb = _context.Appointments.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();

            Mapper.Map(appintmentDto, customerInDb);

            _context.SaveChanges();

            return Ok();
        }

        [System.Web.Http.HttpDelete]
        public IHttpActionResult DeleteWork(int id)
        {
            var workInDb = _context.Appointments.SingleOrDefault(c => c.Id == id);

            if (workInDb == null)
                return NotFound();

            _context.Appointments.Remove(workInDb);
            _context.SaveChanges();

            return Ok();
        }

	}
}