using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            //Configuration = configuration;
            //Environment = environment;
        }
        //public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddSessionStateTempDataProvider()
                    //全局配置Json序列化处理
                    .AddJsonOptions(options =>
                    {
                        //忽略循环引用
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        //不使用驼峰样式的key
                        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                        //设置时间格式
                        //options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
                    }
                );
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            //app.UseStaticHttpContext();
            //app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "api/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
