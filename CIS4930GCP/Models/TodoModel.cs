using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIS4930GCP.Models
{
    public class TodoModel
    {
        public int index { get; set; }
        public string username { get; set; }
        public string todo { get; set; }
        public bool isComplete { get; set; }
    }
}