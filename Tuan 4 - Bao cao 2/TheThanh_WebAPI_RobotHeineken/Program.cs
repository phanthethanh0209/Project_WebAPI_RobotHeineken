using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TheThanh_WebAPI_RobotHeineken.Authorization;
using TheThanh_WebAPI_RobotHeineken.Data;
using TheThanh_WebAPI_RobotHeineken.Mapper;
using TheThanh_WebAPI_RobotHeineken.Repository;
using TheThanh_WebAPI_RobotHeineken.Services;
using TheThanh_WebAPI_RobotHeineken.Validation;

namespace TheThanh_WebAPI_RobotHeineken
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Cau hinh JWT va cau hinh dich vu Authentication
            string secretKey = builder.Configuration["Jwt:Key"]; // Doc cau hinh tu appsettings.json
            byte[] secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        //tu cap token 
                        ValidateIssuer = false,
                        ValidateAudience = false,

                        //ky vao token
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

                        ClockSkew = TimeSpan.Zero
                    };
                });


            // Dang ky Database
            IConfigurationRoot cf = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

            builder.Services.AddDbContext<MyDBContext>(opt => opt.UseSqlServer(cf.GetConnectionString("MyDB")));


            // Dang ky interface respository
            builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();


            // Dang ky interface Services
            builder.Services.AddScoped<IMachineService, MachineService>();
            builder.Services.AddScoped<ILocationService, LocationService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IPermissionService, PermissionService>();
            builder.Services.AddScoped<IRoleUserService, RoleUserService>();
            builder.Services.AddScoped<IRolePermissionService, RolePermissionService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IGiftService, GiftService>();
            builder.Services.AddScoped<IRecyclingHistoryService, RecyclingHistoryService>();
            builder.Services.AddScoped<IContainerFullHistoryService, ContainerFullHistoryService>();
            builder.Services.AddScoped<IQRCodeService, QRCodeService>();


            // Dang ky Mapper
            builder.Services.AddAutoMapper(typeof(MappingUser));
            builder.Services.AddAutoMapper(typeof(MappingRole));
            builder.Services.AddAutoMapper(typeof(MappingPermission));
            builder.Services.AddAutoMapper(typeof(MappingRoleUser));
            builder.Services.AddAutoMapper(typeof(MappingRolePermission));
            builder.Services.AddAutoMapper(typeof(MappingMachine));
            builder.Services.AddAutoMapper(typeof(MappingLocation));
            builder.Services.AddAutoMapper(typeof(MappingLocation));
            builder.Services.AddAutoMapper(typeof(MappingMachineHistory));
            builder.Services.AddAutoMapper(typeof(MappingContainerHistory));
            builder.Services.AddAutoMapper(typeof(MappingQRCode));


            // Dang ky Fluent Validation
            builder.Services.AddControllers().AddFluentValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateMachineValidator>();


            // Dang ky JwtSecurityTokenHandler
            builder.Services.AddSingleton<JwtSecurityTokenHandler>();
            builder.Services.AddSingleton<TokenValidationParameters>(provider =>
            {
                IConfiguration configuration = provider.GetRequiredService<IConfiguration>();
                byte[] key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
                return new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = false
                };
            });

            // Dang ky phan quen
            builder.Services.AddScoped<IUserPermission, UserPermission>();
            builder.Services.AddScoped<IAuthorizationHandler, PermissionHandler>();




            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication(); // Middleware xac thuc

            app.UseAuthorization();// Middleware phan quyen

            app.MapControllers();

            app.Run();
        }
    }
}
