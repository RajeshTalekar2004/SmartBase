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

            services.AddCors(option =>
            {
                option.AddPolicy("Default", builder =>
                builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .WithOrigins("http://localhost:4200"));
            });


            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );


            //Register database context
            services.AddDbContext<SmartAccountContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //automapper
            services.AddAutoMapper(typeof(Startup));

            //JWT Token
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                        .GetBytes(Configuration.GetSection("AppSettings:JwtToken").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
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
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartBase-Bussiness v1");
            });

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("Default");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
