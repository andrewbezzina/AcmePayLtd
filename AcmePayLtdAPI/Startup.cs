using AcmePayLtdLibrary;
using Microsoft.OpenApi.Models;
using AcmePayLtdLibrary.DataAccess;
using MediatR;
using System.Net.NetworkInformation;
using AcmePayLtdLibrary.Services;
using Microsoft.EntityFrameworkCore;

namespace AcmePayLtdAPI
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AcmePayLtdApi", Version = "v1" });
            });

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(typeof(AcmePayLtdLibraryMediatREntrypoint).Assembly);
            });

            // TODO replace with SQL data access.
            services.AddSingleton<ITransactionDataAccess, DemoTransactionDataAccess>();
            services.AddScoped<ITransactionHelpers, TransactionHelpers>();

            //Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.json");
            services.AddDbContext<TransactionDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TransactionSqlDatabase")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
