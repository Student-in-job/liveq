using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EQueueVidly.Models;

namespace EQueueVidly.Core.Repositories
{
    public interface ITeacherRepository:IRepository<Teacher>
    {
        IEnumerable<Teacher> GetAllTeachersWithAccount();

        Teacher GetTeacherById(int id);
        Teacher GetTeacherByAccountId(string id);

        IEnumerable<Appointment> GetAllAppointments(DateTime start, DateTime end, int id);
    }
}
