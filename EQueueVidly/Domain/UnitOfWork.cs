using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EQueueVidly.Core;
using EQueueVidly.Core.Repositories;
using EQueueVidly.Domain.Repositories;
using EQueueVidly.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EQueueVidly.Domain
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Students = new StudentsRepository(context);
            Teachers = new TeachersRepository(_context);
            Appointments = new AppointmentsRepository(_context);
            Attendees = new AttendeesRepository(_context);
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public IStudentRepository Students { get; private set; }
        public ITeacherRepository Teachers { get; private set; }
        public IAppointmentRepository Appointments { get; private set; }
        public IAttendeeRepository Attendees { get; private set; }
        public int Complete()
        {
            return _context.SaveChanges();
        }
    }
}