using AutoMapper;
using CRUDClienteEndereco.Dominio.Contratos;
using CRUDClienteEndereco.Dominio.Entidades;
using CRUDClienteEndereco.Dominio.Servicos;
using CRUDClienteEndereco.Infra.Mapping;
using CRUDClienteEndereco.Web.Configuration;
using CRUDClienteEndereco.Web.Services;
using CRUDClienteEndereco.Web.Services.Interfaces;
using CRUDClienteEndereco.Web.ViewModel;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NetCoreSPA.Web
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

            FluentNhibernateConfiguration(services);

            AutoMapperMap(services);

            services.AddScoped<IUsuarioApplicationService, UsuarioApplicationService>();
            services.AddScoped<IClienteApplicationService, ClienteApplicationService>();
            services.AddScoped<IClienteDomainService, ClienteDomainService>();

            services.AddSingleton(tokenSettings);

            services.AddMvc();
            services.AddCors(o => o.AddPolicy("AppPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
        }

        private void FluentNhibernateConfiguration(IServiceCollection services)
        {
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
        }

        private static void AutoMapperMap(IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Cliente, ClienteViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ClienteId))
                .ForMember(dest => dest.TipoDocumento, opt => opt.MapFrom(src => src.TipoDocumento.TipoDocumentoId))
                .ForMember(dest => dest.Logradouro, opt => opt.MapFrom(src => src.Enderecos.Count == 0 ? string.Empty : src.Enderecos[0].Logradouro))
                .ForMember(dest => dest.Numero, opt => opt.MapFrom(src => src.Enderecos.Count == 0 ? 0 : src.Enderecos[0].Numero))
                .ForMember(dest => dest.Bairro, opt => opt.MapFrom(src => src.Enderecos.Count == 0 ? string.Empty : src.Enderecos[0].Bairro))
                .ForMember(dest => dest.Complemento, opt => opt.MapFrom(src => src.Enderecos.Count == 0 ? string.Empty : src.Enderecos[0].Complemento))
                .ForMember(dest => dest.Cep, opt => opt.MapFrom(src => src.Enderecos.Count == 0 ? string.Empty : src.Enderecos[0].Cep))
                .ForMember(dest => dest.UF, opt => opt.MapFrom(src => src.Enderecos.Count == 0 ? string.Empty : src.Enderecos[0].UF));

                cfg.CreateMap<ClienteViewModel, Cliente>()
                .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TipoDocumento, opt => opt.MapFrom(src => new TipoDocumento { TipoDocumentoId = src.TipoDocumento }))
                .ForMember(dest => dest.Enderecos, opt => opt.MapFrom(src => new List<Endereco>() { new Endereco {
                                                                                                        Logradouro = src.Logradouro,
                                                                                                        Bairro = src.Bairro,
                                                                                                        Numero = src.Numero,
                                                                                                        Complemento = src.Complemento,
                                                                                                        UF = src.UF,
                                                                                                        Cep = src.Cep,
                                                                                                        ClienteId = src.Id
                                                                                                    }}));

            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
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
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            // Middleware to Handle Client Side Routes
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value)
                    && !context.Request.Path.Value.Contains("/api"))
                {
                    context.Request.Path = "/index.html";
                    context.Response.StatusCode = 200;
                    await next();
                }
            });

            DefaultFilesOptions options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("/index.html");
            app.UseDefaultFiles(options);

            app.UseStaticFiles();
            app.UseFileServer(enableDirectoryBrowsing: false);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/{controller}/{action}");
            });
        }
    }
}
