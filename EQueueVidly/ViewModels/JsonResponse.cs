using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EQueueVidly.Models
{
    public class JsonResponse
    {
        public static String SUCCESS = "success";
        public static String ERROR = "error";
        public static String DEFAULT_ERROR_MSG = "Some error accured!";
        public static String DEFAULT_SUCCESS_MSG = "Done!";


        public JsonResponse(string status, object obj)
        {
            response = status;
        }

        private String response { get; set; }
        private Object responseObj { get; set; }
    }


}