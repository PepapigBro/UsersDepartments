using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UsersDepartments.Models
{

    public class Department
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [NotMapped]
        public virtual ICollection<User> Users { get; set; }

        public Department()
        {
            Users = new List<User>();
        }

    }
}