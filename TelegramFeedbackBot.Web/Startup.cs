using BridgeWebTelegram.Core.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;

namespace BridgeWebTelegram.Web
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
            IConfiguration cfg = new ConfigurationBuilder()
               .AddJsonFile("appsettings.Prod.json", true, true)
               .Build();
            var ownerTelegramId = cfg.GetValue<long>("OwnerTelegramId");
            var telegramBotToken = cfg.GetValue<string>("botToken");

            services.AddCors();
            services.AddControllers();

            var botClient = new TelegramBotClient(telegramBotToken);
            services.AddSingleton(new NotificationDirector(ownerTelegramId, botClient));
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(
                options => options
                    .WithHeaders("Content-Type")
                    .WithMethods("GET","POST")
                    .WithOrigins("null")// TODO read allowed origins from config file on non-Dev environments                                         
            );

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
