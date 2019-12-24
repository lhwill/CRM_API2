using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM_API.Model
{
    public class DepartmentInfo
    {
        [Key]
        public int DId { get; set; }
        public string DName { get; set; }
        public string Remark { get; set; }
    }
}
