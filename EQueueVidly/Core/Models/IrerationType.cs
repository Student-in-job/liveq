using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EQueueVidly.Models
{
    public class IrerationType
    {

            public IrerationType()
            {
                this.WorkTimes = new HashSet<Appointment>();
            }
            [Key]
            public int Id { get; set; }
            public string Name { get; set; }
            public int Span { get; set; }


            public ICollection<Appointment> WorkTimes { get; set; }
        }

}