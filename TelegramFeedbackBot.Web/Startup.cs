using BridgeWebTelegram.Core.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Telegram.Bot;

namespace BridgeWebTelegram.Web
{
    public class Startup
    {
        private const string corsAllowTestingFromFileOrigin = "null";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Return a config from the appsetting.{environment}.json        
        private IConfiguration StartupConfiguration(string environmentName = null)
        {
            var environment = environmentName ?? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IConfiguration result = new ConfigurationBuilder()
               .AddJsonFile($"appsettings.{environment}.json", true, true)
               .Build();
            return result;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddCors();
            services.AddControllers();

            IConfiguration cfg = StartupConfiguration("Production");
            var ownerTelegramId = cfg.GetValue<long>("OwnerTelegramId");
            var telegramBotToken = cfg.GetValue<string>("botToken");
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

            ////Log every request(for debug purposes)
            //app.Use(async (context, next) =>
            //{
            //    string logFileName = "log.log";
            //    if (!File.Exists(logFileName))
            //    {
            //        File.WriteAllText(logFileName, string.Empty);
            //        Thread.Sleep(100);
            //    };

            //    var request = context.Request;
            //    using (var bodyReader = new StreamReader(context.Request.Body))
            //    {

            //        string host = request.Host.ToString();
            //        string path = request.Path;                   
            //        string localIp = request.HttpContext.Connection.LocalIpAddress.ToString();
            //        string remoteIp = request.HttpContext.Connection.RemoteIpAddress.ToString();

            //        string messageToLog = $"{host}, {path}, {localIp}, {remoteIp}";
            //        Console.WriteLine(messageToLog);

            //        try
            //        {
            //            File.AppendAllText(logFileName, messageToLog + Environment.NewLine);
            //            await next.Invoke();
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine(ex.ToString());
            //            // suppress logging exception
            //        }
            //    }
            //});

            // Setup cors
            string[] allowedOrigins = StartupConfiguration(env.EnvironmentName)
                ?.GetValue<string>("allowedOrigins")
                ?.Split(";") ?? new string[] { corsAllowTestingFromFileOrigin };
            

            app.UseCors(
                options => options
                    .WithHeaders("Content-Type", "Accept", "Referer")
                    .WithMethods("GET","POST")
                    .WithOrigins(allowedOrigins)// TODO read allowed origins from config file on non-Dev environments                                         
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
