using CRUDClienteEndereco.Configuration;
using CRUDClienteEndereco.Infra.Mapping;
using CRUDClienteEndereco.Services;
using CRUDClienteEndereco.Services.Interfaces;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NHibernate.Driver;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var tokenSettings = new TokenSettings();
            Configuration.GetSection("TokenSettings").Bind(tokenSettings);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = tokenSettings.Issuer,
                        ValidAudience = tokenSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.SecurityKey))
                    };
                }
                );

            var oracleConfig = OracleClientConfiguration.Oracle10.ConnectionString(c =>
                                c.Is(Configuration["ConnectionString"])).Driver<OracleManagedDataClientDriver>();
            
            var _sessionFactory = Fluently.Configure()
                                      .Database(oracleConfig)
                                      .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ClienteMap>())
                                      .ExposeConfiguration(BuildSchema).BuildSessionFactory();

            services.AddScoped(factory =>
            {
                return _sessionFactory.OpenSession();
            });

            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IClienteService, ClienteService>();

            services.AddSingleton(tokenSettings);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        private static void BuildSchema(NHibernate.Cfg.Configuration config)
        {
            new SchemaExport(config)
              .Create(false, false);
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
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/{controller}/{action}");
            });


        }
    }
}
