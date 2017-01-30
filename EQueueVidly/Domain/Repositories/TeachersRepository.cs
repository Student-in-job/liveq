using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EQueueVidly.Core.Repositories;
using EQueueVidly.Models;

namespace EQueueVidly.Domain.Repositories
{
    public class TeachersRepository : Repository<Teacher>, ITeacherRepository
    {
        public TeachersRepository(DbContext context) : base(context)
        {
        }

        public ApplicationDbContext AppContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public IEnumerable<Teacher> GetAllTeachersWithAccount()
        {
            return AppContext.Teachers.Include(t => t.account).OrderBy(t=>t.AccountId).ToList();
        }

        public Teacher GetTeacherById(int id)
        {
            return AppContext.Teachers.Include(t => t.account).FirstOrDefault(t => t.Id == id);
        }

        public Teacher GetTeacherByAccountId(string id)
        {
            return AppContext.Teachers.Include(t=>t.Appointments).FirstOrDefault(t => t.AccountId == id);
        }

        public IEnumerable<Appointment> GetAllAppointments(DateTime start, DateTime end, int id)
        {
            return AppContext.Appointments.Where(s => (s.StartDate >= start & s.EndDate <= end & s.TeacherId == id)).Include(a=>a.Attendees).Include(u=>u.Teacher).Include(t=>t.Teacher.account);
        }
    }
}