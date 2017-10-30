using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UsersDepartments.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        [NotMapped]
        public virtual Department Department { get; set; }

    }

}