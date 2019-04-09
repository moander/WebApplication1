using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace WebApplication1
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddMvcCore()
                .AddApiExplorer()
                                .AddJsonOptions(x =>
                                {
                                    x.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
                                    x.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
                                    x.SerializerSettings.DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffZ";
                                    x.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter(true));
                                    x.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Include;
                                    //x.SerializerSettings.ContractResolver = new DefaultContractResolver
                                    //{
                                    //    NamingStrategy = new SnakeCaseNamingStrategy()
                                    //};
                                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("1.0", new Info { Title = "My API V1", Version = "1.0" });
                c.EnableAnnotations();

                // c.TagActionsBy(api => new string[] { api.GroupName });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/1.0/swagger.json", "My API V1");
            });

            app.UseMvc();

        }
    }
}
