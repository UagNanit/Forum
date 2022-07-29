using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Forum._3.Data;
using Forum._3.Data.Abstract;
using Forum._3.Data.Repositories;
using Forum._3.Services;
using Forum._3.Services.Abstraction;
using Microsoft.EntityFrameworkCore;


namespace Forum._3
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

            services.AddControllers();

            services.AddDbContext<DBContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("ForumContext")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Forum", Version = "v1" });
            });

            services.AddCors();  // добавляем сервисы CORS

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                       options.RequireHttpsMetadata = true;
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                            
                           ValidateIssuer = false,  // укзывает, будет ли валидироваться издатель при валидации токена
                           //ValidIssuer = AuthOptions.ISSUER,  // строка, представляющая издателя
                           ValidateAudience = false,    // будет ли валидироваться потребитель токена           
                           //ValidAudience = AuthOptions.AUDIENCE,  // установка потребителя токена      
                           ValidateLifetime = true,  // будет ли валидироваться время существования          
                           ValidateIssuerSigningKey = true, // будет ли валидация ключа безопасности        
                           IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),  // установка ключа безопасности
                       };
                });

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddSingleton<IAuthService>(new AuthService());
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Forum._3 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder => builder
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
               //.AllowCredentials()
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
