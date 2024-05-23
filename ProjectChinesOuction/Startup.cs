using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProjectChinesOuction.BLL;
using ProjectChinesOuction.DAL;
using Microsoft.OpenApi.Models;
using System.Text;
using ProjectChinesOuction.Models;

namespace ProjectChinesOuction
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
            services.AddLogging();
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IDonatorService, DonatorService>();
            services.AddScoped<IDonatorDAL, DonatorDAL>();
            services.AddScoped<IPresentService, PresentService>();
            services.AddScoped<IPresentDAL, PresentDAL>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderDAL, OrderDAL>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserDAL, UserDAL>();
            services.AddScoped<IRaffleService, RaffleService>();
            services.AddScoped<IRaffleDAL, RaffleDAL>();
            services.AddScoped<ILoginDAL, LoginDAL>();
            services.AddScoped<ILoginService, LoginService>();

            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, AuthMessageSender>();

            services.AddConnections();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<OctionContext>(options => options.UseSqlServer(Configuration.GetConnectionString("OctionContext")));

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", //give it the name you want
                              builder =>
                              {
                                  builder.WithOrigins("http://localhost:4200",
                                                       "development web site")
                                                      .AllowAnyHeader()
                                                      .AllowAnyMethod()
                                                      ;
                              });

            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = "http://localhost:7214/",
         ValidAudience = "http://localhost:7214/",
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
     };
 });


           services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            services.AddMvc();
            services.AddControllers();
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseWhen(context => !context.Request.Path.StartsWithSegments("/api/Login/login")&& !context.Request.Path.StartsWithSegments("/api/Login/register"), orderApp =>
            {
                orderApp.Use(async (context, next) =>
                {
                    if (context.Request.Headers.ContainsKey("Authorization"))
                    {
                        var authorizationHeader = context.Request.Headers["Authorization"].ToString();
                        if (authorizationHeader.StartsWith("Bearer "))
                        {
                            context.Request.Headers["Authorization"] = authorizationHeader.Substring("Bearer ".Length);
                        }
                    }

                    await next();
                });
                orderApp.UseMiddleware<AuthenticationMiddleware>();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}