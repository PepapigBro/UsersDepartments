using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UsersDepartments.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext() : base("UserDBConnection") { }


        public DbSet<User> Users { get; set; }
    }

    public class DepartmentsContext : DbContext
    {
        public DepartmentsContext(): base("DepartmentDBConnection"){}
        public DbSet<Department> Departments { get; set; }
    }


    public class UserDbInitializer : CreateDatabaseIfNotExists<UsersContext>
    {
        protected override void Seed(UsersContext db)
        {
            // db.Scriptures.Add(new Scripture {Text = "Hello"});
            // db.SaveChanges();

            base.Seed(db);
        }
    }


    public class DepartmentDbInitializer : CreateDatabaseIfNotExists<DepartmentsContext>
    {
        protected override void Seed(DepartmentsContext db)
        {
            // db.Scriptures.Add(new Scripture {Text = "Hello"});
            // db.SaveChanges();

            base.Seed(db);
        }
    }

}




