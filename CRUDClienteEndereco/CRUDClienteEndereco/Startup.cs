using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NHibernate.Tool.hbm2ddl;
using System.Text;

namespace CRUDClienteEndereco
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "ApiSegura",
                        ValidAudience = "ApiSegura",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecurityKey"]))
                    };
                }
                );

            var oracleConfig = OracleClientConfiguration.Oracle10.ConnectionString(c =>
                                c.Is(Configuration["ConnectionString"]));

            var _sessionFactory = Fluently.Configure()
                                      .Database(oracleConfig)
                                      .Mappings(m => m.FluentMappings.AddFromAssembly(GetType().Assembly))
                                      .ExposeConfiguration(BuildSchema).BuildSessionFactory();

            services.AddScoped(factory =>
            {
                return _sessionFactory.OpenSession();
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

        }
        private static void BuildSchema(NHibernate.Cfg.Configuration config)
        {
            new SchemaExport(config)
              .Create(false, true);
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
