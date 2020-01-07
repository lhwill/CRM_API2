using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM_API.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CRM_API.Controllers
{
    //改变响应格式
    [Produces("application/json", "application/xml")]
    [Route("api/[controller]")]
    // [ApiController]
    [EnableCors("cors")]//设置跨域处理的代理
    public class BaseInfoController : ControllerBase
    {
        public CRMContext db;
        public BaseInfoController(CRMContext db) { this.db = db; }

        /// <summary>
        /// 添加员工
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        [HttpPost("AddEmp")]
        public async Task<ActionResult<int>> AddEMp([FromBody]EmployeeInfo em)
        {

            db.EmployeeInfo.Add(em);
            return await db.SaveChangesAsync();

        }

        /// <summary>
        /// 修改员工
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UptEmp")]
        public async Task<ActionResult<int>> UpdateEMp([FromBody]EmployeeInfo em)
        {
            //db.EmployeeInfo.AsNoTracking();
            //db.Entry(em).State = EntityState.Modified;
            //int task= await db.SaveChangesAsync();
            //db.Entry(em).State = EntityState.Deleted;
            //return task;



            db.Entry(em).State = EntityState.Modified;
            return await db.SaveChangesAsync();


        }

        /// <summary>
        /// 删除员工
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<int>> RemoveEMp(int id)
        {
            db.EmployeeInfo.Remove(db.EmployeeInfo.FirstOrDefault(m => m.EId == id));
            return await db.SaveChangesAsync();

        }
        [Route("GetEmpJsonP")]
        public ActionResult GetEmp1(string cbEmp)
        {
            return Content(cbEmp + "(" + JsonConvert.SerializeObject(db.EmployeeInfo.ToList()) + ")");
        }
        /// <summary>
        /// 获取所有员工
        /// </summary>
        /// <returns></returns>
        [Route("GetEmp")]
        public async Task<ActionResult<IEnumerable<EmployeeInfo>>> GetEmp()
        {
            return await db.EmployeeInfo.ToListAsync();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [Route("Login")]
        public async Task<ActionResult<EmployeeInfo>> GetEmpLogin(string name, string pwd)
        {

            return await db.EmployeeInfo.AsNoTracking().FirstOrDefaultAsync(e => e.ENO.Equals(name) && e.Password.Equals(pwd));
        }

        /// <summary>
        /// 根据用户ID查询对应的菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetMenu/{id}")]
        public async Task<List<MenuInfo>> GetMenu(int id)
        {
            var linq = from m in db.MenuInfo
                       join rm in db.RoleMenu on m.MId equals rm.MId
                       join r in db.RoleInfo on rm.RId equals r.RId
                       join er in db.EmpRole on r.RId equals er.RID
                       join e in db.EmployeeInfo on er.EID equals e.EId
                       where e.EId == id
                       select m;
            return await linq.ToListAsync();
        }

    }
}