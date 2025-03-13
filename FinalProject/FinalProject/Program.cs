using FinalProject.Application.ServiceRegisteratation;
using FinalProject.Domain.Entities;
using FinalProject.Infrastructure.ServiceRegisteration;
using FinalProject.Persistence.Contexts;
using FinalProject.Persistence.ServiceRegisteratation;
using Microsoft.OpenApi.Models;
using Stripe;




internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddPersistenceServices(builder.Configuration)
                        .AddApplicationServices()
                        .AddInfrastructureServices(builder.Configuration);




        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "FinalProject", Version = "v1" });

            // JWT Auth için Bearer Token Ayarý
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Token ile Authentication (Örnek: 'Bearer {token}')",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT"
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
            new string[] { }
        }
            });
        });


        builder.Services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowReactApp",
                builder => builder.WithOrigins("file:///C:/Users/pc/OneDrive/Masa%C3%BCst%C3%BC/StripeFront/index.html")
                                  .AllowAnyMethod()
                                  .AllowAnyHeader());
        });

        builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

        var app = builder.Build();
        app.UseAuthentication();
        app.UseAuthorization();
        var scope = app.Services.CreateScope();
        var initalizer = scope.ServiceProvider.GetRequiredService<AppDbContextInitalizer>();
        StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];
        initalizer.InitializeDb().Wait();
        initalizer.CreateRolesAsync().Wait();
        initalizer.InitalizeAdminAsync().Wait();



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