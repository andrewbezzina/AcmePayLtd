using AcmePayLtdLibrary.DataAccess;
using AcmePayLtdLibrary.Services;
using AcmePayLtdLibrary;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using AcmePayLtdLibrary.Models.Request;
using AcmePayLtdAPI.Validators;

namespace AcmePayLtdAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(typeof(AcmePayLtdLibraryMediatREntrypoint).Assembly);
            });

            builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.json");
            builder.Services.AddDbContext<TransactionDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TransactionSqlDatabase")));

            builder.Services.AddScoped<ITransactionDataAccess, DbTransactionDataAccess>();
            // If you fail to connect to sql db for some reason please comment above line and uncomment below to use in memory data
            //builder.Services.AddSingleton<ITransactionDataAccess, DemoTransactionDataAccess>();

            builder.Services.AddScoped<ITransactionHelpers, TransactionHelpers>();
            builder.Services.AddScoped<IValidator<PostAuthorizeTransactionModel>, PostAuthorizeTransactionValidator>();

            var app = builder.Build();

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