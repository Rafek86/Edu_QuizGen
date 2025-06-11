using Edu_QuizGen.Helpers;
using Edu_QuizGen.Repository;
using Edu_QuizGen.Repository_Abstraction;
using Edu_QuizGen.Service_Abstraction;
using Edu_QuizGen.Services;
using Edu_QuizGen.Settings;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using Org.BouncyCastle.Asn1.X509.Qualified;

namespace Edu_QuizGen;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services,
       IConfiguration configuration)
    {
        services.AddControllers();

        services.AddCors(options => {
            options.AddDefaultPolicy(builder =>
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            //options.AddPolicy("CustomPolicy",builder=> builder.)
            }
        );


        services.AddAuthConfig(configuration);

        var connectionString = configuration.GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));


        services
            .AddSwaggerServices()
            .AddFluentValidationConfig();

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddAutoMapper(typeof(MappingProfiles));

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IFeedbackService, FeedbackService>();
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IQuestionSevice, QuestionServices>();
        services.AddScoped<IOptionService, OptionServices>();
        services.AddScoped<IHashService, HashService>();
        services.AddScoped<IQuizService, QuizService>();

        services.AddScoped<IEmailSender, EmailService>();

        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();

        services.AddScoped<IFeedbackRepository, FeedbackRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<IOptionRepository, OptionRepository>();
        services.AddScoped<IHashRepository, HashRepository>();
        services.AddScoped<IQuizRepository, QuizRepository>();
        services.AddScoped<IRoomStudentRepository, RoomStudentRepository>();

        services.AddHttpClient<IQuizGenerationService, QuizGenerationService>();
        services.AddScoped<IQuizGenerationService, QuizGenerationService>();

        services.AddHttpContextAccessor();  

        services.AddProblemDetails();

        return services;
    }

    private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }


    private static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
    {
        services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }

    private static IServiceCollection AddAuthConfig(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddIdentity<ApplicationUser, ApplicationRole>()
          .AddEntityFrameworkStores<ApplicationDbContext>()
          .AddDefaultTokenProviders();


        services.AddSingleton<IJwtProvider, JwtProvider>();

        services.AddOptions<JwtOptions>()
            .BindConfiguration(JwtOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var jwtSetting = configuration.GetSection(JwtOptions.SectionName)
            .Get<JwtOptions>();

        services.AddAuthentication(options =>
        {

            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o=> {

            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting?.Key!)),
                ValidIssuer = jwtSetting.Issuer,
                ValidAudience = jwtSetting.Audience
            };
        
        });

        services.AddOptions<MailSettings>()
            .BindConfiguration(MailSettings.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequiredLength = 8;
            options.SignIn.RequireConfirmedEmail = false;
            options.User.RequireUniqueEmail = true;

            //Default Values to the lockout
            options.Lockout.AllowedForNewUsers = true;
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        });

        return services;
    }

    
}
