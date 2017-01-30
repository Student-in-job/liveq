using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EQueueVidly.Core.Repositories;
using EQueueVidly.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EQueueVidly.Domain.Repositories
{
    public class StudentsRepository : Repository<Student>, IStudentRepository 
    {
        public StudentsRepository(DbContext context) : base(context)
        {
        }

        public ApplicationDbContext AppContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public Student GetStudentById(int id)
        {
            return AppContext.Students.Find(id);
        }

        public Student GetStudentByAccountId(string id)
        {
            return AppContext.Students.Include(s=>s.Attendees).FirstOrDefault(s => s.AccountId == id);
        }
    }
}