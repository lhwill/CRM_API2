using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM_API.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRM_API2.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("cors")]//设置跨域处理的代理

    public class SysController : ControllerBase
    {
        public CRMContext db;
        public SysController(CRMContext db) { this.db = db; }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpGet]
        public async Task<ActionResult<int>> AddMenu(string MName, string URL, string Remark, int PId,int Sort, string cb)
        {
            db.MenuInfo.Add(new MenuInfo { MName = MName, URL = URL, Remark = Remark, PId = PId, Sort = Sort });
            return await db.SaveChangesAsync();
        }
        [HttpPost("AddMenuAxios")]
        public async Task<ActionResult<int>> AddMenu([FromBody]MenuInfo menuinfo)
        {
            db.MenuInfo.Add(menuinfo);
            return await db.SaveChangesAsync();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
