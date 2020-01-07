using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM_API.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CRM_API2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //第一种：设置上下文对象生命周期为应用程序生命一次
            //CRMContext db = new CRMContext();
            ////将上下文类 装载到服务中
            //services.AddSingleton<CRMContext>(db);



            //第二种：直接将上下文对象装载到程序服务中，并配置连接字符串
            services.AddDbContext<CRMContext>(options =>
       options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //配置跨域处理
            services.AddCors(options =>
            {
                options.AddPolicy("cors", builder =>
                {
                    builder.WithOrigins("http://localhost:6321", "http://localhost:38876")//允许指定域名访问
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();//指定处理cookie
                });
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("cors");
            //app.UseCors(builder => builder.WithOrigins("http://example.com"));
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
