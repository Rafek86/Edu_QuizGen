namespace Edu_QuizGen
{
    public class Program
    {
        public static void Main(string[] args)
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


            var app = builder.Build();


            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            // Configure the HTTP request pipeline.

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
// 