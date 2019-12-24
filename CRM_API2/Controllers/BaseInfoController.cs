using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM_API.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpPost]
        public async Task<ActionResult<int>> AddEMp(EmployeeInfo em)
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
        public async Task<IActionResult> UpdateEMp(EmployeeInfo em)
        {

            db.Entry(em).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return NoContent();

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

            return await db.EmployeeInfo.FirstOrDefaultAsync(e => e.ENO.Equals(name) && e.Password.Equals(pwd));
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