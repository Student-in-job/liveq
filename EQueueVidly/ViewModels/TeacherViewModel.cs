using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EQueueVidly.ViewModels
{
    public class TeacherViewModel
    {
        public string TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}