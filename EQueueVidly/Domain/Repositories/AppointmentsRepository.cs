using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EQueueVidly.Core.Repositories;
using EQueueVidly.Models;

namespace EQueueVidly.Domain.Repositories
{
    public class AppointmentsRepository:Repository<Appointment>, IAppointmentRepository
    {
        public AppointmentsRepository(DbContext context) : base(context)
        {
        }

        public ApplicationDbContext AppContext
        {
            get { return Context as ApplicationDbContext; }
        }

        /*public void UpdateAppointment(Appointment appointment)
        {
            AppContext.Entry(appointment).State = EntityState.Modified;
        }*/
        public Appointment GetAppointmentById(int id)
        {
            return AppContext.Appointments.Include(a => a.Teacher.account).Include(a => a.Attendees).FirstOrDefault(a => a.Id == id);
        }
    }
}