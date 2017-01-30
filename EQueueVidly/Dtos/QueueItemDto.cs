using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EQueueVidly.Dtos
{
    public class QueueItemDto
    {
        public DateTime WorkDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
       /* public string FirstName { get; set; }
        public string LastName { get; set; }*/
    }
}