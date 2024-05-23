global using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ProjectChinesOuction;
using ProjectChinesOuction.BLL;
using ProjectChinesOuction.DAL;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });
}

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//builder.Services.AddScoped<IPresentService, PresentService>();
//builder.Services.AddScoped<IPresentDAL, PresentDAL>();
//builder.Services.AddScoped<IDonatorService, DonatorService>();
//builder.Services.AddScoped<IDonatorDAL, DonatorDAL>();
//builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<IUserDAL, UserDAL>();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<OctionContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("OctionContext")));//יקרא מהקונפיגורציהappsettings//כדי שנוכל להשתמש בDI//כדי להריץ מיגרשן
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("CorsPolicy", //give it the name you want
//                  builder =>
//                  {
//                      builder.WithOrigins("http://localhost:4200",
//                                           "development web site")
//                                          .AllowAnyHeader()
//                                          .AllowAnyMethod()
//                                          ;
//                  });

//});
////DTO/MAPPER
//var mapperConfig = new MapperConfiguration(mc =>
//{
//    mc.AddProfile(new MappingProfile());
//});
//IMapper mapper = mapperConfig.CreateMapper();
//builder.Services.AddSingleton(mapper);



//var app = builder.Build();
//// Configure the HTTP request pipeline.

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();
//app.UseCors("CorsPolicy");

//app.Run();