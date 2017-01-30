using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EQueueVidly.Models;

namespace EQueueVidly.Core.Repositories
{
    public interface IAttendeeRepository : IRepository<Attendee>
    {
        IQueryable<Attendee> GetAttendeesByStudentId(int id);
        Attendee GetAttendeesById(int id);
        IQueryable<Attendee> GetAttendeesByTime(DateTime start, DateTime end, int id);
        IQueryable<Attendee> GetAttendeesByTimeSpan(DateTime start, DateTime end, int id);
    }
}
