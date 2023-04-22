using Domain.Context;
using Microsoft.EntityFrameworkCore;


namespace LMSWebApi.Helpers
{
    internal static class DbInitializerExtension
    {
        public static IApplicationBuilder UseItToSeedSqlServer(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app, nameof(app));

            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                string vtype;
                DbInitializer.Initialize(context, "vBeforeseed");
                UserSeed.SeedDatabase(app);
                DbInitializer.Initialize(context, "vafterseed");
            }
            catch (Exception ex)
            {

            }

            return app;
        }




    }
}
