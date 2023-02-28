using Manager.Infra.Interfaces;
using Manager.Infra.Repositories;
using Manager.Service.Interfaces;
using Manager.Service.Services;
using Microsoft.OpenApi.Models;

namespace Manager.API;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        #region AutoMapper
        services.AddAutoMapper(typeof(UserService).Assembly);
        #endregion

        #region Di
        services.AddSingleton(Configuration);
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>();
        // services.AddDbContext<ManagerContext>(options => options
        //     .UseMySql(Configuration["ConnectionStrings:USER_MANAGER"],
        //         new MySqlServerVersion(new Version(8, 0, 11))), ServiceLifetime.Transient);
        #endregion

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Manager.API", Version = "v1"});
        });
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseDeveloperExceptionPage();

        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Manager.API v1"));

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}