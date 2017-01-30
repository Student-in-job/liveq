using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EQueueVidly.Models;

namespace EQueueVidly.Core.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        Student GetStudentById(int id);
        Student GetStudentByAccountId(string id);
    }
}
