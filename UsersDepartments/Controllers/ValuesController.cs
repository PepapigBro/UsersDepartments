using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using UsersDepartments.Models;

namespace UsersDepartments.Controllers
{
    public class ValuesController : ApiController
    {

        private DepartmentsContext departmentsContext = new DepartmentsContext();
        private UsersContext usersContext = new UsersContext();

        //Api for Users
        [HttpGet]
        public string GetAllUsers()
        {
           List<User> users = usersContext.Users.ToList()
                            .GroupJoin(departmentsContext.Departments, o=>o.DepartmentId, e=>e.Id, (o,e)=>new {user=o, department=e.SingleOrDefault()})
                         
                            .ToList()
                            .Select(p=> {
                                if (p.department != null)
                                {
                                    p.user.Department = p.department;
                                }
                                else
                                {
                                    p.user.Department = new Department();
                                    
                                }

                                    return p.user;


                            })
                .ToList();


  // List<User> users =
  // from c in usersContext.Users.ToList()
  // join p in departmentsContext.Departments on c.DepartmentId equals p.Id into ps
  // from p in ps.DefaultIfEmpty()
  // select new { User = c, Department = p }
  // as 
  // 
  // List<User>
  // sele


            var json = JsonConvert.SerializeObject(users);          
            // return Json(json, JsonRequestBehavior.AllowGet);
            return json;
        }

        [HttpPost]
        public string CreateUser([FromBody]string value)
        {
            User newUser = new User() { Name = value };
            usersContext.Users.Add(newUser);
            usersContext.SaveChanges();

            return JsonConvert.SerializeObject(newUser);
        }

        [HttpPut]
        public string EditUser([FromBody]User user)
        {
          
            usersContext.Entry(user).State = EntityState.Modified;

            user.Department = departmentsContext.Departments.Where(p => p.Id == user.DepartmentId).FirstOrDefault();

            usersContext.SaveChanges();
            return JsonConvert.SerializeObject(user);

        }

        [HttpDelete]
        public bool RemoveUser(int id)
        {
           
            User user = usersContext.Users.Find(id);
            if (user != null)
            {
                usersContext.Users.Remove(user);
                usersContext.SaveChanges();
                return true;
            }
            else { return false; }
        }

        // [HttpGet]
        // public string GetUserInfo(int id)
        // {
        //     return "value";
        // }
        //Api for Users






        //Api for Departments
        [HttpGet]
        public string GetAllDepartments()
        {
            List<Department> departments = departmentsContext.Departments.ToList();
           // return departments;

            return JsonConvert.SerializeObject(departments);
            //  return new string[] { "value1", "value2" };
        }

        [HttpPost]
        public string CreateDepartment([FromBody]string value)
        {
            Department newDepartment = new Department() { Title = value };
            departmentsContext.Departments.Add(newDepartment);
            departmentsContext.SaveChanges();
            return JsonConvert.SerializeObject(newDepartment);
        }

        [HttpPut]
        public bool EditDepartment([FromBody]Department department)
        {

                departmentsContext.Entry(department).State = EntityState.Modified;
                departmentsContext.SaveChanges();
                return true;
          
        }

        [HttpDelete]
        public bool RemoveDepartment(int id)
        {

            Department department = departmentsContext.Departments.Find(id);
                if (department != null)
            {
                departmentsContext.Departments.Remove(department);
                departmentsContext.SaveChanges();

                //Обнуляем у всех пользователей этого департамента DepartmentId
                var users = usersContext.Users.Where(p => p.DepartmentId == id).ToList()
                .Select(p => { p.DepartmentId = null; return p; });

                foreach (User user in users)
                {
                    usersContext.Entry(user).State = EntityState.Modified;
                }
                usersContext.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }


        [HttpGet]
        public Department GetDepartmentInfo(int id)
        {
            Department department = departmentsContext.Departments
                                      .Include(p => p.Users)
                                      .FirstOrDefault();
            return department;

            // var json = JsonConvert.SerializeObject(department);          
            // return Json(json, JsonRequestBehavior.AllowGet);

        }
        //Api for Departments


    }
}
