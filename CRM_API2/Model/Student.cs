
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM_API.Model
{
    public class Student
    {
        [Key]
        public int SId { get; set; }
        public string SName { get; set; }
        public bool Sex { get; set; }

    }
}
