using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using EQueueVidly.Models;

namespace EQueueVidly.Core.Repositories
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        Appointment GetAppointmentById(int id);
    }


}