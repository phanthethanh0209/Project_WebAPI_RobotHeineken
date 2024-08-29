using Microsoft.EntityFrameworkCore;
using TheThanh_WebAPI_RobotHeineken.Data;
using TheThanh_WebAPI_RobotHeineken.Mapper;
using TheThanh_WebAPI_RobotHeineken.Repository;
using TheThanh_WebAPI_RobotHeineken.Services;

namespace TheThanh_WebAPI_RobotHeineken
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // Dang ky Database
            IConfigurationRoot cf = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

            builder.Services.AddDbContext<MyDBContext>(opt => opt.UseSqlServer(cf.GetConnectionString("MyDB")));


            // Dang ky interface respository
            builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>(); //Repository & unit of work
            builder.Services.AddScoped<IRepositoryBase<RecyclingMachine>, RepositoryBase<RecyclingMachine>>();

            //builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));


            // Dang ky interface Services
            builder.Services.AddScoped<IMachineService, MachineService>();

            // Dang ky Mapper
            builder.Services.AddAutoMapper(typeof(MappingMachine));



            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
