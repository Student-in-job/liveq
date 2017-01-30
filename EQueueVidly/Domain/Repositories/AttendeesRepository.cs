using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EQueueVidly.Core.Repositories;
using EQueueVidly.Models;

namespace EQueueVidly.Domain.Repositories
{
    public class AttendeesRepository:Repository<Attendee>, IAttendeeRepository
    {
        public AttendeesRepository(DbContext context) : base(context)
        {
        }

        public ApplicationDbContext AppContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public IQueryable<Attendee> GetAttendeesByStudentId(int id)
        {
            return AppContext.Attendees.Include(a => a.Appointment).Where(a => a.Id == id);
        }

        public Attendee GetAttendeesById(int id)
        {
            return AppContext.Attendees.Include(a=>a.Student.Account).Include(a=>a.Appointment.Attendees).Include(a=>a.Appointment).FirstOrDefault(a => a.Id == id);
        }

        public IQueryable<Attendee> GetAttendeesByTime(DateTime start, DateTime end, int id)
        {
            return AppContext.Attendees.Where(s =>
                (s.Start >= start & s.End <= end & s.StudentId == id))
                .Include(q => q.Student)
                .Include(q => q.Appointment.Teacher);
        }

        public IQueryable<Attendee> GetAttendeesByTimeSpan(DateTime start, DateTime end, int id)
        {
            return AppContext.Attendees.Where(s =>
                (s.Start >= start & s.End <= end & s.StudentId == id))
                .Include(q => q.Student)
                .Include(q => q.Appointment)
                .Include(q => q.Appointment.Teacher)
                .Include(q => q.Appointment.Teacher.account);
        }
    }
}