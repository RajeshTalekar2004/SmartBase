using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SmartBase.BusinessLayer.Persistence;
using SmartBase.BusinessLayer.Services;
using SmartBase.BusinessLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer
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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmartBase-Bussiness", Version = "v1", Description = "REST API for SmartBase Account System" });

            });

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("SmartBase-Bussiness-Specification",
                    new OpenApiInfo
                    {
                        Title = "SmartBase-Bussiness",
                        Version = "v1",
                        Description = "Smart Accouting",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                        {
                            Email = "rajeshtalekar@gmail.com",
                            Name = "Smart Base"
                        },
                    }
                 );

                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                setupAction.IncludeXmlComments(xmlCommentsFullPath,true);

                //Adding Cors filters
                services.AddCors(option =>
                {
                    option.AddPolicy("Default",
                        builder => builder
                        .AllowAnyOrigin().AllowAnyHeader().WithMethods("GET", "POST", "PUT", "DELETE"));
                });

                var settingsKey = Configuration["JwtSettings:key"];
                services.AddAuthentication(def =>
                {
                    def.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    def.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(options =>
                        {
                            options.RequireHttpsMetadata = false;
                            options.SaveToken = true;
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuerSigningKey = true,
                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settingsKey)),
                                ValidateIssuer = false,
                                ValidateAudience = false,
                            // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                            ClockSkew = TimeSpan.Zero
                            };
                            options.Events = new JwtBearerEvents
                            {
                                OnAuthenticationFailed = context =>
                                {
                                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                                    {
                                        context.Response.Headers.Add("Token-Expired", "true");
                                    }
                                    return Task.CompletedTask;
                                }
                            };

                 });

                // add JWT Authentication
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // must be lower case
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                setupAction.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
                });

                // add Basic Authentication
                var basicSecurityScheme = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    Reference = new OpenApiReference { Id = "BasicAuth", Type = ReferenceType.SecurityScheme }
                };
                setupAction.AddSecurityDefinition(basicSecurityScheme.Reference.Id, basicSecurityScheme);
                setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {basicSecurityScheme, new string[] { }}
                });

            });

            services.AddApiVersioning(setupAction =>
            {
                setupAction.AssumeDefaultVersionWhenUnspecified = true;
                setupAction.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                setupAction.ReportApiVersions = true;
            });

            //Adding Cors filters
            services.AddCors(option =>
            {
                option.AddPolicy("Default",
                    builder => builder
                    .AllowAnyOrigin().AllowAnyHeader().WithMethods("GET", "POST", "PUT", "DELETE"));
            });


            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );


            //Register database context
            services.AddDbContext<SmartAccountContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //automapper
            services.AddAutoMapper(typeof(Startup));

            //JWT Token
            services.AddAuthentication(def =>
            {
                def.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                def.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                        .GetBytes(Configuration.GetSection("JwtSettings:key").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };

            });

            //Register Services
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IUserService, UserServicecs>();
            services.AddScoped<ISgstMasterService, SgstMasterService>();
            services.AddScoped<ICgstMasterService, CgstMasterService>();
            services.AddScoped<IIgstMasterService, IgstMasterService>();
            services.AddScoped<IAccountMasterService, AccountMasterService>();
            services.AddScoped<IBillMasterService, BillMasterService>();
            services.AddScoped<IBillDetailService, BillDetailService>();
            services.AddScoped<ILedgerService, LedgerService>();
            services.AddScoped<ITransactionMasterService, TransactionMasterService>();
            services.AddScoped<ITypeMasterService, TypeMasterService>();
            services.AddScoped<IVoucherMasterService, VoucherMasterService>();
            services.AddScoped<IVoucherDetailService, VoucherDetailService>();
            services.AddControllers();

            services.AddApiVersioning(setupAction =>
            {
                setupAction.AssumeDefaultVersionWhenUnspecified = true;
                setupAction.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                setupAction.ReportApiVersions = true;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            //Enable middleware to serve swagger-ui
            app.UseSwaggerUI(setupAction => {
                setupAction.SwaggerEndpoint("/swagger/SmartBase-Bussiness-Specification/swagger.json", "BusinessLayer API");
               // setupAction.RoutePrefix = "";
                setupAction.DefaultModelExpandDepth(2);
                setupAction.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Model);
                setupAction.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                setupAction.EnableDeepLinking();
                setupAction.DisplayOperationId();
            });


            app.UseHttpsRedirection();
            app.UseRouting();

            var _configuration = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                       .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                       .AddEnvironmentVariables()
                       .Build();

            var hosts   = _configuration.GetSection("Cors:AllowedOriginsList").Get<List<string>>();
            app.UseCors(options =>
                        options.WithOrigins(hosts.ToArray())
                        .AllowAnyMethod()
                        .AllowAnyHeader()
             );

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
