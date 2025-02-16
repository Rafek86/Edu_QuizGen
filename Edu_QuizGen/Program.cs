namespace Edu_QuizGen
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            //builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            //.AddEntityFrameworkStores<ApplicationDbContext>()
            //.AddDefaultTokenProviders();


            //builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //builder.Services.AddScoped<IAuthService, AuthService>();    
            builder.Services.AddDependencies(builder.Configuration);

            // to able frontend to access the api
            //builder.Services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAllOrigins",
            //        builder =>
            //        {
            //            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            //        });
            //});

            var app = builder.Build();


            #region dataSeeding
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            await ApplicationDbContextSeedData.SeedUserAsync(userManager);
            #endregion

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            // Configure the HTTP request pipeline.
            //app.UseCors("AllowAllOrigins");

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            

            app.MapControllers();

            app.Run();
        }
    }
}
