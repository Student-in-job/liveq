using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EQueueVidly.ViewModels
{
    public class StudentEvent
    {
        public int id { get; set; }
        public string title { get; set; }
        public int TeacherId { get; set; }
        public int AppointmentId { get; set; }
        public string TeacherName { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string color { get; set; }
    }
}