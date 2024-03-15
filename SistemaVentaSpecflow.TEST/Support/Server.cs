using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using SistemaVenta.DAL.DBContext;

namespace SistemaVentaSpecflow.TEST.Support
{
    public class Server
    {
        public static string Hostname { get; private set; }
        public static HttpClient ArrancarServidor()
        {
            Hostname = $"http://localhost:5043";

            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseEnvironment("Testing");

                    builder.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<DBTIENDAContext>));

                        services.Remove(descriptor);

                        services.AddDbContext<DBTIENDAContext>(options =>
                        {
                            options.UseInMemoryDatabase("InMemoryDbForTesting");
                        });



                        var sp = services.BuildServiceProvider();

                        using (var scope = sp.CreateScope())
                        {
                            var scopedServices = scope.ServiceProvider;
                            var db = scopedServices.GetRequiredService<DBTIENDAContext>();

                            db.Database.EnsureCreated();

                            try
                            {
                                //db.Database.Migrate();
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                        }
                    });
                });
            HttpClient client = application.CreateClient();
            return client;
        }

    }
}
