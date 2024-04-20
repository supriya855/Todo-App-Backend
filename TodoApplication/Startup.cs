using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using TodoApplicationWebAPI.Model;
using TodoApplicationWebAPI.Services;

namespace mongodb_dotnet_example
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
            services.Configure<TodoDatabaseSettings>(Configuration.GetSection(nameof(TodoDatabaseSettings)));

            services.AddSingleton<ITodoDatabaseSettings>(sp => sp.GetRequiredService<IOptions<TodoDatabaseSettings>>().Value);

            services.AddSingleton<ApplicationDBContext>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "mongodb_dotnet_example", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();


            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "mongodb_dotnet_example v1"));

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

