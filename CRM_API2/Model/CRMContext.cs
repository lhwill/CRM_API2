using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace CRM_API.Model
{
    //ef core 使用codeFirst迁移生成数据库
    //在程序包管理器控制台依次执行以下命令
    //Add-Migration Init  //其中Init是你的版本名称
    //update-database Init //更新数据库操作 init为版本名称
    public class CRMContext:DbContext
    {


        public CRMContext() { }
        public CRMContext(DbContextOptions<CRMContext> options):base(options) { }



        public DbSet<Base_Dictionary> Base_Dictionary { get; set; }
        public DbSet<Buy_Product> Buy_Product { get; set; }
        public DbSet<Contact_Record> Contact_Record { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Customer_Away> Customer_Away { get; set; }
        public DbSet<Customer_Person> Customer_Person { get; set; }
        public DbSet<Customer_plan> Customer_plan { get; set; }
        public DbSet<Customer_Service> Customer_Service { get; set; }
        public DbSet<DepartmentInfo> DepartmentInfo { get; set; }
        public DbSet<EmployeeInfo> EmployeeInfo { get; set; }
        public DbSet<EmpRole> EmpRole { get; set; }
        public DbSet<MenuInfo> MenuInfo { get; set; }
        public DbSet<RoleInfo> RoleInfo { get; set; }
        public DbSet<RoleMenu> RoleMenu { get; set; }
        public DbSet<Sale_Chance> Sale_Chance { get; set; }
        public DbSet<Student> Student { get; set; }
    }
}
