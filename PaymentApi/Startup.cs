using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PaymentApi.Data;
using PaymentApi.Services;
using PaymentApi.Services.PaymentGateways;

namespace PaymentApi
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
            services.AddDbContext<PaymentsDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("PaymentStore")));
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<ICheapPaymentGateway, CheapPaymentGateway>();
            services.AddTransient<IExpensivePaymentGateway, ExpensivePaymentGateway>();
            services.AddTransient<IPremiumPaymentGateway, PremiumPaymentGateway>();
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IUnitOfWork, EfUnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
