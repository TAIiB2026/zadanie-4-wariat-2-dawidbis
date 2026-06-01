using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebAPI.Interfaces;
using WebAPI.Services;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var angularCorsPolicy = "AllowAngularApp";
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: angularCorsPolicy,
                              policy =>
                              {
                                  policy.WithOrigins("http://localhost:4106")
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                              });
        });

        builder.Services.AddSingleton<DataService>();
        builder.Services.AddTransient<IGetDataInterface>(x => x.GetRequiredService<DataService>());
        builder.Services.AddTransient<IFormSubmitInterface>(x => x.GetRequiredService<DataService>());
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(angularCorsPolicy);
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}