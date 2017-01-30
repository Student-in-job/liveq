using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EQueueVidly.ViewModels
{
    public class ScheduleResource
    {
        public string id { get; set; } // required
        public string title { get; set; } // required
       /* public string notes { get; set; }
        // add any other general fields you might want to access in the browser to this property list
        public List<ScheduleResource> children { get; set; } // optional, but, required if you want 1:M relationship, ie: Branches have many Users .. build up list recursively

        public ScheduleResource()
        {
            children = new List<ScheduleResource>();
        }*/
    }
}