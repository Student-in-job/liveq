using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EQueueVidly.Core.Repositories;

namespace EQueueVidly.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IStudentRepository Students { get; }
        ITeacherRepository Teachers { get; }
        IAppointmentRepository Appointments { get; }
        IAttendeeRepository Attendees { get; }

        int Complete();
    }
}